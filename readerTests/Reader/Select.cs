using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using USBTCommon;

namespace readerTests
{
    [KnownType(typeof(Select))]
    [DataContract]
    [Serializable]
    class Select : IOperation
    {
        [DataMember]
        public Dictionary<string, string> settings { get; set; }
        [DataMember]
        public Dictionary<string, bool> timers { get; set; }

        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);

        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);	

        public Select()
        {
            settings = new Dictionary<string, string>();
            timers = new Dictionary<string, bool>();

            settings.Add("op_name", "Select");
            settings.Add("app_name", Data.app);
            settings.Add("app_path", Data.path);
            settings.Add("ref_hash", Data.hash);
            settings.Add("cur_hash", Data.hash);
        }

        public void checkOp()
        {
            Console.WriteLine(this.ToString());
        }

        public Window runOp(Window hWnd)
        {
            AutoItX3Lib.AutoItX3Class au3 = new AutoItX3Lib.AutoItX3Class();

            //SetForegroundWindow(Convert.ToInt32(hWnd.handle));
            au3.Opt("WinTitleMatchMode", 4);
            au3.WinActivate("[HANDLE:" + hWnd.handle + "]", "");

            //while (true)
            //{
            //    au3.WinActivate("[HANDLE:" + hWnd.handle + "]", "");
            //    if (au3.WinActive("[HANDLE:" + hWnd.handle + "]", "") != 0) { break; }
            //}

            //hWnd.handle = au3.WinGetHandle(, "");
            //hWnd.title = au3.WinGetTitle(title, "");
            return hWnd;
        }
    }
}
