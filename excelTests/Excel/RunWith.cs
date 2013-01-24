using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using USBTCommon;

namespace excelTests
{
    [KnownType(typeof(RunWith))]
    [DataContract]
    [Serializable]
    class RunWith : IOperation
    {
        [DataMember]
        public Dictionary<string, string> settings { get; set; }
        [DataMember]
        public Dictionary<string, bool> timers { get; set; }

        public RunWith()
        {
            settings = new Dictionary<string, string>();
            timers = new Dictionary<string, bool>();

            settings.Add("op_name", "Run with");
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

            string path = settings["app_path"];
            string[] str = path.Split('\\');
            string name = settings["app_name"];

            Process appProc = Process.Start(path,Path.GetFullPath(settings["open"]));
            
            Time.startTimer(name, "launch to ready");
            Time.startTimer(name, "runtime");

            appProc.WaitForInputIdle();
            string handle = appProc.MainWindowHandle.ToString();

            Time.stopTimer(name, "launch to ready");
            hWnd.handle = handle;
            hWnd.myProc = appProc;
            return hWnd;
        }
    }
}
