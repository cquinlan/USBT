using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using USBTCommon;

namespace genericTests
{
    [KnownType(typeof(Minimize))]
    [DataContract]
    [Serializable]
    class Minimize : IOperation
    {
        [DataMember]
        public Dictionary<string, string> settings { get; set; }
        [DataMember]
        public Dictionary<string, bool> timers { get; set; }

        public Minimize()
        {
            settings = new Dictionary<string, string>();
            timers = new Dictionary<string, bool>();

            settings.Add("op_name", "Minimize");
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

            return hWnd;
        }
    }
}
