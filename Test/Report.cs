using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Windows.Documents;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Controls;
using USBTCommon;

namespace Test
{
    public partial class Report : Form
    {
        //Dictionary<string, Dictionary<string, Stopwatch>> times;
        TextWriter _writer = null;

        public Report()
        {
            //times = Time.timeMaster;
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            // Instantiate the writer
            _writer = new TextBoxStreamWriter(textOutput);
            // Redirect the out Console stream
            Console.SetOut(_writer);
            textOutput.SelectionFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
        }

        public void pushText(string str)
        {
            textOutput.SelectedText = str;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
