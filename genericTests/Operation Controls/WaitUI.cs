using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace genericTests
{
    public partial class WaitUI : Form
    {
        Dictionary<string, string> settings;
        TimeSpan time;
        public WaitUI(Dictionary<string,string> set)
        {
            InitializeComponent();
            settings = set;
            time = TimeSpan.FromMilliseconds(Convert.ToDouble(settings["time"]));
            display();
        }

        private void mill_ValueChanged(object sender, EventArgs e)
        {
            update();
        }

        private void sec_ValueChanged(object sender, EventArgs e)
        {
            update();
        }

        private void min_ValueChanged(object sender, EventArgs e)
        {
            update();
        }

        private void display()
        {
            //Have to make seperate ints to avoid the weird TimeSpan truncate bug.
            //Feeing time.Minutes directly into min.Value truncated the rest of the values
            //from the TimeSpan.
            int mins = time.Minutes;
            int secs = time.Seconds;
            int mills = time.Milliseconds;
            min.Value = mins;
            sec.Value = secs;
            mill.Value = mills;
        }

        private void update()
        {
            time = new TimeSpan(0, 0, Convert.ToInt32(min.Value),
                Convert.ToInt32(sec.Value), Convert.ToInt32(mill.Value));
            settings["time"] = time.TotalMilliseconds.ToString();
        }
    }
}
