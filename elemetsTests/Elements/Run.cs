using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using USBTCommon;

namespace elementsTests
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
            au3.Opt("WinTitleMatchMode", 2);

            string path = settings["app_path"];
            string name = settings["app_name"];
            string[] str = path.Split('\\');

            Process appProc = Process.Start(path,"");

            Time.startTimer(name, "launch to ready");
            Time.startTimer(name, "runtime");

            appProc.WaitForInputIdle();

            while (true)
            {
                if (au3.WinActive("Organizer", "").Equals(1)) { break; }
            }

            au3.WinActivate("Organizer", "");

            checkKeystroke();
            string handle = appProc.MainWindowHandle.ToString();

            Time.stopTimer(name, "launch to ready");
            
            hWnd.handle = handle;
            hWnd.myProc = appProc;
            return hWnd;
        }

        public bool checkKeystroke()
        {
            AutoItX3Lib.AutoItX3Class au3 = new AutoItX3Lib.AutoItX3Class();
            au3.Send("^e", 0);
            while(true)
            {
                au3.WinActivate("All Displayed Items Chosen","");
                if (au3.WinActive("All Displayed Items Chosen", "").Equals(1))
                {
                    au3.Send("{e up}{CTRLUP}", 0);
                    au3.Send("{ESC 2}", 0);
                    au3.WinClose("All Displayed Items Chosen", "");
                    return true;
                }
            }
        }            
    }
}
