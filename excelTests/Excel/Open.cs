using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using USBTCommon;

namespace excelTests
{
    [KnownType(typeof(Open))]
    [DataContract]
    [Serializable]
    class Open : IOperation
    {
        [DataMember]
        public Dictionary<string, string> settings { get; set; }
        [DataMember]
        public Dictionary<string, bool> timers { get; set; }

        public Open()
        {
            settings = new Dictionary<string, string>();
            timers = new Dictionary<string, bool>();

            settings.Add("op_name", "Open");
            settings.Add("open", "");
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

            string doc = settings["open"];
            string[] str = doc.Split('\\');
            string name = settings["app_name"] + ":" + settings["app_instance"];

            string title = str[str.Length - 1];

            au3.Opt("WinTitleMatchMode", 4);
            au3.WinActivate("[HANDLE:" + hWnd.handle + "]", "");

            au3.Opt("WinTitleMatchMode", 2);
            au3.Opt("SendKeyDelay", 20);
            au3.Send("^o", 0);
            while (true)
            {
                if (au3.WinExists("Open", "") != 0) { break; }
            }
            au3.Sleep(1000);
            au3.Send(doc, 0);
            au3.Send("{ENTER}", 0);

            Time.startTimer(name, "open");

            au3.Opt("WinTitleMatchMode", 2);

            while (true)
            {
                au3.WinActivate(title, "");
                if (au3.WinActive(title, "") != 0) { break; }
            }

            Time.stopTimer(name, "open");
            hWnd.handle = au3.WinGetHandle(title, "");
            hWnd.title = au3.WinGetTitle(title, "");
            return hWnd;
        }
    }
}
