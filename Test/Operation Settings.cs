using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using USBTCommon;

namespace Test
{
    public partial class TestSettings : Form
    {
        ListView store = new ListView();

        public TestSettings(ListView opQueueMain)
        {
            InitializeComponent();
            store = opQueueMain;
        }

        private ListViewGroup isGroup(string name)
        {
            foreach (ListViewGroup g in opQueue.Groups)
            {
                if (g.Header == name)
                    return g;

            }
            return null;
        }

        public void appPathUpdate(string path)
        {
            foreach (ListViewItem item in opQueue.SelectedItems)
            {
                //Use if a new path has been defined or being defined for 1st time.
                IOperation op = (IOperation)item.Tag;
                if (File.Exists(path))
                {
                    //Updates operation checksum with hash from new executable.
                    if (op.settings["app_path"] != path)
                    {
                        string newHash = Verify.GetMD5HashFromFile(path);
                        op.settings["cur_hash"] = newHash;
                        op.settings["app_path"] = path;
                    }
                    else
                        cur_checksum.Text = op.settings["cur_hash"];

                    //Write new executable path.
                    cur_checksum.Text = op.settings["cur_hash"];
                    app_path.Text = op.settings["app_path"];

                    //Retrieve new file information to update description and icon.
                    FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(path);
                    appName.Text = fileInfo.FileDescription;
                    appVersion.Text = fileInfo.FileVersion;
                    appLegal.Text = fileInfo.LegalCopyright;
                    Icon icon1 = Icon.ExtractAssociatedIcon(path);
                    appIcon.Image = icon1.ToBitmap();
                }
                else
                    clearOpInfo(op);
            }
        }

        public void clearOpInfo(IOperation op)
        {
            cur_checksum.Text = "";
            app_path.Text = "";
            appName.Text = "";
            appIcon.Image = null;
        }

        public void updateOpInfo()
        {
            if (opQueue.SelectedIndices.Count > 0)
            {
                ListViewItem key = opQueue.SelectedItems[0];
                IOperation op = (IOperation)key.Tag;

                //Update title, application, and association
                if (op.settings["app_name"] != "Generic")
                {
                    appPathUpdate(op.settings["app_path"]);

                    //Update MD5 checksums
                    cur_checksum.Text = op.settings["cur_hash"];
                    ref_checksum.Text = op.settings["ref_hash"];
                }
            }
        }

        //---------------
        //GUI Events
        //---------------

        private void opQueue_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateOpInfo();
        }

        private void editAppPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofn = new OpenFileDialog();
            ofn.Filter = "EXE |*.exe";
            ofn.Title = "Type File";
            if (ofn.ShowDialog() != DialogResult.Cancel)
            {
                appPathUpdate(ofn.FileName);
            }
        }

        private void TestSettings_Load(object sender, EventArgs e)
        {
            foreach (ListViewItem op in store.Items)
            {
                IOperation opTag = (IOperation)op.Tag;
                string app_name = opTag.settings["app_name"];
                ListViewGroup find = new ListViewGroup();
                find.Header = app_name;
                if (isGroup(app_name) == null)
                {
                    int index = opQueue.Groups.Add(find);
                    ListViewItem temp = opQueue.Items.Add(opTag.settings["op_name"]);
                    ListViewGroup tempG = opQueue.Groups[index];
                    temp.Group = tempG;
                    temp.Tag = opTag;
                    tempG.Tag = opTag;

                }
                else
                {
                    ListViewItem temp = opQueue.Items.Add(opTag.settings["op_name"]);
                    temp.Group = isGroup(app_name);
                    temp.Tag = opTag;
                }
            }
        }
    }
}
