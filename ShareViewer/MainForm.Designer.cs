namespace ShareViewer
{
    partial class MainForm
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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelBusyDownload = new System.Windows.Forms.Label();
            this.buttonNewShareList = new System.Windows.Forms.Button();
            this.buttonLogfile = new System.Windows.Forms.Button();
            this.buttonExplorer = new System.Windows.Forms.Button();
            this.progressBarDownload = new System.Windows.Forms.ProgressBar();
            this.buttonDayDataDownload = new System.Windows.Forms.Button();
            this.listBoxShareList = new System.Windows.Forms.ListBox();
            this.listBoxInhalt = new System.Windows.Forms.ListBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.stripText = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxSource = new System.Windows.Forms.GroupBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButtonSource = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.daysBack = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.calendarTo = new System.Windows.Forms.MonthCalendar();
            this.calendarFrom = new System.Windows.Forms.MonthCalendar();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControlMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxSource.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.daysBack)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPage1);
            this.tabControlMain.Controls.Add(this.tabPage2);
            this.tabControlMain.Controls.Add(this.tabPage3);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(784, 761);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.statusStrip);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(776, 735);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Importation";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.labelBusyDownload);
            this.panel2.Controls.Add(this.buttonNewShareList);
            this.panel2.Controls.Add(this.buttonLogfile);
            this.panel2.Controls.Add(this.buttonExplorer);
            this.panel2.Controls.Add(this.progressBarDownload);
            this.panel2.Controls.Add(this.buttonDayDataDownload);
            this.panel2.Controls.Add(this.listBoxShareList);
            this.panel2.Controls.Add(this.listBoxInhalt);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 218);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(770, 492);
            this.panel2.TabIndex = 4;
            // 
            // labelBusyDownload
            // 
            this.labelBusyDownload.AutoSize = true;
            this.labelBusyDownload.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelBusyDownload.Location = new System.Drawing.Point(308, 67);
            this.labelBusyDownload.Name = "labelBusyDownload";
            this.labelBusyDownload.Size = new System.Drawing.Size(115, 13);
            this.labelBusyDownload.TabIndex = 11;
            this.labelBusyDownload.Text = "downloads remaining...";
            this.labelBusyDownload.Visible = false;
            // 
            // buttonNewShareList
            // 
            this.buttonNewShareList.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonNewShareList.Enabled = false;
            this.buttonNewShareList.Location = new System.Drawing.Point(305, 89);
            this.buttonNewShareList.Name = "buttonNewShareList";
            this.buttonNewShareList.Size = new System.Drawing.Size(183, 23);
            this.buttonNewShareList.TabIndex = 11;
            this.buttonNewShareList.Text = "New Share List";
            this.buttonNewShareList.UseVisualStyleBackColor = true;
            this.buttonNewShareList.Click += new System.EventHandler(this.onNewShareListBtn);
            // 
            // buttonLogfile
            // 
            this.buttonLogfile.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonLogfile.Location = new System.Drawing.Point(299, 444);
            this.buttonLogfile.Name = "buttonLogfile";
            this.buttonLogfile.Size = new System.Drawing.Size(195, 23);
            this.buttonLogfile.TabIndex = 12;
            this.buttonLogfile.Text = "Open Logfile Folder";
            this.buttonLogfile.UseVisualStyleBackColor = true;
            this.buttonLogfile.Click += new System.EventHandler(this.OnOpenLogfileFolderButton);
            // 
            // buttonExplorer
            // 
            this.buttonExplorer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonExplorer.Location = new System.Drawing.Point(299, 467);
            this.buttonExplorer.Name = "buttonExplorer";
            this.buttonExplorer.Size = new System.Drawing.Size(195, 23);
            this.buttonExplorer.TabIndex = 13;
            this.buttonExplorer.Text = "Open Extra Folder";
            this.buttonExplorer.UseVisualStyleBackColor = true;
            this.buttonExplorer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnOpenExplorer);
            // 
            // progressBarDownload
            // 
            this.progressBarDownload.Location = new System.Drawing.Point(305, 41);
            this.progressBarDownload.Name = "progressBarDownload";
            this.progressBarDownload.Size = new System.Drawing.Size(183, 23);
            this.progressBarDownload.TabIndex = 3;
            this.progressBarDownload.Visible = false;
            // 
            // buttonDayDataDownload
            // 
            this.buttonDayDataDownload.Enabled = false;
            this.buttonDayDataDownload.Location = new System.Drawing.Point(305, 12);
            this.buttonDayDataDownload.Name = "buttonDayDataDownload";
            this.buttonDayDataDownload.Size = new System.Drawing.Size(183, 23);
            this.buttonDayDataDownload.TabIndex = 10;
            this.buttonDayDataDownload.Text = "Download data files";
            this.buttonDayDataDownload.UseVisualStyleBackColor = true;
            this.buttonDayDataDownload.Click += new System.EventHandler(this.DownloadDayDataBtnClicked);
            // 
            // listBoxShareList
            // 
            this.listBoxShareList.DisplayMember = "Name";
            this.listBoxShareList.Dock = System.Windows.Forms.DockStyle.Right;
            this.listBoxShareList.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxShareList.FormattingEnabled = true;
            this.listBoxShareList.ItemHeight = 11;
            this.listBoxShareList.Location = new System.Drawing.Point(494, 0);
            this.listBoxShareList.Name = "listBoxShareList";
            this.listBoxShareList.ScrollAlwaysVisible = true;
            this.listBoxShareList.Size = new System.Drawing.Size(274, 490);
            this.listBoxShareList.Sorted = true;
            this.listBoxShareList.TabIndex = 14;
            this.listBoxShareList.ValueMember = "Number";
            // 
            // listBoxInhalt
            // 
            this.listBoxInhalt.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBoxInhalt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxInhalt.FormattingEnabled = true;
            this.listBoxInhalt.Location = new System.Drawing.Point(0, 0);
            this.listBoxInhalt.Name = "listBoxInhalt";
            this.listBoxInhalt.ScrollAlwaysVisible = true;
            this.listBoxInhalt.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxInhalt.Size = new System.Drawing.Size(299, 490);
            this.listBoxInhalt.TabIndex = 9;
            this.listBoxInhalt.Click += new System.EventHandler(this.OnInhaltClicked);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripText});
            this.statusStrip.Location = new System.Drawing.Point(3, 710);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(770, 22);
            this.statusStrip.TabIndex = 3;
            // 
            // stripText
            // 
            this.stripText.Name = "stripText";
            this.stripText.Size = new System.Drawing.Size(22, 17);
            this.stripText.Text = "Ok";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.groupBoxSource);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.daysBack);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.calendarTo);
            this.panel1.Controls.Add(this.calendarFrom);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(770, 215);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "✔= file already downloaded";
            // 
            // groupBoxSource
            // 
            this.groupBoxSource.Controls.Add(this.buttonLogin);
            this.groupBoxSource.Controls.Add(this.radioButton2);
            this.groupBoxSource.Controls.Add(this.radioButtonSource);
            this.groupBoxSource.Location = new System.Drawing.Point(15, 102);
            this.groupBoxSource.Name = "groupBoxSource";
            this.groupBoxSource.Size = new System.Drawing.Size(249, 87);
            this.groupBoxSource.TabIndex = 10;
            this.groupBoxSource.TabStop = false;
            this.groupBoxSource.Text = "Inhalt Source";
            // 
            // buttonLogin
            // 
            this.buttonLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLogin.Location = new System.Drawing.Point(1, 46);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(248, 27);
            this.buttonLogin.TabIndex = 5;
            this.buttonLogin.Text = "Show Inhalt for Date range";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.OnLogin);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(106, 24);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 17);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "local";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButtonSource_CheckedChanged);
            // 
            // radioButtonSource
            // 
            this.radioButtonSource.AutoSize = true;
            this.radioButtonSource.Checked = true;
            this.radioButtonSource.Location = new System.Drawing.Point(15, 24);
            this.radioButtonSource.Name = "radioButtonSource";
            this.radioButtonSource.Size = new System.Drawing.Size(60, 17);
            this.radioButtonSource.TabIndex = 3;
            this.radioButtonSource.TabStop = true;
            this.radioButtonSource.Text = "internet";
            this.radioButtonSource.UseVisualStyleBackColor = true;
            this.radioButtonSource.CheckedChanged += new System.EventHandler(this.radioButtonSource_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxPassword);
            this.groupBox2.Controls.Add(this.textBoxUsername);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(14, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(249, 59);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Credentials";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(91, 33);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(138, 20);
            this.textBoxPassword.TabIndex = 2;
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(91, 11);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(138, 20);
            this.textBoxUsername.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "user";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(689, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 20);
            this.label7.TabIndex = 9;
            this.label7.Text = "Share List";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(417, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "days back";
            // 
            // daysBack
            // 
            this.daysBack.Location = new System.Drawing.Point(366, 5);
            this.daysBack.Name = "daysBack";
            this.daysBack.Size = new System.Drawing.Size(47, 20);
            this.daysBack.TabIndex = 7;
            this.daysBack.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.daysBack.ValueChanged += new System.EventHandler(this.DaysBackChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(533, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "To Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(301, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "From Date";
            // 
            // calendarTo
            // 
            this.calendarTo.Location = new System.Drawing.Point(536, 26);
            this.calendarTo.MaxSelectionCount = 1;
            this.calendarTo.Name = "calendarTo";
            this.calendarTo.ShowToday = false;
            this.calendarTo.TabIndex = 8;
            this.calendarTo.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.ToDateChanged);
            // 
            // calendarFrom
            // 
            this.calendarFrom.Location = new System.Drawing.Point(300, 27);
            this.calendarFrom.MaxSelectionCount = 1;
            this.calendarFrom.Name = "calendarFrom";
            this.calendarFrom.ShowToday = false;
            this.calendarFrom.ShowTodayCircle = false;
            this.calendarFrom.TabIndex = 6;
            this.calendarFrom.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.FromDateChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(776, 735);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "All-table";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(776, 735);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Settings";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.tabControlMain);
            this.Name = "MainForm";
            this.Text = "ShareViewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClose);
            this.Load += new System.EventHandler(this.OnLoad);
            this.tabControlMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBoxSource.ResumeLayout(false);
            this.groupBoxSource.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.daysBack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MonthCalendar calendarTo;
        private System.Windows.Forms.MonthCalendar calendarFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown daysBack;
        private System.Windows.Forms.ToolStripStatusLabel stripText;
        public System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox listBoxInhalt;
        private System.Windows.Forms.ListBox listBoxShareList;
        private System.Windows.Forms.Button buttonDayDataDownload;
        internal System.Windows.Forms.ProgressBar progressBarDownload;
        private System.Windows.Forms.Button buttonExplorer;
        private System.Windows.Forms.Button buttonLogfile;
        private System.Windows.Forms.Button buttonNewShareList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.GroupBox groupBoxSource;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButtonSource;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelBusyDownload;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage3;
    }
}

