using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using USBTCommon;


namespace Test
{

    public partial class ToolMain : Form
    {
        //Storage for reference operation objects.
        Form outWind; //Hack for the console window. Should figure out a way to have it just write to a buffer, then have it call that in a new report window.
        public static Dictionary<string, int> instDict; //How many instances of each application running are there
        public List<Type> typeList = new List<Type>(); //List of our known types. Needed for deserializing.
        public List<string> appList = new List<string>(); //List of apps. Needed for GUI stuff.

        bool debug = false;

        public ToolMain(string[] args)
        {
            //Construct.
            instDict = new Dictionary<string, int>();
            InitializeComponent();

            //Initialize and populate.
            initializeTests();
            opQueue.InsertionMark.Color = Color.Green;

            //Run through the arguments
            if (args.Length > 0)
            {
                if (args.Contains("-o"))
                {
                    //If we have an open, go find the test file to load.
                    int pos = Array.IndexOf(args, "-o");
                    string path = args[pos + 1];

                    //Load the test file
                    if (File.Exists(path))
                    {
                        IOperation[] list = Xml.loadOpsFile(path, typeList.ToArray());
                        if (list != null)
                        {
                            foreach (IOperation op in list)
                            {
                                addOperation(op, opQueue.Items.Count);
                            }
                        }

                        //If we have an open and a run...
                        if (args.Contains("-r"))
                        {
                            runTest_Click(null, EventArgs.Empty);
                            Environment.Exit(1);
                        }
                    }
                }
            }
            else
            {
                outWind = new Report();
            }
        }

        //---------------------------
        //Related operation functions
        //---------------------------

        //Go find the test libaries ending with *Test.dll. Load them to the node dictionary. Display library count.
        private void initializeTests()
        {
            List<string> libPaths = Directory.GetFiles("Tests\\", "*.dll").ToList();
            Dictionary<Assembly, Type[]> nodeControl = new Dictionary<Assembly, Type[]>();
            int x = 0;
            foreach (string lib in libPaths)
            {
                if(lib.EndsWith("Tests.dll")) //If it's one of the test libraries, then load it.
                {
                    string path = Path.GetFullPath(lib);
                    Assembly dll = Assembly.LoadFile(path); //Load the dll as an assembly.

                    nodeControl.Add(dll, dll.GetTypes()); //Add the assembly and its types to a list.

                    foreach(Type theType in dll.GetTypes())
                    {
                        if (theType.IsSerializable)
                        {
                            typeList.Add(theType);
                        }
                    }
                    x++;
                }
            }
            generateTreeNode(nodeControl); //Generates the nodes with tagged IOperations for the tree.
            populateXmlCombo(appList);

            toolStripStatusLabel2.Text = x.ToString() + " test libraries registered.";
        }

        private Type[] requestMethods(Assembly dll)
        {
            Type[] types = dll.GetTypes();
            List<string> list = new List<string>();
            foreach(Type type in types)
            {
                list.Add(type.Name);
            }
            return types;
        }

        //Build the profile tree from the Assemblies
        private void generateTreeNode(Dictionary<Assembly,Type[]> nodeControl)
        {
            TreeNode t1, t2, t3;
            foreach (KeyValuePair<Assembly, Type[]> item in nodeControl)
            {
                FieldInfo field = item.Key.GetType(item.Key.GetName().Name + ".Data").GetField("app"); //Get the Data class and pull the app field.
                string theName = field.GetValue(null).ToString(); //Get the value from the app field. (needs an object, but works fine with null)

                t1 = new TreeNode(theName);
                appList.Add(theName);
                profileTree.Nodes.Add(t1);

                t2 = new TreeNode("Operation"); //Add an Operations subtree. Probably unnecessary.
                t1.Nodes.Add(t2);
                
                foreach (Type subitem in item.Value)
                {
                    //Make sure the type uses the IOperation interface.
                    if (subitem.BaseType != typeof(Form) && subitem.GetInterfaces().Contains(typeof(IOperation)))
                    {
                        IOperation theObj = (IOperation)Activator.CreateInstance(subitem);
                        t3 = new TreeNode(theObj.settings["op_name"]);
                        t3.Tag = theObj; //Tag the op to the node.
                        t2.Nodes.Add(t3); //Push it to the tree.
                    }
                }
            }
            profileTree.Sort(); //Sort alphabetically.
        }

        private void populateXmlCombo(List<string>apps)
        {
            appList.Sort();
            profileCombo.Items.Add("All");
            foreach (String app in apps)
            {
                profileCombo.Items.Add(app);
            }
        }

        //Update the path location. Called when path is changed manually.
        public void appPathUpdate(string path)
        {
            //Use if a new path has been defined or being defined for 1st time.
            IOperation op = (IOperation)opQueue.SelectedItems[0].Tag;
            if (File.Exists(path))
            {
                //Updates operation checksum with hash from new executable.
                if (op.settings["app_path"] != path)
                {
                    //string newHash = Verify.GetMD5HashFromFile(path);
                    //op.settings["cur_hash"] = newHash;
                    op.settings["app_path"] = path;
                }
                app_path.Text = op.settings["app_path"];

                //Retrieve new file information to update description and icon.
                FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(path);
                Icon icon1 = Icon.ExtractAssociatedIcon(path);
                appIcon.Image = icon1.ToBitmap();
            }
            else
                clearOpInfo();
        }

        public void addOperation(IOperation newOp, int index)
        {
            //Generate list view item from Operation makeListKey method.
            string[] elements = { newOp.settings["op_name"], newOp.settings["app_name"] };
            ListViewItem key = new ListViewItem(elements);
            key.Tag = newOp; //Tag the Listview item with the operation.

            //Error check. Is this needed?
            if (newOp == null)
                Console.WriteLine("OBJECT NULL");
            else
            {
                string name = newOp.settings["app_name"];

                //Get application icon if the path is valid.
                if (File.Exists(newOp.settings["app_path"]))
                {
                    Icon appIcon = Icon.ExtractAssociatedIcon(newOp.settings["app_path"]);
                    iconImages.Images.Add(name,appIcon);
                }

                opQueue.Items.Insert(index, key); //Adds the Listview item at a position in the list.

                //Add new window object if operation called an exe. Generate app instance + accompanying window.
                int i = 0;
                if (newOp.settings["op_name"].ToLower().Contains("run")
                    && !newOp.settings["op_name"].ToLower().Contains("script")) //Temp fix for the Run script bug. Need to fix. Add flags to operation?
                {
                    if (!instDict.ContainsKey(name))
                    {
                        instDict.Add(name, 0);
                    }
                    else
                    {
                        instDict[name]++;
                    }
                    foreach (ListViewItem wind in windowList.Items)
                    {
                        if (((Window)wind.Tag).name == name)
                            i++;
                    }
                    ListViewItem window = new ListViewItem(name + ": " + instDict[name].ToString(), name);
                    window.Tag = new Window("", "", name, instDict[name]);
                    windowList.Items.Add(window);
                }

                if (!newOp.settings.ContainsKey("app_instance"))
                {
                    newOp.settings.Add("app_instance", i.ToString());
                }

                //Colors the listviewitem if checked.
                if (appQueueColorsToolStripMenuItem.Checked)
                {
                    if (File.Exists(newOp.settings["app_path"]))
                    {
                        Icon appIcon = Icon.ExtractAssociatedIcon(newOp.settings["app_path"]);
                        key.BackColor = getDominantColor(appIcon.ToBitmap());
                    }
                }
            }
        }

        //Will add more to this to take care of removing app instances and windows.
        public void delOperation(IOperation op, int index)
        {
            //foreach (Window win in windList)
            //{
            //    if (win.name == op.settings["app_name"] && win.instance == Convert.ToInt32(op.settings["app_instance"]))
            //    {
            //        opWind = win;
            //        break;
            //    }
            //    i++;
            //}

            //IOperation temp = (IOperation)key.Tag;
            //if (temp.settings.ContainsKey("head_instance"))
            //{
            //    ListViewItem[] list = windowList.Items.Find(temp.settings["app_name"] + ": " + temp.settings["head_instance"], false);
            //    windowList.Items.Remove(list[0]);
            //}
            
        } 

        //Clear the settings boxes
        public void clearOpInfo()
        {
            app_path.Text = "";
            appIcon.Image = null;
            requiredList.Items.Clear();
            //Clear the textboxes in the settings groups.
            foreach (Control con in infoGroup.Controls)
            {
                if(con.GetType() == typeof(TextBox))
                {
                    con.Text = "";
                }
            }
            foreach (Control con in optionsGroup.Controls)
            {
                if (con.GetType() == typeof(TextBox))
                {
                    con.Text = "";
                }
            }
        }

        //Colorize the operation queue
        public void opColors()
        {
            if (appQueueColorsToolStripMenuItem.Checked)
            {
                foreach (ListViewItem op in opQueue.Items)
                {
                    if (File.Exists(((IOperation)op.Tag).settings["app_path"]))
                    {
                        Icon icon1 = Icon.ExtractAssociatedIcon(((IOperation)op.Tag).settings["app_path"]);
                        op.BackColor = getDominantColor(icon1.ToBitmap());
                    }
                }
            }
            else
            {
                foreach (ListViewItem op in opQueue.Items)
                {
                    op.BackColor = Color.White;
                }
            }
        }

        //Colorize the tree
        public void treeColors()
        {
            if (appTreeColorsToolStripMenuItem.Checked)
            {
                foreach (TreeNode op in profileTree.Nodes)
                {
                    TreeNode list = op.Nodes[0].Nodes[0];
                    if (File.Exists(((IOperation)list.Tag).settings["app_path"]))
                    {
                        Icon icon1 = Icon.ExtractAssociatedIcon(((IOperation)list.Tag).settings["app_path"]);
                        op.BackColor = getDominantColor(icon1.ToBitmap());
                    }
                }
            }
            else
            {
                foreach (TreeNode op in profileTree.Nodes)
                {
                    op.BackColor = Color.White;
                }
            }
        }

        //Clear everything.
        public void clearForms()
        {
            windowList.Items.Clear();
            requiredList.Items.Clear();
            instDict.Clear();
            opQueue.Items.Clear();
            clearOpInfo();
        }

        //Get the color for background shading
        public static Color getDominantColor(Bitmap bmp)
        {
            //Used for tally
            int r = 0;
            int g = 0;
            int b = 0;

            int total = 0;
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color clr = bmp.GetPixel(x, y);
                    if (clr.GetHue() > 100.00 | clr.GetHue() < 200.00)
                    {
                        if (clr.GetBrightness() < 0.8 | clr.GetBrightness() > 0.6 )
                        {
                            if (clr.GetSaturation() > 0.5)
                            {
                                r += clr.R;
                                g += clr.G;
                                b += clr.B;
                                total++;
                            }
                        }
                    }
                }
            }
            //Calculate average
            r /= total;
            g /= total;
            b /= total;
            return Color.FromArgb(r, g, b);
        }

        //Called when a new item is pressed or data in the info boxes needs to be updated.
        public void updateOpInfo()
        {
            if (opQueue.SelectedIndices.Count > 0)
            {
                ListViewItem key = opQueue.SelectedItems[0];
                IOperation op = (IOperation)key.Tag;

                string[] flags;
                string[] required;
                
                //Update title, application, and association
                op_title.Text = op.settings["op_name"];
                associate_name.Text = op.settings["app_name"];

                //Generate our flags and requirements.
                if (op.settings.ContainsKey("flags"))
                {
                    flags = op.settings["flags"].Split(',');
                }
                if (op.settings.ContainsKey("required"))
                {
                    required = op.settings["required"].Split(',');
                }
                
                //Add the settings to the settings listview.
                requiredList.Items.Clear();
                foreach (KeyValuePair<string, string> item in op.settings)
                {
                    string[] elements = { item.Key, item.Value };
                    requiredList.Items.Add(new ListViewItem(elements));
                }

                //If there is an entry in the instDict for this application, set the maximum value.
                if (instDict.ContainsKey(op.settings["app_name"]))
                {
                    instSelector.Maximum = instDict[op.settings["app_name"]];
                    instSelector.Value = Convert.ToDecimal(op.settings["app_instance"]);
                }

                //Do checks on available settings

                //Description
                if (op.settings.ContainsKey("op_desc"))
                {
                    op_desc.Text = op.settings["op_desc"];
                }
                else
                {
                    op_desc.Text = "";
                }

                //Opens a file.
                if (op.settings.ContainsKey("open"))
                {
                    open_path.Enabled = true;
                    fileOpen.Enabled = true;
                    open_path.Text = op.settings["open"];
                }
                else
                {
                    open_path.Enabled = false;
                    fileOpen.Enabled = false;
                    open_path.Text = "";
                }

                //Saves a file.
                if (op.settings.ContainsKey("save"))
                {
                    save_path.Enabled = true;
                    fileSave.Enabled = true;
                    save_path.Text = op.settings["save"];
                }
                else
                {
                    save_path.Enabled = false;
                    fileSave.Enabled = false;
                    save_path.Text = "";
                }

                //Opens a directory.
                if (op.settings.ContainsKey("directory"))
                {
                    directory_path.Enabled = true;
                    fileDir.Enabled = true;
                    directory_path.Text = op.settings["directory"];
                }
                else
                {
                    directory_path.Enabled = false;
                    fileDir.Enabled = false;
                    directory_path.Text = "";
                }

                //Requires input of some kind.
                if (op.settings.ContainsKey("input"))
                {
                    fileInput.Enabled = true;
                    input_path.Enabled = true;
                    input_text.Enabled = true;
                    input_path.Text = op.settings["input"];
                    if (op.settings.ContainsKey("input_text"))
                    {
                        input_text.Text = op.settings["input_text"];
                        inputLabel.Text = op.settings["app_name"] + " - " + op.settings["op_name"];
                        inputLanguageSelect.Text = op.settings["input_type"];
                    }
                    else
                    {
                        input_text.Text = "";
                        inputLabel.Text = "";
                        inputLanguageSelect.Text = "";
                    }
                    if (op.settings.ContainsKey("input_name"))
                    {
                        inputName.Enabled = true;
                        inputName.Text = op.settings["input_name"];
                    }
                    else
                    {
                        inputName.Enabled = false;
                        inputName.Text = "";
                    }
                }
                else
                {
                    input_path.Enabled = false;
                    fileInput.Enabled = false;
                    input_text.Enabled = false;
                    inputName.Enabled = false;
                    inputName.Text = "";
                    input_path.Text = "";
                    input_text.Text = "";
                    inputLabel.Text = "";
                    inputLanguageSelect.Text = "";
                }
                if (op.settings["app_name"] != "Generic")
                {
                    appPathUpdate(op.settings["app_path"]);
                }
            }
        }

        //---------------------------
        //GUI Events
        //---------------------------

        private void profileCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void opQueue_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateOpInfo();
        }

        private void addOp_Click(object sender, EventArgs e)
        {
            string path = profileTree.SelectedNode.FullPath;
            string[] items = path.Split('\\');
            if (items.Length >= 3)
            {
                IOperation refOp = (IOperation)profileTree.SelectedNode.Tag;

                Dictionary<string, string> ret = new Dictionary<string, string>(refOp.settings.Count, refOp.settings.Comparer);
                foreach (KeyValuePair<string, string> entry in refOp.settings)
                {
                    ret.Add(entry.Key, (string)entry.Value);
                }

                Type opType = refOp.GetType();
                Object theObj = Activator.CreateInstance(opType);
                IOperation newOp = (IOperation)theObj;

                newOp.settings = ret;
                newOp.timers = refOp.timers;
                addOperation(newOp,opQueue.Items.Count);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (opQueue.SelectedIndices.Count > 0)
            {
                int n = opQueue.SelectedIndices[0];
                ListViewItem key = opQueue.Items[n];
                ListViewItem next = new ListViewItem();
                if (opQueue.Items.Count > 1)
                {
                    if (n < opQueue.Items.Count - 1)
                    {
                        next = opQueue.Items[n + 1];
                    }
                    else if (n >= opQueue.Items.Count - 1)
                    {
                        next = opQueue.Items[n - 1];
                    }
                    else
                    {
                        return;
                    }
                }
                opQueue.Items.Remove(key);
                opQueue.Focus();
                next.Selected = true;
            }
        }

        private void fileOpen_Click(object sender, EventArgs e)
        {
            if (opQueue.SelectedItems.Count > 0)
            {
                IOperation op = (IOperation)opQueue.SelectedItems[0].Tag;
                OpenFileDialog ofn = new OpenFileDialog();
                ofn.Filter = "All |*.*";
                ofn.Title = "Type File";
                if (ofn.ShowDialog() != DialogResult.Cancel)
                {
                    open_path.Text = ofn.FileName;
                    ((IOperation)opQueue.SelectedItems[0].Tag).settings["open"] = ofn.FileName;
                }
            }
        }

        private void fileSave_Click(object sender, EventArgs e)
        {
            if (opQueue.SelectedItems.Count > 0)
            {
                IOperation op = (IOperation)opQueue.SelectedItems[0].Tag;
                SaveFileDialog sfn = new SaveFileDialog();
                sfn.Filter = "All |*.*";
                sfn.Title = "Type File";
                if (sfn.ShowDialog() != DialogResult.Cancel)
                {
                    save_path.Text = sfn.FileName;
                    op.settings["save"] = sfn.FileName;
                }
            }
        }

        private void fileInput_Click(object sender, EventArgs e)
        {
            if (opQueue.SelectedItems.Count > 0)
            {
                IOperation op = (IOperation)opQueue.SelectedItems[0].Tag;
                OpenFileDialog ofn = new OpenFileDialog();
                ofn.Filter = "Text |*.txt";
                ofn.Title = "Type File";
                if (ofn.ShowDialog() != DialogResult.Cancel)
                {
                    StreamReader parse = File.OpenText(ofn.FileName);
                    input_path.Text = ofn.FileName;
                    string parsed = "";
                    while (!parse.EndOfStream)
                    {
                        parsed += parse.ReadLine() + System.Environment.NewLine;
                    }
                    input_text.Text = parsed;
                    inputLabel.Text = op.settings["app_name"] + " - " + op.settings["op_name"];
                    op.settings["input_text"] = parsed;
                    op.settings["input"] = ofn.FileName;
                }
            }
        }

        private void fileDir_Click(object sender, EventArgs e)
        {
            if (opQueue.SelectedItems.Count > 0)
            {
                IOperation op = (IOperation)opQueue.SelectedItems[0].Tag;
                FolderBrowserDialog ofn = new FolderBrowserDialog();
                if (ofn.ShowDialog() != DialogResult.Cancel)
                {
                    directory_path.Text = ofn.SelectedPath;
                    op.settings["directory"] = ofn.SelectedPath;
                }
            }
        }     

        private void saveOps_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfn = new SaveFileDialog();
            sfn.Filter = "XML |*.xml";
            sfn.Title = "Type File";
            if (sfn.ShowDialog() != DialogResult.Cancel)
            {
                List<IOperation> opsList = new List<IOperation>();
                foreach (ListViewItem op in opQueue.Items)
                {
                    opsList.Add((IOperation)op.Tag);
                }
                Xml.saveOpsFile(sfn.FileName, opsList.ToArray(), typeList.ToArray());
                ToolMain.ActiveForm.Text = "Benchmark - " + sfn.FileName;
                
            }
        }

        private void loadOps_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofn = new OpenFileDialog();
            ofn.Filter = "XML |*.xml";
            ofn.Title = "Type File";
            if (ofn.ShowDialog() == DialogResult.Cancel) { }
            else
            {
                clearForms();
                IOperation[] list = Xml.loadOpsFile(ofn.FileName,typeList.ToArray());
                if (list != null)
                {
                    foreach (IOperation op in list)
                    {
                        addOperation(op, opQueue.Items.Count);
                    }
                    //ToolMain.ActiveForm.Text = "Benchmark Tool - " + ofn.FileName;
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete_Click(null, EventArgs.Empty);
        }

        private void listContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (opQueue.SelectedIndices.Count == 0)
                listContextMenu.Enabled = false;
            else
                listContextMenu.Enabled = true;
        }

        private void treeContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            string path = profileTree.SelectedNode.FullPath;
            string[] items = path.Split('\\');
            if (items.Length >= 3)
            {
                treeContextMenu.Enabled = true;
            }
            else
                treeContextMenu.Enabled = false;
        }

        private void profileTree_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                profileTree.SelectedNode = profileTree.GetNodeAt(e.X, e.Y);

                if (profileTree.SelectedNode != null)
                {
                    treeContextMenu.Show(profileTree, e.Location);
                }
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolMain.ActiveForm.Close();
        }

        private void opSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TestSettings(opQueue).Show();
        }

        private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (opQueue.SelectedItems.Count > 0)
            {
                IOperation refOp = (IOperation)opQueue.SelectedItems[0].Tag;

                Dictionary<string, string> ret = new Dictionary<string, string>(refOp.settings.Count, refOp.settings.Comparer);
                foreach (KeyValuePair<string, string> entry in refOp.settings)
                {
                    ret.Add(entry.Key, (string)entry.Value);
                }

                Type opType = refOp.GetType();
                Object theObj = Activator.CreateInstance(opType);
                IOperation newOp = (IOperation)theObj;

                newOp.settings = ret;
                newOp.timers = refOp.timers;
                addOperation(newOp, opQueue.SelectedIndices[0] + 1);
            }
        }

        //------------------------------------
        //Handles drag/drop of operation queue.
        private void opQueue_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void opQueue_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void opQueue_DragOver(object sender, DragEventArgs e)
        {

            Point targetPoint =
                opQueue.PointToClient(new Point(e.X, e.Y));

            int targetIndex = opQueue.InsertionMark.NearestIndex(targetPoint);

            if (targetIndex > -1)
            {
                Rectangle itemBounds = opQueue.GetItemRect(targetIndex);
                if (targetPoint.X > itemBounds.Left + (itemBounds.Width / 2))
                {
                    opQueue.InsertionMark.AppearsAfterItem = true;
                }
                else
                {
                    opQueue.InsertionMark.AppearsAfterItem = false;
                }
            }

            opQueue.InsertionMark.Index = targetIndex;
        }

        private void opQueue_DragDrop(object sender, DragEventArgs e)
        {
            int dropIndex = opQueue.InsertionMark.Index;

            if (dropIndex == -1)
            {
                return;
            }

            if (opQueue.InsertionMark.AppearsAfterItem)
            {
                dropIndex++;
            }

            if(e.Data.GetDataPresent(typeof(TreeNode)))
            {
                TreeNode tree = (TreeNode)e.Data.GetData(typeof(TreeNode));
                IOperation op = (IOperation)tree.Tag;
                addOperation(op, dropIndex);

            }
            if(e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                ListViewItem dragged = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                opQueue.Items.Insert(dropIndex,(ListViewItem)dragged.Clone());
                opQueue.Items.Remove(dragged);
            }
            
        }

        private void opQueue_DragLeave(object sender, EventArgs e)
        {
            opQueue.InsertionMark.Index = -1;
        }

        private void profileTree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode selected = (TreeNode)e.Item;
            string[] str = selected.FullPath.Split('\\');
            if (str.Length >= 3)
            {
                profileTree.DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }
        //------------------------------------

        private void applicationColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            opColors();
        }

        private void appTreeColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeColors();
        }

        private void checkToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int n = opQueue.SelectedIndices[0];
            ListViewItem temp = opQueue.Items[n];
            MessageBox.Show(((IOperation)temp.Tag).ToString());
        }
        
        private void clearStripMenuItem_Click(object sender, EventArgs e)
        {
            clearForms();
        }

        private void instSelector_ValueChanged(object sender, EventArgs e)
        {
            if (opQueue.SelectedItems.Count > 0)
            {
                ListViewItem key = opQueue.SelectedItems[0];
                IOperation op = (IOperation)key.Tag;
                op.settings["app_instance"] = instSelector.Value.ToString();
            }
        }

        //Run the test
        private void runTest_Click(object sender, EventArgs e) 
        {
            if (opQueue.Items.Count > 0)
            {
                outWind.Show();
                List<IOperation> opList = new List<IOperation>();
                List<Window> winList = new List<Window>();
                foreach (ListViewItem item in opQueue.Items)
                {
                    opList.Add((IOperation)item.Tag);
                }
                foreach (ListViewItem item in windowList.Items)
                {
                    winList.Add((Window)item.Tag);
                }
                Execution.runTest(opList.ToArray(), winList.ToArray(), debug);
                Time.printTimers();
                runTest.Enabled = false;
            }
        }

        private void lastRunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            outWind.Show();
        }

        private void clearTimes_Click(object sender, EventArgs e)
        {
            Time.clearTimers();
            runTest.Enabled = true;
        }

        private void inputName_TextChanged(object sender, EventArgs e)
        {
            if (opQueue.SelectedItems.Count > 0)
            {
                if (inputName.Text != "")
                {
                    IOperation op = (IOperation)opQueue.SelectedItems[0].Tag;
                    op.settings["input_name"] = inputName.Text;
                }
            }
        }

        private void input_text_TextChanged(object sender, EventArgs e)
        {
            if (opQueue.SelectedItems.Count > 0)
            {
                if (input_text.Text != "")
                {
                    IOperation op = (IOperation)opQueue.SelectedItems[0].Tag;
                    op.settings["input_text"] = input_text.Text;
                }
            }
        }

        //If the user clicks the operation settings UI button
        private void opSettings_Click(object sender, EventArgs e)
        {
            if (opQueue.SelectedItems.Count > 0)
            {
                IOperation op = (IOperation)opQueue.SelectedItems[0].Tag;
                op.checkOp();
            }
        }

        //If the user modifies the text in the genertic settings textboxes
        private void directory_path_TextChanged(object sender, EventArgs e)
        {
            if (opQueue.SelectedItems.Count > 0)
            {
                IOperation op = (IOperation)opQueue.SelectedItems[0].Tag;
                if (op.settings.ContainsKey("directory"))
                    op.settings["directory"] = directory_path.Text;
            }
        }

        private void input_path_TextChanged(object sender, EventArgs e)
        {
            if (opQueue.SelectedItems.Count > 0)
            {
                IOperation op = (IOperation)opQueue.SelectedItems[0].Tag;
                if (op.settings.ContainsKey("input"))
                    op.settings["input"] = input_path.Text;
            }
        }

        private void save_path_TextChanged(object sender, EventArgs e)
        {
            if (opQueue.SelectedItems.Count > 0)
            {
                IOperation op = (IOperation)opQueue.SelectedItems[0].Tag;
                if (op.settings.ContainsKey("save"))
                    op.settings["save"] = save_path.Text;
            }
        }

        private void open_path_TextChanged(object sender, EventArgs e)
        {
            if (opQueue.SelectedItems.Count > 0)
            {
                IOperation op = (IOperation)opQueue.SelectedItems[0].Tag;
                if (op.settings.ContainsKey("open"))
                    op.settings["open"] = open_path.Text;
            }
        }

        private void debugFlag_CheckedChanged(object sender, EventArgs e)
        {
            debug = debugFlag.Checked;
        }
    }
}
