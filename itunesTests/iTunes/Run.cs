using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using USBTCommon;

namespace itunesTests
{
    [KnownType(typeof(Run))]
    [DataContract]
    [Serializable]
    class Run : IOperation
    {
        [DataMember]
        public Dictionary<string, string> settings { get; set; }
        [DataMember]
        public Dictionary<string, bool> timers { get; set; }

        public Run()
        {
            settings = new Dictionary<string, string>();
            timers = new Dictionary<string, bool>();

            settings.Add("op_name", "Run");
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

            Process appProc = Process.Start(path,"");

            Time.startTimer(name, "launch to ready");
            Time.startTimer(name, "runtime");

            appProc.WaitForInputIdle();

            string handle = appProc.MainWindowHandle.ToString();

            au3.Opt("WinTitleMatchMode", 2);
            while (true)
            {
                au3.WinActivate("iTunes", "");
                if (au3.WinActive("iTunes", "").Equals(1)) { break; }
            }
            
            while (true)
            {
                au3.Send("^u", 0);
                au3.WinActivate("Open Stream", "");
                if (au3.WinActive("Open Stream", "").Equals(1)) { break; }
            }

            au3.Send("{ESC}",0);

            appProc.WaitForInputIdle();

            handle = au3.WinGetHandle("iTunes", "");

            Console.WriteLine("Passed WaitActive");

            Time.stopTimer(name, "launch to ready");
            hWnd.handle = handle;
            hWnd.myProc = appProc;
            return hWnd;
        }
    }
}
