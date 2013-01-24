using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using USBTCommon;

namespace chromeTests
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

            string name = settings["app_name"];

            //au3.Opt("WinTitleMatchMode", 4);
            au3.WinActivate(hWnd.handle, "");
            au3.WinWaitActive(hWnd.handle, "", 5);
            au3.WinClose(hWnd.handle, "");
            au3.WinWaitClose(hWnd.handle, "", 5);
            if (!hWnd.myProc.HasExited)
            {
                hWnd.myProc.CloseMainWindow();
                hWnd.myProc.Kill();
            }
            Time.stopTimer(name, "runtime");
            return hWnd;
        }

    }
}
