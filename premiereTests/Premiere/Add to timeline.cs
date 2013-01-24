﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using USBTCommon;

namespace premiereTests
{
    [KnownType(typeof(AddToTimeline))]
    [DataContract]
    [Serializable]
    class AddToTimeline : IOperation
    {
        [DataMember]
        public Dictionary<string, string> settings { get; set; }
        [DataMember]
        public Dictionary<string, bool> timers { get; set; }

        public AddToTimeline()
        {
            settings = new Dictionary<string, string>();
            timers = new Dictionary<string, bool>();

            settings.Add("op_name", "Add to timeline");
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

            au3.Opt("WinTitleMatchMode", 2);
            au3.Opt("SendKeyDelay", 20);
            au3.Send("^i", 0);
            while (true)
            {
                if (au3.WinExists("Import", "") != 0) { break; }
            }
            au3.Sleep(1000);
            au3.Send(doc, 0);
            au3.Send("{ENTER}", 0);
            Time.startTimer(name, "Import");

            while (true)
            {
                if (au3.WinExists("Import Files", "") == 0) { break; }
            }

            Time.stopTimer(name, "Import");
            return hWnd;
        }
    }
}
