using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using USBTCommon;

namespace excelTests
{
    [KnownType(typeof(Close))]
    [DataContract]
    [Serializable]
    class Close : IOperation
    {
        [DataMember]
        public Dictionary<string, string> settings { get; set; }
        [DataMember]
        public Dictionary<string, bool> timers { get; set; }

        public Close()
        {
            settings = new Dictionary<string, string>();
            timers = new Dictionary<string, bool>();

            settings.Add("op_name", "Close");
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
            string name = settings["app_name"] + ":" + settings["app_instance"];


            au3.Opt("WinTitleMatchMode", 4);
            au3.WinActivate("[HANDLE:" + hWnd.handle + "]", "");
            au3.WinClose("[HANDLE:" + hWnd.handle + "]", "");
            hWnd.myProc.CloseMainWindow();
            Time.stopTimer(name, "runtime");
            return hWnd;
        }
    }
}
