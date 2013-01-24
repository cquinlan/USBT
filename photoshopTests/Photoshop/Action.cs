using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ps = Photoshop;
using USBTCommon;

namespace photoshopTests
{
    [KnownType(typeof(Action))]
    [DataContract]
    [Serializable]
    class Action : IOperation
    {
        [DataMember]
        public Dictionary<string, string> settings { get; set; }
        [DataMember]
        public Dictionary<string, bool> timers { get; set; }

        public Action()
        {
            settings = new Dictionary<string, string>();
            timers = new Dictionary<string, bool>();

            //Primary settings
            settings.Add("op_name", "Action");
            settings.Add("flags", "");
            settings.Add("required", "action_name,action_set");

            //Input settings
            settings.Add("action_name", "");
            settings.Add("action_set", "");

            //Generic app information (path, hash, etc.)
            settings.Add("app_name", Data.app);
            settings.Add("app_path", Data.path);
            settings.Add("ref_hash", Data.hash);
            settings.Add("cur_hash", Data.hash);
        }

        public void checkOp()
        {
            new ActionUI(settings).Show();
        }

        public Window runOp(Window hWnd)
        {
            AutoItX3Lib.AutoItX3Class au3 = new AutoItX3Lib.AutoItX3Class();

            string name = settings["app_name"] + ":" + settings["app_instance"];
            string act_name = settings["action_name"];

            ps.ApplicationClass app = new ps.ApplicationClass();
            app.DisplayDialogs = ps.PsDialogModes.psDisplayNoDialogs; //Do not display dialog boxes
            Time.startTimer(name, "action " + act_name);
            app.DoAction(act_name, settings["action_set"]); //Do action of name act_name in the set given.
            Time.stopTimer(name, "action " + act_name);
            return hWnd;
        }
    }
}
