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
            this.tabPageImportation = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelGenNewAllTables = new System.Windows.Forms.Label();
            this.progressBarGenNewAllTables = new System.Windows.Forms.ProgressBar();
            this.buttonNewAllTables = new System.Windows.Forms.Button();
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
            this.labelDatafilesCount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxShareNumSearch = new System.Windows.Forms.TextBox();
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
            this.labelBackFrom = new System.Windows.Forms.Label();
            this.calendarTo = new System.Windows.Forms.MonthCalendar();
            this.calendarFrom = new System.Windows.Forms.MonthCalendar();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.labelBusyAllTables = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControlMain.SuspendLayout();
            this.tabPageImportation.SuspendLayout();
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
            this.tabControlMain.Controls.Add(this.tabPageImportation);
            this.tabControlMain.Controls.Add(this.tabPageSettings);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(984, 761);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageImportation
            // 
            this.tabPageImportation.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageImportation.Controls.Add(this.panel2);
            this.tabPageImportation.Controls.Add(this.statusStrip);
            this.tabPageImportation.Controls.Add(this.panel1);
            this.tabPageImportation.Location = new System.Drawing.Point(4, 22);
            this.tabPageImportation.Name = "tabPageImportation";
            this.tabPageImportation.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageImportation.Size = new System.Drawing.Size(976, 735);
            this.tabPageImportation.TabIndex = 0;
            this.tabPageImportation.Text = "Importation";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.labelBusyAllTables);
            this.panel2.Controls.Add(this.labelGenNewAllTables);
            this.panel2.Controls.Add(this.progressBarGenNewAllTables);
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
            this.panel2.Size = new System.Drawing.Size(970, 492);
            this.panel2.TabIndex = 4;
            // 
            // labelGenNewAllTables
            // 
            this.labelGenNewAllTables.AutoSize = true;
            this.labelGenNewAllTables.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelGenNewAllTables.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelGenNewAllTables.Location = new System.Drawing.Point(299, 408);
            this.labelGenNewAllTables.Name = "labelGenNewAllTables";
            this.labelGenNewAllTables.Size = new System.Drawing.Size(56, 13);
            this.labelGenNewAllTables.TabIndex = 17;
            this.labelGenNewAllTables.Text = "progress...";
            this.labelGenNewAllTables.Visible = false;
            // 
            // progressBarGenNewAllTables
            // 
            this.progressBarGenNewAllTables.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBarGenNewAllTables.Location = new System.Drawing.Point(299, 421);
            this.progressBarGenNewAllTables.Name = "progressBarGenNewAllTables";
            this.progressBarGenNewAllTables.Size = new System.Drawing.Size(395, 23);
            this.progressBarGenNewAllTables.TabIndex = 16;
            this.progressBarGenNewAllTables.Visible = false;
            // 
            // buttonNewAllTables
            // 
            this.buttonNewAllTables.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonNewAllTables.Enabled = false;
            this.buttonNewAllTables.FlatAppearance.BorderSize = 2;
            this.buttonNewAllTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewAllTables.Location = new System.Drawing.Point(794, 57);
            this.buttonNewAllTables.Name = "buttonNewAllTables";
            this.buttonNewAllTables.Size = new System.Drawing.Size(151, 97);
            this.buttonNewAllTables.TabIndex = 15;
            this.buttonNewAllTables.Text = "New All-Tables";
            this.buttonNewAllTables.UseVisualStyleBackColor = true;
            this.buttonNewAllTables.Click += new System.EventHandler(this.OnMakeNewAllTables);
            // 
            // labelBusyDownload
            // 
            this.labelBusyDownload.AutoSize = true;
            this.labelBusyDownload.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelBusyDownload.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelBusyDownload.Location = new System.Drawing.Point(299, 66);
            this.labelBusyDownload.Name = "labelBusyDownload";
            this.labelBusyDownload.Size = new System.Drawing.Size(115, 13);
            this.labelBusyDownload.TabIndex = 11;
            this.labelBusyDownload.Text = "downloads remaining...";
            this.labelBusyDownload.Visible = false;
            // 
            // buttonNewShareList
            // 
            this.buttonNewShareList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNewShareList.Enabled = false;
            this.buttonNewShareList.FlatAppearance.BorderSize = 2;
            this.buttonNewShareList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewShareList.Location = new System.Drawing.Point(304, 93);
            this.buttonNewShareList.Name = "buttonNewShareList";
            this.buttonNewShareList.Size = new System.Drawing.Size(383, 30);
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
            this.buttonLogfile.Size = new System.Drawing.Size(395, 23);
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
            this.buttonExplorer.Size = new System.Drawing.Size(395, 23);
            this.buttonExplorer.TabIndex = 13;
            this.buttonExplorer.Text = "Open Extra Folder";
            this.buttonExplorer.UseVisualStyleBackColor = true;
            this.buttonExplorer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnOpenExplorer);
            // 
            // progressBarDownload
            // 
            this.progressBarDownload.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBarDownload.Location = new System.Drawing.Point(299, 43);
            this.progressBarDownload.Name = "progressBarDownload";
            this.progressBarDownload.Size = new System.Drawing.Size(395, 23);
            this.progressBarDownload.TabIndex = 3;
            this.progressBarDownload.Visible = false;
            // 
            // buttonDayDataDownload
            // 
            this.buttonDayDataDownload.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonDayDataDownload.Enabled = false;
            this.buttonDayDataDownload.FlatAppearance.BorderSize = 2;
            this.buttonDayDataDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDayDataDownload.Location = new System.Drawing.Point(299, 0);
            this.buttonDayDataDownload.Name = "buttonDayDataDownload";
            this.buttonDayDataDownload.Size = new System.Drawing.Size(395, 43);
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
            this.listBoxShareList.Location = new System.Drawing.Point(694, 0);
            this.listBoxShareList.Name = "listBoxShareList";
            this.listBoxShareList.ScrollAlwaysVisible = true;
            this.listBoxShareList.Size = new System.Drawing.Size(274, 490);
            this.listBoxShareList.Sorted = true;
            this.listBoxShareList.TabIndex = 14;
            this.listBoxShareList.ValueMember = "Number";
            this.listBoxShareList.DoubleClick += new System.EventHandler(this.OnShareDoubleClicked);
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
            this.statusStrip.Size = new System.Drawing.Size(970, 22);
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
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.labelDatafilesCount);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.textBoxShareNumSearch);
            this.panel1.Controls.Add(this.buttonNewAllTables);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.groupBoxSource);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.daysBack);
            this.panel1.Controls.Add(this.labelBackFrom);
            this.panel1.Controls.Add(this.calendarTo);
            this.panel1.Controls.Add(this.calendarFrom);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(970, 215);
            this.panel1.TabIndex = 0;
            // 
            // labelDatafilesCount
            // 
            this.labelDatafilesCount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelDatafilesCount.AutoSize = true;
            this.labelDatafilesCount.Location = new System.Drawing.Point(240, 201);
            this.labelDatafilesCount.Name = "labelDatafilesCount";
            this.labelDatafilesCount.Size = new System.Drawing.Size(63, 13);
            this.labelDatafilesCount.TabIndex = 15;
            this.labelDatafilesCount.Text = "files present";
            this.labelDatafilesCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(832, 196);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "search share #";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxShareNumSearch
            // 
            this.textBoxShareNumSearch.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.textBoxShareNumSearch.Location = new System.Drawing.Point(914, 193);
            this.textBoxShareNumSearch.Name = "textBoxShareNumSearch";
            this.textBoxShareNumSearch.Size = new System.Drawing.Size(50, 20);
            this.textBoxShareNumSearch.TabIndex = 13;
            this.textBoxShareNumSearch.TextChanged += new System.EventHandler(this.OnSearchForShare);
            this.textBoxShareNumSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxShareNumSearch_KeyPress);
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
            this.buttonLogin.FlatAppearance.BorderSize = 2;
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
            this.label7.Location = new System.Drawing.Point(696, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 20);
            this.label7.TabIndex = 9;
            this.label7.Text = "Share List";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(675, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "trading days";
            // 
            // daysBack
            // 
            this.daysBack.Location = new System.Drawing.Point(621, 3);
            this.daysBack.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
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
            // labelBackFrom
            // 
            this.labelBackFrom.AutoSize = true;
            this.labelBackFrom.Location = new System.Drawing.Point(298, 8);
            this.labelBackFrom.Name = "labelBackFrom";
            this.labelBackFrom.Size = new System.Drawing.Size(59, 13);
            this.labelBackFrom.TabIndex = 3;
            this.labelBackFrom.Text = "From today";
            // 
            // calendarTo
            // 
            this.calendarTo.Location = new System.Drawing.Point(297, 24);
            this.calendarTo.MaxSelectionCount = 1;
            this.calendarTo.Name = "calendarTo";
            this.calendarTo.ShowToday = false;
            this.calendarTo.TabIndex = 8;
            this.calendarTo.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.ToDateChanged);
            // 
            // calendarFrom
            // 
            this.calendarFrom.Location = new System.Drawing.Point(542, 24);
            this.calendarFrom.MaxSelectionCount = 1;
            this.calendarFrom.Name = "calendarFrom";
            this.calendarFrom.ShowToday = false;
            this.calendarFrom.ShowTodayCircle = false;
            this.calendarFrom.TabIndex = 6;
            this.calendarFrom.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.FromDateChanged);
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Size = new System.Drawing.Size(976, 735);
            this.tabPageSettings.TabIndex = 2;
            this.tabPageSettings.Text = "Settings";
            // 
            // labelBusyAllTables
            // 
            this.labelBusyAllTables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBusyAllTables.AutoSize = true;
            this.labelBusyAllTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBusyAllTables.ForeColor = System.Drawing.Color.Green;
            this.labelBusyAllTables.Location = new System.Drawing.Point(334, 193);
            this.labelBusyAllTables.Name = "labelBusyAllTables";
            this.labelBusyAllTables.Size = new System.Drawing.Size(353, 37);
            this.labelBusyAllTables.TabIndex = 18;
            this.labelBusyAllTables.Text = "Generating All-Tables...";
            this.labelBusyAllTables.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(543, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "backwards for";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.tabControlMain);
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClose);
            this.Load += new System.EventHandler(this.OnLoad);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageImportation.ResumeLayout(false);
            this.tabPageImportation.PerformLayout();
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
        private System.Windows.Forms.TabPage tabPageImportation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelBackFrom;
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
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.Button buttonNewAllTables;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxShareNumSearch;
        internal System.Windows.Forms.ProgressBar progressBarGenNewAllTables;
        internal System.Windows.Forms.Label labelGenNewAllTables;
        private System.Windows.Forms.Label labelDatafilesCount;
        private System.Windows.Forms.Label labelBusyAllTables;
        private System.Windows.Forms.Label label3;
    }
}

