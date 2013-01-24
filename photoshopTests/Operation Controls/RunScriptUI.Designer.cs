namespace photoshopTests
{
    partial class runScriptUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.inputMenuStrip = new System.Windows.Forms.ToolStrip();
            this.newDoc = new System.Windows.Forms.ToolStripButton();
            this.openDoc = new System.Windows.Forms.ToolStripButton();
            this.saveDoc = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.inputLanguageSelect = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.inputName = new System.Windows.Forms.ToolStripTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.input_text = new ICSharpCode.TextEditor.TextEditorControl();
            this.inputMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputMenuStrip
            // 
            this.inputMenuStrip.BackgroundImage = global::photoshopTests.Properties.Resources.gradient_67D58;
            this.inputMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDoc,
            this.openDoc,
            this.saveDoc,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.inputLanguageSelect,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.inputName});
            this.inputMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.inputMenuStrip.Name = "inputMenuStrip";
            this.inputMenuStrip.Size = new System.Drawing.Size(662, 27);
            this.inputMenuStrip.TabIndex = 29;
            this.inputMenuStrip.Text = "toolStrip1";
            // 
            // newDoc
            // 
            this.newDoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newDoc.Image = global::photoshopTests.Properties.Resources.NewDocumentHS;
            this.newDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newDoc.Name = "newDoc";
            this.newDoc.Size = new System.Drawing.Size(23, 24);
            this.newDoc.Text = "toolStripButton1";
            // 
            // openDoc
            // 
            this.openDoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openDoc.Image = global::photoshopTests.Properties.Resources.OpenFolder;
            this.openDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openDoc.Name = "openDoc";
            this.openDoc.Size = new System.Drawing.Size(23, 24);
            this.openDoc.Text = "toolStripButton2";
            // 
            // saveDoc
            // 
            this.saveDoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveDoc.Image = global::photoshopTests.Properties.Resources.Save;
            this.saveDoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveDoc.Name = "saveDoc";
            this.saveDoc.Size = new System.Drawing.Size(23, 24);
            this.saveDoc.Text = "toolStripButton3";
            this.saveDoc.Click += new System.EventHandler(this.saveDoc_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(64, 24);
            this.toolStripLabel1.Text = "Input type:";
            // 
            // inputLanguageSelect
            // 
            this.inputLanguageSelect.BackColor = System.Drawing.SystemColors.Menu;
            this.inputLanguageSelect.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.inputLanguageSelect.Items.AddRange(new object[] {
            "Actionscript",
            "Batch",
            "C#",
            "Javascript",
            "Python",
            "Text"});
            this.inputLanguageSelect.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.inputLanguageSelect.Name = "inputLanguageSelect";
            this.inputLanguageSelect.Size = new System.Drawing.Size(121, 23);
            this.inputLanguageSelect.Sorted = true;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(42, 24);
            this.toolStripLabel2.Text = "Name:";
            // 
            // inputName
            // 
            this.inputName.Name = "inputName";
            this.inputName.Size = new System.Drawing.Size(100, 27);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.input_text);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(662, 416);
            this.panel1.TabIndex = 30;
            // 
            // input_text
            // 
            this.input_text.AutoSize = true;
            this.input_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.input_text.IsReadOnly = false;
            this.input_text.Location = new System.Drawing.Point(0, 0);
            this.input_text.Name = "input_text";
            this.input_text.Size = new System.Drawing.Size(658, 412);
            this.input_text.TabIndex = 1;
            // 
            // runScriptUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 443);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.inputMenuStrip);
            this.MaximizeBox = false;
            this.Name = "runScriptUI";
            this.Text = "Run Script";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.runScriptUI_FormClosing);
            this.inputMenuStrip.ResumeLayout(false);
            this.inputMenuStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip inputMenuStrip;
        private System.Windows.Forms.ToolStripButton newDoc;
        private System.Windows.Forms.ToolStripButton openDoc;
        private System.Windows.Forms.ToolStripButton saveDoc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox inputLanguageSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox inputName;
        private System.Windows.Forms.Panel panel1;
        private ICSharpCode.TextEditor.TextEditorControl input_text;
    }
}