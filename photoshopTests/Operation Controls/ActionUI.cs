using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace photoshopTests
{
    public partial class ActionUI : Form
    {
        Dictionary<string, string> settings;
        public ActionUI(Dictionary<string, string> set)
        {
            InitializeComponent();
            settings = set;
            action_name.Text = settings["action_name"];
            action_set.Text = settings["action_set"];
        }

        private void action_set_TextChanged(object sender, EventArgs e)
        {
            settings["action_set"] = action_set.Text;
        }

        private void action_name_TextChanged(object sender, EventArgs e)
        {
            settings["action_name"] = action_name.Text;
        }
    }
}
