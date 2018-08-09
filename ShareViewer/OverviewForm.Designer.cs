namespace ShareViewer
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
            this.listBoxCols = new System.Windows.Forms.ListBox();
            this.panelTop = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxCalculations = new System.Windows.Forms.GroupBox();
            this.linkLabelLoad = new System.Windows.Forms.LinkLabel();
            this.buttonCalcAll = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripDDBtnSaveAs = new System.Windows.Forms.ToolStripDropDownButton();
            this.loadNamedOverviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripOverviewNameLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.groupBoxCalculations.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBoxParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOverview)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripText});
            this.statusStrip.Location = new System.Drawing.Point(0, 665);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1157, 22);
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
            this.panelLeft.Controls.Add(this.listBoxCols);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(165, 665);
            this.panelLeft.TabIndex = 4;
            // 
            // listBoxCols
            // 
            this.listBoxCols.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBoxCols.FormattingEnabled = true;
            this.listBoxCols.Location = new System.Drawing.Point(0, 0);
            this.listBoxCols.Name = "listBoxCols";
            this.listBoxCols.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxCols.Size = new System.Drawing.Size(162, 665);
            this.listBoxCols.TabIndex = 4;
            this.listBoxCols.SelectedIndexChanged += new System.EventHandler(this.listBoxCols_SelectedIndexChanged);
            // 
            // panelTop
            // 
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
            this.panelTop.Size = new System.Drawing.Size(992, 211);
            this.panelTop.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "\\/ double click for All-Table";
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
            this.groupBoxCalculations.Size = new System.Drawing.Size(743, 208);
            this.groupBoxCalculations.TabIndex = 22;
            this.groupBoxCalculations.TabStop = false;
            this.groupBoxCalculations.Text = "Calculations";
            // 
            // linkLabelLoad
            // 
            this.linkLabelLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelLoad.AutoSize = true;
            this.linkLabelLoad.Location = new System.Drawing.Point(645, 161);
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
            this.buttonCalcAll.Location = new System.Drawing.Point(620, 12);
            this.buttonCalcAll.Name = "buttonCalcAll";
            this.buttonCalcAll.Size = new System.Drawing.Size(117, 85);
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
            this.buttonSave.Location = new System.Drawing.Point(643, 128);
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
            this.statusStrip1.Size = new System.Drawing.Size(737, 24);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripDDBtnSaveAs
            // 
            this.toolStripDDBtnSaveAs.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDDBtnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDDBtnSaveAs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadNamedOverviewToolStripMenuItem,
            this.saveAsToolStripMenuItem});
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
            this.groupBoxParams.Size = new System.Drawing.Size(374, 180);
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
            this.listBoxVariables.Location = new System.Drawing.Point(13, 15);
            this.listBoxVariables.Name = "listBoxVariables";
            this.listBoxVariables.Size = new System.Drawing.Size(207, 160);
            this.listBoxVariables.TabIndex = 11;
            this.listBoxVariables.SelectedIndexChanged += new System.EventHandler(this.listBoxVariables_SelectedIndexChanged);
            // 
            // linkLabelLazy
            // 
            this.linkLabelLazy.AutoSize = true;
            this.linkLabelLazy.Location = new System.Drawing.Point(7, 42);
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
            this.comboBoxViews.Location = new System.Drawing.Point(6, 151);
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
            this.linkLabelSaveView.Location = new System.Drawing.Point(6, 129);
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
            this.linkLabelLock.Location = new System.Drawing.Point(3, 10);
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
            this.dgOverview.Size = new System.Drawing.Size(992, 454);
            this.dgOverview.TabIndex = 6;
            this.dgOverview.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgOverview_ColumnHeaderMouseClick);
            this.dgOverview.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgOverview_RowHeaderMouseDoubleClick);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // toolStripOverviewNameLabel
            // 
            this.toolStripOverviewNameLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripOverviewNameLabel.ForeColor = System.Drawing.Color.Green;
            this.toolStripOverviewNameLabel.Name = "toolStripOverviewNameLabel";
            this.toolStripOverviewNameLabel.Size = new System.Drawing.Size(16, 19);
            this.toolStripOverviewNameLabel.Text = "...";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // OverviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1157, 687);
            this.Controls.Add(this.dgOverview);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.statusStrip);
            this.Name = "OverviewForm";
            this.Text = "Overview";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
    }
}