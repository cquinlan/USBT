using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ps = Photoshop;
using USBTCommon;

namespace photoshopTests
{
    [KnownType(typeof(ZoomOut))]
    [DataContract]
    [Serializable]
    class ZoomOut : IOperation
    {
        [DataMember]
        public Dictionary<string, string> settings { get; set; }
        [DataMember]
        public Dictionary<string, bool> timers { get; set; }

        public ZoomOut()
        {
            settings = new Dictionary<string, string>();
            timers = new Dictionary<string, bool>();

            //Primary settings
            settings.Add("op_name", "Zoom out");
            settings.Add("flags", "");
            settings.Add("required", "");

            //Generic app information (path, hash, etc.)
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

            au3.Send("^-",0);
            au3.Sleep(200);
            return hWnd;
        }
    }
}
