using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using USBTCommon;

namespace excelTests
{
    [KnownType(typeof(Restore))]
    [DataContract]
    [Serializable]
    class Restore : IOperation
    {
        [DataMember]
        public Dictionary<string, string> settings { get; set; }
        [DataMember]
        public Dictionary<string, bool> timers { get; set; }

        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);

        public Restore()
        {
            settings = new Dictionary<string, string>();
            timers = new Dictionary<string, bool>();

            settings.Add("op_name", "Restore");
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

            au3.Opt("WinTitleMatchMode", 4);
            au3.WinSetState("[HANDLE:" + hWnd.handle + "]", "", au3.SW_RESTORE);
            
            return hWnd;
        }
    }
}
