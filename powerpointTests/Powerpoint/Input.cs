using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using USBTCommon;

namespace powerpointTests
{
    [KnownType(typeof(Input))]
    [DataContract]
    [Serializable]
    class Input : IOperation
    {
        [DataMember]
        public Dictionary<string, string> settings { get; set; }
        [DataMember]
        public Dictionary<string, bool> timers { get; set; }

        public Input()
        {
            settings = new Dictionary<string, string>();
            timers = new Dictionary<string, bool>();

            settings.Add("op_name", "Input");
            settings.Add("input", "");
            settings.Add("input_text", "");
            settings.Add("input_type", "Text");
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

            string doc = settings["input"];
            string[] str = doc.Split('\\');
            string name = settings["app_name"] + ":" + settings["app_instance"];
            string[] parsed = settings["input_text"].Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            string title = str[str.Length - 1];

            au3.Opt("WinTitleMatchMode", 4);
            au3.WinActivate("[HANDLE:" + hWnd.handle + "]", "");

            au3.Opt("WinTitleMatchMode", 2);
            au3.Opt("SendKeyDelay", 20);

            Time.startTimer(name, "input");

            foreach (string part in parsed)
            {
                au3.Send(part, 0);
                au3.Sleep(100);
                au3.Send("{ENTER}", 0);
                au3.Sleep(500);
            }
           
            Time.stopTimer(name, "input");

            hWnd.handle = au3.WinGetHandle(title, "");
            hWnd.title = au3.WinGetTitle(title, "");
            return hWnd;
        }
    }
}
