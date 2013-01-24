using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace USBTCommon
{

    /*    Windows are tagged to the window list. They represent an instance of an application.
     * 
    Windows have three important members.
     - handle (the window handle given by Windows on creation)
     - instance (the instance of the application it represents)
     - myProc (the process of the application it represents)   */

    public class Window
    {
        public string handle;
        public string title;
        public string name;
        public Process myProc;
        public int instance;

        public Window()
        {
            handle = "";
            title = "";
            name = "";
            myProc = new Process();
            instance = 0;
        }
        public Window(string hWnd, string tWnd, string aWnd, int iWnd)
        {
            handle = hWnd;
            title = tWnd;
            name = aWnd;
            instance = iWnd;
        }
    }
}
