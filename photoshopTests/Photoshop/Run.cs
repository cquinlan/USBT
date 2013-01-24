using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using ps = Photoshop;
using USBTCommon;

namespace photoshopTests
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

        //Constructor
        public Run()
        {
            settings = new Dictionary<string, string>();
            timers = new Dictionary<string, bool>();

            //Primary settings
            settings.Add("op_name", "Run");
            settings.Add("flags", "start_app");
            settings.Add("required", ""); //Operation settings that must have valid entries

            //Generic app information (path, hash, etc.)
            settings.Add("app_name", Data.app);
            settings.Add("app_path", Data.path);
            settings.Add("ref_hash", Data.hash);
            settings.Add("cur_hash", Data.hash);
        }

        //Required by IOperation interface

        public void checkOp()
        {
            Console.WriteLine(this.ToString());
        }

        public Window runOp(Window hWnd)
        {
            AutoItX3Lib.AutoItX3Class au3 = new AutoItX3Lib.AutoItX3Class();

            string path = settings["app_path"];
            string[] str = path.Split('\\');
            string name = settings["app_name"] + ":" + settings["app_instance"];

            Process appProc = Process.Start(path);
            ps.ApplicationClass app = new ps.ApplicationClass();

            Time.startTimer(name, "launch to ready");
            Time.startTimer(name, "runtime");

            WaitForRedraw(app);
            while (true)
            {
                if (au3.WinExists("Adobe Photoshop", "").Equals(1)) { break; }
            }

            string handle = au3.WinGetHandle("Adobe Photoshop", "");

            Time.stopTimer(name, "launch to ready");

            hWnd.handle = handle;
            hWnd.myProc = appProc;
            return hWnd;
        }
        public void WaitForRedraw(ps.ApplicationClass app)
        {
            var eventWait = app.CharIDToTypeID("Wait");
            var enumRedrawComplete = app.CharIDToTypeID("RdCm");
            var typeState = app.CharIDToTypeID("Stte");
            var keyState = app.CharIDToTypeID("Stte");
            var desc = new ps.ActionDescriptor();
            desc.PutEnumerated(keyState, typeState, enumRedrawComplete);
            app.ExecuteAction(eventWait, desc, ps.PsDialogModes.psDisplayNoDialogs);
        }
    }
}
