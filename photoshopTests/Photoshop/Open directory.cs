using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ps = Photoshop;
using USBTCommon;

namespace photoshopTests
{
    [KnownType(typeof(OpenDir))]
    [DataContract]
    [Serializable]
    class OpenDir : IOperation
    {
        [DataMember]
        public Dictionary<string, string> settings { get; set; }
        [DataMember]
        public Dictionary<string, bool> timers { get; set; }

        public OpenDir()
        {
            settings = new Dictionary<string, string>();
            timers = new Dictionary<string, bool>();

            //Primary settings
            settings.Add("op_name", "Open directory");
            settings.Add("flags", "");
            settings.Add("required", "directory"); //Operation settings that must have valid entries

            //Input settings
            settings.Add("directory", "");

            //Generic app information (path, hash, etc.)
            settings.Add("app_name", Data.app);
            settings.Add("app_path", Data.path);
            settings.Add("ref_hash", Data.hash);
            settings.Add("cur_hash", Data.hash);
        }

        public void checkOp()
        {
            //Console.WriteLine(this.ToString());
        }

        public Window runOp(Window hWnd)
        {
            AutoItX3Lib.AutoItX3Class au3 = new AutoItX3Lib.AutoItX3Class();

            string doc = settings["directory"];
            string name = settings["app_name"] + ":" + settings["app_instance"];

            ps.ApplicationClass app = new ps.ApplicationClass();

            app.DisplayDialogs = ps.PsDialogModes.psDisplayNoDialogs; //Do not display dialog boxes
            string[] dirFiles = Directory.GetFiles(doc); //Get a list of files in that directory
            Object[] supportedTypes = (Object[])app.WindowsFileTypes; //Get a list of supported types

            Time.startTimer(name, "openDir");

            foreach (string file in dirFiles)
            {
                string[] parse = file.Split('.');
                string ext = parse[parse.Length - 1];
                if (supportedTypes.Contains((Object)ext)) //Ensure the filetype is supported
                {
                    app.Application.Load(file);
                }
                else
                    Console.WriteLine("Could not load " + file + "\nType not supported");
            }
            //WaitForRedraw(app); //Pauses while Photoshop refreshes. Unsure if this works as desired.

            Time.stopTimer(name, "openDir");

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
