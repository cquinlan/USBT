using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using System.Text;

namespace USBTCommon
{
    //Behold! My mastery of the temporal plane.
    //Here be timelords.
    public class Time
    {
        //Dictionary of Applications with a value of a timer dictionary.
        private static Dictionary<string, Dictionary<string, Stopwatch>> timeMaster 
            = new Dictionary<string, Dictionary<string, Stopwatch>>();

        public static void startTimer(string app_name ,string timer_name)
        {
            Stopwatch timer = new Stopwatch();
            if(!timeMaster.Keys.Contains(app_name))
            {
                timeMaster.Add(app_name, new Dictionary<string,Stopwatch>());
            }
            timeMaster[app_name].Add(timer_name, timer);
            timer.Start();
        }

        public static void stopTimer(string app_name, string timer_name)
        {
            Stopwatch timer = timeMaster[app_name][timer_name];
            timer.Stop();
        }

        public static void clearTimers()
        {
            timeMaster.Clear();
        }

        public static Dictionary<string, Dictionary<string, Stopwatch>> getTimers()
        {
            return timeMaster;
        }

        public static void printTimers()
        {
            //Debug for now. Outputs applications and their timers.
            Console.WriteLine("[" + DateTime.UtcNow.ToLocalTime().ToShortTimeString() + "] USBT (Build [80] Noble Reflex)\n\n");

            for (int i=0;i<timeMaster.Count;i++)
            {
                Dictionary<string,Stopwatch> child = timeMaster.Values.ElementAt(i);
                Console.WriteLine("[Entry] " + timeMaster.Keys.ElementAt(i));
                for (int x=0;x<child.Count;x++)
                {
                    Stopwatch timer = child.Values.ElementAt(x);
                    string name = child.Keys.ElementAt(x);
                    double convert = Convert.ToDouble((timer.ElapsedMilliseconds));
                    Console.WriteLine(name+": "+convert.ToString());
                }
            }
        }
    }
}
