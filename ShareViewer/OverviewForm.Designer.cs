﻿namespace ShareViewer
{
    partial class OverviewForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OverviewForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.stripText = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.labelShareListName = new System.Windows.Forms.Label();
            this.linkLabelAllShares = new System.Windows.Forms.LinkLabel();
            this.linkLabelChooseSelecteds = new System.Windows.Forms.LinkLabel();
            this.listBoxSelecteds = new System.Windows.Forms.ListBox();
            this.listBoxCols = new System.Windows.Forms.ListBox();
            this.panelTop = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabelNotes = new System.Windows.Forms.LinkLabel();
            this.linkLabelDeleteSelected = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxCalculations = new System.Windows.Forms.GroupBox();
            this.linkLabelLoad = new System.Windows.Forms.LinkLabel();
            this.buttonCalcAll = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripDDBtnSaveAs = new System.Windows.Forms.ToolStripDropDownButton();
            this.loadNamedOverviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemSaveNewShareList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripOverviewNameLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripCalcs = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxParams = new System.Windows.Forms.GroupBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.listBoxVariables = new System.Windows.Forms.ListBox();
            this.linkLabelLazy = new System.Windows.Forms.LinkLabel();
            this.comboBoxViews = new System.Windows.Forms.ComboBox();
            this.linkLabelSaveView = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabelLock = new System.Windows.Forms.LinkLabel();
            this.dgOverview = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.groupBoxCalculations.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBoxParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOverview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripText});
            this.statusStrip.Location = new System.Drawing.Point(0, 707);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip";
            // 
            // stripText
            // 
            this.stripText.Name = "stripText";
            this.stripText.Size = new System.Drawing.Size(22, 17);
            this.stripText.Text = "Ok";
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.splitContainer1);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(165, 707);
            this.panelLeft.TabIndex = 4;
            // 
            // labelShareListName
            // 
            this.labelShareListName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelShareListName.AutoSize = true;
            this.labelShareListName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShareListName.ForeColor = System.Drawing.Color.Green;
            this.labelShareListName.Location = new System.Drawing.Point(2, 23);
            this.labelShareListName.Name = "labelShareListName";
            this.labelShareListName.Size = new System.Drawing.Size(163, 17);
            this.labelShareListName.TabIndex = 8;
            this.labelShareListName.Text = "All shares to be included";
            // 
            // linkLabelAllShares
            // 
            this.linkLabelAllShares.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelAllShares.AutoSize = true;
            this.linkLabelAllShares.Location = new System.Drawing.Point(108, 4);
            this.linkLabelAllShares.Name = "linkLabelAllShares";
            this.linkLabelAllShares.Size = new System.Drawing.Size(54, 13);
            this.linkLabelAllShares.TabIndex = 7;
            this.linkLabelAllShares.TabStop = true;
            this.linkLabelAllShares.Text = "All Shares";
            this.linkLabelAllShares.Click += new System.EventHandler(this.linkLabelAllShares_Click);
            // 
            // linkLabelChooseSelecteds
            // 
            this.linkLabelChooseSelecteds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelChooseSelecteds.AutoSize = true;
            this.linkLabelChooseSelecteds.Location = new System.Drawing.Point(1, 4);
            this.linkLabelChooseSelecteds.Name = "linkLabelChooseSelecteds";
            this.linkLabelChooseSelecteds.Size = new System.Drawing.Size(99, 13);
            this.linkLabelChooseSelecteds.TabIndex = 6;
            this.linkLabelChooseSelecteds.TabStop = true;
            this.linkLabelChooseSelecteds.Text = "Choose a ShareList";
            this.linkLabelChooseSelecteds.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelChooseSelecteds_LinkClicked);
            // 
            // listBoxSelecteds
            // 
            this.listBoxSelecteds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxSelecteds.FormattingEnabled = true;
            this.listBoxSelecteds.Location = new System.Drawing.Point(0, 48);
            this.listBoxSelecteds.Name = "listBoxSelecteds";
            this.listBoxSelecteds.ScrollAlwaysVisible = true;
            this.listBoxSelecteds.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxSelecteds.Size = new System.Drawing.Size(165, 293);
            this.listBoxSelecteds.TabIndex = 5;
            // 
            // listBoxCols
            // 
            this.listBoxCols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxCols.FormattingEnabled = true;
            this.listBoxCols.Location = new System.Drawing.Point(0, 0);
            this.listBoxCols.Name = "listBoxCols";
            this.listBoxCols.ScrollAlwaysVisible = true;
            this.listBoxCols.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxCols.Size = new System.Drawing.Size(165, 360);
            this.listBoxCols.TabIndex = 4;
            this.listBoxCols.SelectedIndexChanged += new System.EventHandler(this.listBoxCols_SelectedIndexChanged);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.label3);
            this.panelTop.Controls.Add(this.linkLabelNotes);
            this.panelTop.Controls.Add(this.linkLabelDeleteSelected);
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.groupBoxCalculations);
            this.panelTop.Controls.Add(this.linkLabelLazy);
            this.panelTop.Controls.Add(this.comboBoxViews);
            this.panelTop.Controls.Add(this.linkLabelSaveView);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.linkLabelLock);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(165, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(843, 211);
            this.panelTop.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.IndianRed;
            this.label3.Location = new System.Drawing.Point(13, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 20);
            this.label3.TabIndex = 26;
            this.label3.Text = "OVERVIEW TABLE";
            // 
            // linkLabelNotes
            // 
            this.linkLabelNotes.AutoSize = true;
            this.linkLabelNotes.Enabled = false;
            this.linkLabelNotes.Location = new System.Drawing.Point(207, 134);
            this.linkLabelNotes.Name = "linkLabelNotes";
            this.linkLabelNotes.Size = new System.Drawing.Size(33, 13);
            this.linkLabelNotes.TabIndex = 25;
            this.linkLabelNotes.TabStop = true;
            this.linkLabelNotes.Text = "notes";
            this.toolTip1.SetToolTip(this.linkLabelNotes, "See/edit notes entered when Overview was saved. \r\n");
            this.linkLabelNotes.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelNotes_LinkClicked);
            // 
            // linkLabelDeleteSelected
            // 
            this.linkLabelDeleteSelected.AutoSize = true;
            this.linkLabelDeleteSelected.Location = new System.Drawing.Point(3, 89);
            this.linkLabelDeleteSelected.Name = "linkLabelDeleteSelected";
            this.linkLabelDeleteSelected.Size = new System.Drawing.Size(124, 13);
            this.linkLabelDeleteSelected.TabIndex = 24;
            this.linkLabelDeleteSelected.TabStop = true;
            this.linkLabelDeleteSelected.Text = "Discard Selected Shares";
            this.linkLabelDeleteSelected.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDeleteSelected_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(235, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "\\/ dbl click: full All-Table, Rt-Click: See Last Day";
            this.toolTip1.SetToolTip(this.label2, "Double click on the far left of the row to pop up the corresponding All-Table");
            // 
            // groupBoxCalculations
            // 
            this.groupBoxCalculations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCalculations.Controls.Add(this.linkLabelLoad);
            this.groupBoxCalculations.Controls.Add(this.buttonCalcAll);
            this.groupBoxCalculations.Controls.Add(this.buttonSave);
            this.groupBoxCalculations.Controls.Add(this.statusStrip1);
            this.groupBoxCalculations.Controls.Add(this.groupBoxParams);
            this.groupBoxCalculations.Controls.Add(this.listBoxVariables);
            this.groupBoxCalculations.Location = new System.Drawing.Point(246, 1);
            this.groupBoxCalculations.Name = "groupBoxCalculations";
            this.groupBoxCalculations.Size = new System.Drawing.Size(594, 208);
            this.groupBoxCalculations.TabIndex = 22;
            this.groupBoxCalculations.TabStop = false;
            this.groupBoxCalculations.Text = "Calculations";
            // 
            // linkLabelLoad
            // 
            this.linkLabelLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelLoad.AutoSize = true;
            this.linkLabelLoad.Location = new System.Drawing.Point(496, 161);
            this.linkLabelLoad.Name = "linkLabelLoad";
            this.linkLabelLoad.Size = new System.Drawing.Size(75, 13);
            this.linkLabelLoad.TabIndex = 18;
            this.linkLabelLoad.TabStop = true;
            this.linkLabelLoad.Text = "Quick Re-load";
            this.toolTip1.SetToolTip(this.linkLabelLoad, "Load the grid with the Overview which was last \'quick-saved\'.\r\nIt will be overwri" +
        "tten should you save again.");
            this.linkLabelLoad.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLoad_LinkClicked);
            // 
            // buttonCalcAll
            // 
            this.buttonCalcAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCalcAll.Image = ((System.Drawing.Image)(resources.GetObject("buttonCalcAll.Image")));
            this.buttonCalcAll.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonCalcAll.Location = new System.Drawing.Point(471, 12);
            this.buttonCalcAll.Name = "buttonCalcAll";
            this.buttonCalcAll.Size = new System.Drawing.Size(117, 65);
            this.buttonCalcAll.TabIndex = 17;
            this.buttonCalcAll.Text = "Compile | ReCalc";
            this.buttonCalcAll.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonCalcAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.buttonCalcAll, "Compile an Overview from existing All-Tables or \r\nRe-Calculate and then complie, " +
        "\r\ndepending on whether parameters have changed.\r\n");
            this.buttonCalcAll.UseVisualStyleBackColor = true;
            this.buttonCalcAll.Click += new System.EventHandler(this.buttonCalcAll_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(494, 128);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(76, 28);
            this.buttonSave.TabIndex = 16;
            this.buttonSave.Text = "Quick Save";
            this.toolTip1.SetToolTip(this.buttonSave, "Save the current Overview so that it may be quickly recalled next time.\r\nIt will " +
        "overwrite any previously saved Overview.\r\nFor more permanent named worksheet sav" +
        "es, see the Save|Load button");
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDDBtnSaveAs,
            this.toolStripOverviewNameLabel,
            this.toolStripCalcs});
            this.statusStrip1.Location = new System.Drawing.Point(3, 181);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(588, 24);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripDDBtnSaveAs
            // 
            this.toolStripDDBtnSaveAs.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDDBtnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDDBtnSaveAs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadNamedOverviewToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItemSaveNewShareList});
            this.toolStripDDBtnSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDDBtnSaveAs.Image")));
            this.toolStripDDBtnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDDBtnSaveAs.Name = "toolStripDDBtnSaveAs";
            this.toolStripDDBtnSaveAs.Size = new System.Drawing.Size(79, 22);
            this.toolStripDDBtnSaveAs.Text = "Save | Load";
            // 
            // loadNamedOverviewToolStripMenuItem
            // 
            this.loadNamedOverviewToolStripMenuItem.Name = "loadNamedOverviewToolStripMenuItem";
            this.loadNamedOverviewToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.loadNamedOverviewToolStripMenuItem.Text = "Load named Overview";
            this.loadNamedOverviewToolStripMenuItem.Click += new System.EventHandler(this.loadNamedOverviewToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.saveAsToolStripMenuItem.Text = "Save as a named Overview";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(210, 6);
            // 
            // toolStripMenuItemSaveNewShareList
            // 
            this.toolStripMenuItemSaveNewShareList.Name = "toolStripMenuItemSaveNewShareList";
            this.toolStripMenuItemSaveNewShareList.Size = new System.Drawing.Size(213, 22);
            this.toolStripMenuItemSaveNewShareList.Text = "Save as a Named ShareList";
            this.toolStripMenuItemSaveNewShareList.Click += new System.EventHandler(this.toolStripMenuItemSaveNewShareList_Click);
            // 
            // toolStripOverviewNameLabel
            // 
            this.toolStripOverviewNameLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripOverviewNameLabel.ForeColor = System.Drawing.Color.Green;
            this.toolStripOverviewNameLabel.Name = "toolStripOverviewNameLabel";
            this.toolStripOverviewNameLabel.Size = new System.Drawing.Size(16, 19);
            this.toolStripOverviewNameLabel.Text = "...";
            // 
            // toolStripCalcs
            // 
            this.toolStripCalcs.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripCalcs.Margin = new System.Windows.Forms.Padding(142, 3, 0, 2);
            this.toolStripCalcs.Name = "toolStripCalcs";
            this.toolStripCalcs.Size = new System.Drawing.Size(20, 19);
            this.toolStripCalcs.Text = "...";
            // 
            // groupBoxParams
            // 
            this.groupBoxParams.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxParams.Controls.Add(this.propertyGrid1);
            this.groupBoxParams.Location = new System.Drawing.Point(240, 1);
            this.groupBoxParams.Name = "groupBoxParams";
            this.groupBoxParams.Size = new System.Drawing.Size(225, 180);
            this.groupBoxParams.TabIndex = 13;
            this.groupBoxParams.TabStop = false;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(16, 23);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(130, 130);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.ToolbarVisible = false;
            // 
            // listBoxVariables
            // 
            this.listBoxVariables.FormattingEnabled = true;
            this.listBoxVariables.Location = new System.Drawing.Point(16, 13);
            this.listBoxVariables.Name = "listBoxVariables";
            this.listBoxVariables.Size = new System.Drawing.Size(207, 160);
            this.listBoxVariables.TabIndex = 11;
            this.listBoxVariables.SelectedIndexChanged += new System.EventHandler(this.listBoxVariables_SelectedIndexChanged);
            // 
            // linkLabelLazy
            // 
            this.linkLabelLazy.AutoSize = true;
            this.linkLabelLazy.Location = new System.Drawing.Point(3, 65);
            this.linkLabelLazy.Name = "linkLabelLazy";
            this.linkLabelLazy.Size = new System.Drawing.Size(104, 13);
            this.linkLabelLazy.TabIndex = 21;
            this.linkLabelLazy.TabStop = true;
            this.linkLabelLazy.Text = "Discard Lazy Shares";
            this.toolTip1.SetToolTip(this.linkLabelLazy, "Hides or Shows Lazy (or those checked as \'Lazy\') shares. ");
            this.linkLabelLazy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLazy_LinkClicked);
            // 
            // comboBoxViews
            // 
            this.comboBoxViews.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxViews.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxViews.FormattingEnabled = true;
            this.comboBoxViews.Items.AddRange(new object[] {
            "Initial"});
            this.comboBoxViews.Location = new System.Drawing.Point(6, 154);
            this.comboBoxViews.Name = "comboBoxViews";
            this.comboBoxViews.Size = new System.Drawing.Size(219, 24);
            this.comboBoxViews.Sorted = true;
            this.comboBoxViews.TabIndex = 20;
            this.comboBoxViews.Text = "Views";
            this.comboBoxViews.SelectedIndexChanged += new System.EventHandler(this.comboBoxViews_SelectedIndexChanged);
            // 
            // linkLabelSaveView
            // 
            this.linkLabelSaveView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelSaveView.AutoSize = true;
            this.linkLabelSaveView.Location = new System.Drawing.Point(3, 133);
            this.linkLabelSaveView.Name = "linkLabelSaveView";
            this.linkLabelSaveView.Size = new System.Drawing.Size(93, 13);
            this.linkLabelSaveView.TabIndex = 19;
            this.linkLabelSaveView.TabStop = true;
            this.linkLabelSaveView.Text = "Save current view";
            this.linkLabelSaveView.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSaveView_LinkClicked);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "<-- ctrl-click to add/remove columns (not shift)";
            // 
            // linkLabelLock
            // 
            this.linkLabelLock.AutoSize = true;
            this.linkLabelLock.Location = new System.Drawing.Point(3, 42);
            this.linkLabelLock.Name = "linkLabelLock";
            this.linkLabelLock.Size = new System.Drawing.Size(52, 13);
            this.linkLabelLock.TabIndex = 17;
            this.linkLabelLock.TabStop = true;
            this.linkLabelLock.Text = "lock view";
            this.linkLabelLock.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLock_LinkClicked);
            // 
            // dgOverview
            // 
            this.dgOverview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgOverview.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgOverview.Location = new System.Drawing.Point(165, 211);
            this.dgOverview.Name = "dgOverview";
            this.dgOverview.Size = new System.Drawing.Size(843, 496);
            this.dgOverview.TabIndex = 6;
            this.dgOverview.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgOverview_ColumnHeaderMouseClick);
            this.dgOverview.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgOverview_RowHeaderMouseClick);
            this.dgOverview.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgOverview_RowHeaderMouseDoubleClick);
            this.dgOverview.SelectionChanged += new System.EventHandler(this.dgOverview_SelectionChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBoxCols);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listBoxSelecteds);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(165, 707);
            this.splitContainer1.SplitterDistance = 360;
            this.splitContainer1.SplitterIncrement = 2;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelShareListName);
            this.panel1.Controls.Add(this.linkLabelChooseSelecteds);
            this.panel1.Controls.Add(this.linkLabelAllShares);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(165, 48);
            this.panel1.TabIndex = 0;
            // 
            // OverviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.dgOverview);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.statusStrip);
            this.Name = "OverviewForm";
            this.Text = "Overview";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.OverviewForm_HelpButtonClicked);
            this.Load += new System.EventHandler(this.OverviewForm_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.groupBoxCalculations.ResumeLayout(false);
            this.groupBoxCalculations.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxParams.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgOverview)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel stripText;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.ListBox listBoxCols;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.LinkLabel linkLabelLock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabelSaveView;
        private System.Windows.Forms.ComboBox comboBoxViews;
        private System.Windows.Forms.DataGridView dgOverview;
        private System.Windows.Forms.LinkLabel linkLabelLazy;
        private System.Windows.Forms.GroupBox groupBoxCalculations;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBoxParams;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ListBox listBoxVariables;
        private System.Windows.Forms.ToolStripStatusLabel toolStripCalcs;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCalcAll;
        private System.Windows.Forms.LinkLabel linkLabelLoad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDDBtnSaveAs;
        private System.Windows.Forms.ToolStripMenuItem loadNamedOverviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripOverviewNameLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.LinkLabel linkLabelDeleteSelected;
        private System.Windows.Forms.LinkLabel linkLabelNotes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.LinkLabel linkLabelAllShares;
        private System.Windows.Forms.LinkLabel linkLabelChooseSelecteds;
        private System.Windows.Forms.ListBox listBoxSelecteds;
        private System.Windows.Forms.Label labelShareListName;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSaveNewShareList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
    }
}