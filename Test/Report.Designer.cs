namespace Test
{
    partial class Report
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
            this.textOutput = new System.Windows.Forms.RichTextBox();
            this.save = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textOutput
            // 
            this.textOutput.BackColor = System.Drawing.SystemColors.MenuText;
            this.textOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textOutput.ForeColor = System.Drawing.SystemColors.Window;
            this.textOutput.Location = new System.Drawing.Point(10, 13);
            this.textOutput.Name = "textOutput";
            this.textOutput.ReadOnly = true;
            this.textOutput.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textOutput.Size = new System.Drawing.Size(564, 462);
            this.textOutput.TabIndex = 7;
            this.textOutput.Text = "";
            // 
            // save
            // 
            this.save.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.save.Location = new System.Drawing.Point(10, 475);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(564, 23);
            this.save.TabIndex = 9;
            this.save.Text = "Save";
            this.save.UseVisualStyleBackColor = true;
            // 
            // close
            // 
            this.close.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.close.Location = new System.Drawing.Point(10, 498);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(564, 23);
            this.close.TabIndex = 8;
            this.close.Text = "Close";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 531);
            this.Controls.Add(this.textOutput);
            this.Controls.Add(this.save);
            this.Controls.Add(this.close);
            this.Name = "Report";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Report";
            this.Load += new System.EventHandler(this.Report_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox textOutput;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button close;


    }
}