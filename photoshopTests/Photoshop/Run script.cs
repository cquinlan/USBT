using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ps = Photoshop;
using USBTCommon;

namespace photoshopTests
{
    [KnownType(typeof(RunScript))]
    [DataContract]
    [Serializable]
    class RunScript : IOperation
    {
        [DataMember]
        public Dictionary<string, string> settings { get; set; }
        [DataMember]
        public Dictionary<string, bool> timers { get; set; }

        public RunScript()
        {
            settings = new Dictionary<string, string>();
            timers = new Dictionary<string, bool>();

            //Primary settings
            settings.Add("op_name", "Run Script (JS)");
            settings.Add("flags","settings_ui");
            settings.Add("required", "input_text,input_type"); //Operation settings that must have valid entries

            //Input settings
            settings.Add("input", ""); //This is a filepath. Ignore if unused.
            settings.Add("input_type", "Javascript"); //The type of input
            settings.Add("input_text", ""); //The content of the input
            settings.Add("input_name", ""); //The name of the input ie. script name

            //Generic app information (path, hash, etc.)
            settings.Add("app_name", Data.app);
            settings.Add("app_path", Data.path);
            settings.Add("ref_hash", Data.hash);
            settings.Add("cur_hash", Data.hash);
        }

        public void checkOp()
        {
            new runScriptUI(settings).Show();
        }

        public Window runOp(Window hWnd)
        {
            AutoItX3Lib.AutoItX3Class au3 = new AutoItX3Lib.AutoItX3Class();

            string doc = settings["input"];
            string name = settings["app_name"] + ":" + settings["app_instance"];

            ps.ApplicationClass app = new ps.ApplicationClass();
            app.DisplayDialogs = ps.PsDialogModes.psDisplayNoDialogs; //Do not display dialog boxes
            string[] arr = new string[0];
            Time.startTimer(name, "script");
            app.DoJavaScript(settings["input_text"],arr,1);
            WaitForRedraw(app); //Pauses while Photoshop refreshes. Unsure if this works as desired.
            Time.stopTimer(name, "script");




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
