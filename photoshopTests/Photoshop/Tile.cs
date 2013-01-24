using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ps = Photoshop;
using USBTCommon;

namespace photoshopTests
{
    [KnownType(typeof(Tile))]
    [DataContract]
    [Serializable]
    class Tile : IOperation
    {
        [DataMember]
        public Dictionary<string, string> settings { get; set; }
        [DataMember]
        public Dictionary<string, bool> timers { get; set; }

        public Tile()
        {
            settings = new Dictionary<string, string>();
            timers = new Dictionary<string, bool>();

            //Primary settings
            settings.Add("op_name", "Tile");
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

            ps.ApplicationClass app = new ps.ApplicationClass();
            app.DisplayDialogs = ps.PsDialogModes.psDisplayNoDialogs; //Do not display dialog boxes

            au3.Send("!w",0);
            au3.Send("a", 0);
            au3.Send("t", 0);

            Time.startTimer(name, "tile");
            WaitForRedraw(app); //Pauses while Photoshop refreshes. Unsure if this works as desired.
            Time.stopTimer(name, "tile");
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
