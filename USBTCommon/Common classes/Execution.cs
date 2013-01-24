using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Media;

namespace USBTCommon
{
    public class Execution
    {
        public static void runTest(IOperation[] opList, Window[] windList, bool debug)
        {
            Time.startTimer("Test", "Total time"); //Our timer for the entire run.
            foreach (IOperation op in opList)
            {
                int i = 0;
                if(debug) //Beep to tell us when a new operation has started.
                    SystemSounds.Asterisk.Play();

                Window opWind = new Window();

                //Get the window to manipulate
                foreach (Window win in windList)
                {
                    if(win.name == op.settings["app_name"] && win.instance == Convert.ToInt32(op.settings["app_instance"]))
                    {
                        opWind = win;
                        break;
                    }
                    i++;
                }
                //Make something up if a Generic command.
                if (op.settings["app_name"] == "Generic")
                {
                    opWind = new Window();
                }

                //Use runOp from the IOperation interface. Return the window when done with it.
                if(debug)
                    Console.WriteLine("Starting " + op.settings["app_name"] + " - " + op.settings["op_name"]);

                Window returnValue = op.runOp(opWind);

                if(debug)
                    Console.WriteLine("(" + returnValue.name + " - " + returnValue.handle + ")\n");

                if (op.settings["app_name"] != "Generic")
                {
                    Window got = (Window)returnValue;
                    windList[i] = got;
                }
            }
            Time.stopTimer("Test", "Total time");
            //Done!

        }
    }
}
