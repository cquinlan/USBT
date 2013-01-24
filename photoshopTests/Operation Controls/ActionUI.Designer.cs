namespace photoshopTests
{
    partial class ActionUI
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
            this.action_name = new System.Windows.Forms.TextBox();
            this.action_set = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // action_name
            // 
            this.action_name.Location = new System.Drawing.Point(12, 66);
            this.action_name.Name = "action_name";
            this.action_name.Size = new System.Drawing.Size(208, 20);
            this.action_name.TabIndex = 0;
            this.action_name.TextChanged += new System.EventHandler(this.action_name_TextChanged);
            // 
            // action_set
            // 
            this.action_set.Location = new System.Drawing.Point(12, 27);
            this.action_set.Name = "action_set";
            this.action_set.Size = new System.Drawing.Size(208, 20);
            this.action_set.TabIndex = 1;
            this.action_set.TextChanged += new System.EventHandler(this.action_set_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Action set:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Action name:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(226, 9);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(196, 78);
            this.textBox1.TabIndex = 4;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "Choose the action set and name from the Photoshop Action Palette. (F9)";
            // 
            // ActionUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 99);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.action_set);
            this.Controls.Add(this.action_name);
            this.MaximizeBox = false;
            this.Name = "ActionUI";
            this.Text = "Action";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox action_name;
        private System.Windows.Forms.TextBox action_set;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
    }
}