namespace Test
{
    partial class TestSettings
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
            this.opQueue = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.appDetails = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.appLegal = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cur_checksum = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ref_checksum = new System.Windows.Forms.TextBox();
            this.appIcon = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.app_path = new System.Windows.Forms.TextBox();
            this.editAppPath = new System.Windows.Forms.Button();
            this.appName = new System.Windows.Forms.TextBox();
            this.appVersion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.appDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // opQueue
            // 
            this.opQueue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.opQueue.GridLines = true;
            this.opQueue.Location = new System.Drawing.Point(14, 28);
            this.opQueue.Name = "opQueue";
            this.opQueue.Size = new System.Drawing.Size(249, 307);
            this.opQueue.TabIndex = 0;
            this.opQueue.UseCompatibleStateImageBehavior = false;
            this.opQueue.View = System.Windows.Forms.View.Details;
            this.opQueue.SelectedIndexChanged += new System.EventHandler(this.opQueue_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 90;
            // 
            // appDetails
            // 
            this.appDetails.Controls.Add(this.label3);
            this.appDetails.Controls.Add(this.label1);
            this.appDetails.Controls.Add(this.appLegal);
            this.appDetails.Controls.Add(this.label12);
            this.appDetails.Controls.Add(this.cur_checksum);
            this.appDetails.Controls.Add(this.label9);
            this.appDetails.Controls.Add(this.ref_checksum);
            this.appDetails.Controls.Add(this.appIcon);
            this.appDetails.Controls.Add(this.label5);
            this.appDetails.Controls.Add(this.label4);
            this.appDetails.Controls.Add(this.app_path);
            this.appDetails.Controls.Add(this.editAppPath);
            this.appDetails.Controls.Add(this.appName);
            this.appDetails.Controls.Add(this.appVersion);
            this.appDetails.Location = new System.Drawing.Point(270, 13);
            this.appDetails.Name = "appDetails";
            this.appDetails.Size = new System.Drawing.Size(337, 322);
            this.appDetails.TabIndex = 4;
            this.appDetails.TabStop = false;
            this.appDetails.Text = "Details";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(236, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Version:";
            // 
            // appLegal
            // 
            this.appLegal.Location = new System.Drawing.Point(6, 109);
            this.appLegal.Multiline = true;
            this.appLegal.Name = "appLegal";
            this.appLegal.ReadOnly = true;
            this.appLegal.Size = new System.Drawing.Size(318, 62);
            this.appLegal.TabIndex = 32;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 213);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 13);
            this.label12.TabIndex = 31;
            this.label12.Text = "Current";
            // 
            // cur_checksum
            // 
            this.cur_checksum.Location = new System.Drawing.Point(9, 229);
            this.cur_checksum.Name = "cur_checksum";
            this.cur_checksum.ReadOnly = true;
            this.cur_checksum.Size = new System.Drawing.Size(242, 20);
            this.cur_checksum.TabIndex = 30;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 174);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 29;
            this.label9.Text = "Reference";
            // 
            // ref_checksum
            // 
            this.ref_checksum.Location = new System.Drawing.Point(9, 190);
            this.ref_checksum.Name = "ref_checksum";
            this.ref_checksum.ReadOnly = true;
            this.ref_checksum.Size = new System.Drawing.Size(242, 20);
            this.ref_checksum.TabIndex = 28;
            // 
            // appIcon
            // 
            this.appIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.appIcon.Location = new System.Drawing.Point(6, 19);
            this.appIcon.Name = "appIcon";
            this.appIcon.Size = new System.Drawing.Size(32, 32);
            this.appIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.appIcon.TabIndex = 26;
            this.appIcon.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Description:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Path:";
            // 
            // app_path
            // 
            this.app_path.Location = new System.Drawing.Point(6, 70);
            this.app_path.Name = "app_path";
            this.app_path.ReadOnly = true;
            this.app_path.Size = new System.Drawing.Size(227, 20);
            this.app_path.TabIndex = 24;
            // 
            // editAppPath
            // 
            this.editAppPath.Location = new System.Drawing.Point(239, 68);
            this.editAppPath.Name = "editAppPath";
            this.editAppPath.Size = new System.Drawing.Size(65, 23);
            this.editAppPath.TabIndex = 23;
            this.editAppPath.Text = "Select";
            this.editAppPath.UseVisualStyleBackColor = true;
            this.editAppPath.Click += new System.EventHandler(this.editAppPath_Click);
            // 
            // appName
            // 
            this.appName.Location = new System.Drawing.Point(44, 31);
            this.appName.Name = "appName";
            this.appName.ReadOnly = true;
            this.appName.Size = new System.Drawing.Size(189, 20);
            this.appName.TabIndex = 22;
            // 
            // appVersion
            // 
            this.appVersion.Location = new System.Drawing.Point(239, 31);
            this.appVersion.Name = "appVersion";
            this.appVersion.ReadOnly = true;
            this.appVersion.Size = new System.Drawing.Size(92, 20);
            this.appVersion.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Select group";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Legal:";
            // 
            // TestSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 344);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.appDetails);
            this.Controls.Add(this.opQueue);
            this.Name = "TestSettings";
            this.Text = "Operation Settings";
            this.Load += new System.EventHandler(this.TestSettings_Load);
            this.appDetails.ResumeLayout(false);
            this.appDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView opQueue;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.GroupBox appDetails;
        private System.Windows.Forms.PictureBox appIcon;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox app_path;
        private System.Windows.Forms.Button editAppPath;
        private System.Windows.Forms.TextBox appName;
        private System.Windows.Forms.TextBox appVersion;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox cur_checksum;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox ref_checksum;
        private System.Windows.Forms.TextBox appLegal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}