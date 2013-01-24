using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using iTunesLib;
using USBTCommon;

namespace itunesTests
{
    [KnownType(typeof(Control))]
    [DataContract]
    [Serializable]
    class Control : IOperation
    {
        [DataMember]
        public Dictionary<string, string> settings { get; set; }
        [DataMember]
        public Dictionary<string, bool> timers { get; set; }

        public Control()
        {
            settings = new Dictionary<string, string>();
            timers = new Dictionary<string, bool>();

            settings.Add("op_name", "Control");
            settings.Add("input", "");
            settings.Add("input_text", "");
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
            iTunesLib.iTunesAppClass itunes = new iTunesLib.iTunesAppClass();

            string path = settings["app_path"];
            string name = settings["app_name"];
            string nput = settings["input_text"];

            switch (nput)
            {
                case "play":
                    itunes.Play();
                    break;
                case "pause":
                    itunes.Pause();
                    break;
                case "stop":
                    itunes.Stop();
                    break;
                case "toggle":
                    itunes.PlayPause();
                    break;
                case "forward":
                    itunes.NextTrack();
                    break;
                case "backward":
                    itunes.PreviousTrack();
                    break;
            }
            //Release the iTunes COM to avoid conflict when closing later
            Marshal.ReleaseComObject(itunes);

            //Set the object to null, leaving it marked for deletion by GC
            itunes = null;
            GC.Collect();

            return hWnd;
        }
    }
}
