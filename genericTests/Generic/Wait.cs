using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using USBTCommon;

namespace genericTests
{
    [KnownType(typeof(Wait))]
    [DataContract]
    [Serializable]
    class Wait : IOperation
    {
        [DataMember]
        public Dictionary<string, string> settings { get; set; }
        [DataMember]
        public Dictionary<string, bool> timers { get; set; }

        public Wait()
        {
            settings = new Dictionary<string, string>();
            timers = new Dictionary<string, bool>();

            //Primary settings
            settings.Add("op_name", "Wait");
            settings.Add("op_desc", "Wait for specified milliseconds");
            settings.Add("flags", "generic,settings_ui");
            settings.Add("required", "time");

            //Input settings
            settings.Add("time", "0");

            //Generic app information (path, hash, etc.)
            settings.Add("app_name", Data.app);
            settings.Add("app_path", Data.path);
        }

        public void checkOp()
        {
            new WaitUI(settings).Show();
        }

        public Window runOp(Window hWnd)
        {
            AutoItX3Lib.AutoItX3Class au3 = new AutoItX3Lib.AutoItX3Class();

            au3.Sleep(Convert.ToInt32(settings["time"]));

            return hWnd;
        }
    }
}
