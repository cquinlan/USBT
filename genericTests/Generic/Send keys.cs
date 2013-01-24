using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using USBTCommon;

namespace genericTests
{
    [KnownType(typeof(Send))]
    [DataContract]
    [Serializable]
    class Send : IOperation
    {
        [DataMember]
        public Dictionary<string, string> settings { get; set; }
        [DataMember]
        public Dictionary<string, bool> timers { get; set; }

        public Send()
        {
            settings = new Dictionary<string, string>();
            timers = new Dictionary<string, bool>();

            settings.Add("op_name", "Keypress");
            settings.Add("input", "");
            settings.Add("input_text", "");
            settings.Add("app_name", Data.app);
            settings.Add("app_path", Data.path);
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
            string name = settings["app_name"];
            string[] parsed = settings["input_text"].Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            //string title = str[str.Length - 1];

            //au3.Opt("WinTitleMatchMode", 4);
            //au3.WinActivate("[HANDLE:" + hWnd.handle + "]", "");

            au3.Opt("WinTitleMatchMode", 2);
            au3.Opt("SendKeyDelay", 20);

            au3.Send(settings["input_text"], 0);

            hWnd.handle = au3.WinGetHandle("[ACTIVE]", "");
            hWnd.title = au3.WinGetTitle("[ACTIVE]", "");

            return hWnd;
        }
    }
}
