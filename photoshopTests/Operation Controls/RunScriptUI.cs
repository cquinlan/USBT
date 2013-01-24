using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace photoshopTests
{
    public partial class runScriptUI : Form
    {
        Dictionary<string, string> settings;
        public runScriptUI(Dictionary<string,string> set)
        {
            InitializeComponent();
            settings = set;
            input_text.Text = settings["input_text"];
            inputLanguageSelect.Text = settings["input_type"];
            inputName.Text = settings["input_name"];
            input_text.SetHighlighting("JavaScript");
            input_text.TextEditorProperties.EnableFolding = true;
        }

        private void runScriptUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (input_text.Text != settings["input_text"] || inputName.Text != settings["input_name"]) //Not our original text?
            {
                DialogResult result = MessageBox.Show(
                    "Would you like to save your changes?",
                    "Closing",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Exclamation);

                if (result == DialogResult.Yes)
                {
                    //Save changes
                    settings["input_text"] = input_text.Text;
                    settings["input_name"] = inputName.Text;
                }
                if (result == DialogResult.Cancel)
                {
                    //Stop the form close
                    e.Cancel = true;
                }
            }
        }

        private void saveDoc_Click(object sender, EventArgs e)
        {
            settings["input_text"] = input_text.Text;
            settings["input_name"] = inputName.Text;
        }
    }
}
