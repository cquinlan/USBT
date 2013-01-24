using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ps = Photoshop;
using USBTCommon;

namespace photoshopTests
{
    [KnownType(typeof(Open))]
    [DataContract]
    [Serializable]
    class Open : IOperation
    {
        [DataMember]
        public Dictionary<string, string> settings { get; set; }
        [DataMember]
        public Dictionary<string, bool> timers { get; set; }

        public Open()
        {
            settings = new Dictionary<string, string>();
            timers = new Dictionary<string, bool>();

            //Primary settings
            settings.Add("op_name", "Open");
            settings.Add("flags", "");
            settings.Add("required", "open");

            //Input settings
            settings.Add("open", "");

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

            string doc = settings["open"];
            string name = settings["app_name"] + ":" + settings["app_instance"];

            ps.ApplicationClass app = new ps.ApplicationClass();
            app.DisplayDialogs = ps.PsDialogModes.psDisplayNoDialogs; //Do not display dialog boxes
            app.Application.Load(doc);
            Time.startTimer(name, "open");

            WaitForRedraw(app); //Pauses while Photoshop refreshes. Unsure if this works as desired.
            
            Time.stopTimer(name, "open");
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
