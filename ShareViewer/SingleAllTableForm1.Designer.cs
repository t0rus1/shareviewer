namespace ShareViewer
{
    partial class SingleAllTableForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.listBoxCols = new System.Windows.Forms.ListBox();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.panelTop = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.labelLazy = new System.Windows.Forms.Label();
            this.linkLabelLock = new System.Windows.Forms.LinkLabel();
            this.linkLabelSaveView = new System.Windows.Forms.LinkLabel();
            this.comboBoxViews = new System.Windows.Forms.ComboBox();
            this.labelShareDesc = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioWeekly = new System.Windows.Forms.RadioButton();
            this.radioDays = new System.Windows.Forms.RadioButton();
            this.radioHours = new System.Windows.Forms.RadioButton();
            this.radio5mins = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxCalculations = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.linkLabelCalcToHere = new System.Windows.Forms.LinkLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stripText = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxParams = new System.Windows.Forms.GroupBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.listBoxVariables = new System.Windows.Forms.ListBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.panelTop.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxCalculations.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBoxParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.listBoxCols);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(130, 687);
            this.panelLeft.TabIndex = 0;
            // 
            // listBoxCols
            // 
            this.listBoxCols.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBoxCols.FormattingEnabled = true;
            this.listBoxCols.Location = new System.Drawing.Point(0, 0);
            this.listBoxCols.Name = "listBoxCols";
            this.listBoxCols.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxCols.Size = new System.Drawing.Size(130, 687);
            this.listBoxCols.TabIndex = 0;
            this.listBoxCols.SelectedIndexChanged += new System.EventHandler(this.listBoxCols_SelectedIndexChanged);
            // 
            // dgView
            // 
            this.dgView.AllowUserToDeleteRows = false;
            this.dgView.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.NullValue = null;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.Format = "N3";
            dataGridViewCellStyle6.NullValue = null;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgView.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgView.EnableHeadersVisualStyles = false;
            this.dgView.Location = new System.Drawing.Point(130, 211);
            this.dgView.Name = "dgView";
            this.dgView.ReadOnly = true;
            this.dgView.Size = new System.Drawing.Size(971, 476);
            this.dgView.TabIndex = 1;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.label3);
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.labelLazy);
            this.panelTop.Controls.Add(this.linkLabelLock);
            this.panelTop.Controls.Add(this.linkLabelSaveView);
            this.panelTop.Controls.Add(this.comboBoxViews);
            this.panelTop.Controls.Add(this.labelShareDesc);
            this.panelTop.Controls.Add(this.groupBox2);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.groupBoxCalculations);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(130, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(971, 211);
            this.panelTop.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(172, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Modelling done here -->";
            // 
            // labelLazy
            // 
            this.labelLazy.AutoSize = true;
            this.labelLazy.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLazy.ForeColor = System.Drawing.Color.IndianRed;
            this.labelLazy.Location = new System.Drawing.Point(7, 67);
            this.labelLazy.Name = "labelLazy";
            this.labelLazy.Size = new System.Drawing.Size(157, 26);
            this.labelLazy.TabIndex = 17;
            this.labelLazy.Text = "currently LAZY";
            this.labelLazy.Visible = false;
            // 
            // linkLabelLock
            // 
            this.linkLabelLock.AutoSize = true;
            this.linkLabelLock.Location = new System.Drawing.Point(7, 120);
            this.linkLabelLock.Name = "linkLabelLock";
            this.linkLabelLock.Size = new System.Drawing.Size(52, 13);
            this.linkLabelLock.TabIndex = 16;
            this.linkLabelLock.TabStop = true;
            this.linkLabelLock.Text = "lock view";
            this.linkLabelLock.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLock_LinkClicked);
            // 
            // linkLabelSaveView
            // 
            this.linkLabelSaveView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelSaveView.AutoSize = true;
            this.linkLabelSaveView.Location = new System.Drawing.Point(7, 162);
            this.linkLabelSaveView.Name = "linkLabelSaveView";
            this.linkLabelSaveView.Size = new System.Drawing.Size(93, 13);
            this.linkLabelSaveView.TabIndex = 8;
            this.linkLabelSaveView.TabStop = true;
            this.linkLabelSaveView.Text = "Save current view";
            this.linkLabelSaveView.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSaveView_LinkClicked);
            // 
            // comboBoxViews
            // 
            this.comboBoxViews.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxViews.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxViews.FormattingEnabled = true;
            this.comboBoxViews.Items.AddRange(new object[] {
            "Initial"});
            this.comboBoxViews.Location = new System.Drawing.Point(7, 183);
            this.comboBoxViews.Name = "comboBoxViews";
            this.comboBoxViews.Size = new System.Drawing.Size(281, 24);
            this.comboBoxViews.Sorted = true;
            this.comboBoxViews.TabIndex = 7;
            this.comboBoxViews.Text = "Views";
            this.comboBoxViews.SelectedIndexChanged += new System.EventHandler(this.comboBoxViews_SelectedIndexChanged);
            // 
            // labelShareDesc
            // 
            this.labelShareDesc.AutoSize = true;
            this.labelShareDesc.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelShareDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelShareDesc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.labelShareDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShareDesc.ForeColor = System.Drawing.Color.IndianRed;
            this.labelShareDesc.Location = new System.Drawing.Point(7, 41);
            this.labelShareDesc.Name = "labelShareDesc";
            this.labelShareDesc.Size = new System.Drawing.Size(51, 22);
            this.labelShareDesc.TabIndex = 6;
            this.labelShareDesc.Text = "share";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioWeekly);
            this.groupBox2.Controls.Add(this.radioDays);
            this.groupBox2.Controls.Add(this.radioHours);
            this.groupBox2.Controls.Add(this.radio5mins);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(868, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(103, 211);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Vertically";
            // 
            // radioWeekly
            // 
            this.radioWeekly.AutoSize = true;
            this.radioWeekly.Location = new System.Drawing.Point(8, 118);
            this.radioWeekly.Name = "radioWeekly";
            this.radioWeekly.Size = new System.Drawing.Size(93, 17);
            this.radioWeekly.TabIndex = 3;
            this.radioWeekly.TabStop = true;
            this.radioWeekly.Text = "Weekly bands";
            this.radioWeekly.UseVisualStyleBackColor = true;
            this.radioWeekly.CheckedChanged += new System.EventHandler(this.radioWeekly_CheckedChanged);
            // 
            // radioDays
            // 
            this.radioDays.AutoSize = true;
            this.radioDays.Location = new System.Drawing.Point(8, 84);
            this.radioDays.Name = "radioDays";
            this.radioDays.Size = new System.Drawing.Size(80, 17);
            this.radioDays.TabIndex = 2;
            this.radioDays.TabStop = true;
            this.radioDays.Text = "Daily bands";
            this.radioDays.UseVisualStyleBackColor = true;
            this.radioDays.CheckedChanged += new System.EventHandler(this.radioDays_CheckedChanged);
            // 
            // radioHours
            // 
            this.radioHours.AutoSize = true;
            this.radioHours.Location = new System.Drawing.Point(8, 50);
            this.radioHours.Name = "radioHours";
            this.radioHours.Size = new System.Drawing.Size(87, 17);
            this.radioHours.TabIndex = 1;
            this.radioHours.TabStop = true;
            this.radioHours.Text = "Hourly bands";
            this.radioHours.UseVisualStyleBackColor = true;
            this.radioHours.CheckedChanged += new System.EventHandler(this.radioHours_CheckedChanged);
            // 
            // radio5mins
            // 
            this.radio5mins.AutoSize = true;
            this.radio5mins.Checked = true;
            this.radio5mins.Location = new System.Drawing.Point(8, 16);
            this.radio5mins.Name = "radio5mins";
            this.radio5mins.Size = new System.Drawing.Size(82, 17);
            this.radio5mins.TabIndex = 0;
            this.radio5mins.TabStop = true;
            this.radio5mins.Text = "5 min bands";
            this.radio5mins.UseVisualStyleBackColor = true;
            this.radio5mins.CheckedChanged += new System.EventHandler(this.radio5mins_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "<-- ctrl-click to add/remove columns (not shift)";
            // 
            // groupBoxCalculations
            // 
            this.groupBoxCalculations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCalculations.Controls.Add(this.toolStrip1);
            this.groupBoxCalculations.Controls.Add(this.linkLabelCalcToHere);
            this.groupBoxCalculations.Controls.Add(this.statusStrip1);
            this.groupBoxCalculations.Controls.Add(this.groupBoxParams);
            this.groupBoxCalculations.Controls.Add(this.listBoxVariables);
            this.groupBoxCalculations.Location = new System.Drawing.Point(294, 3);
            this.groupBoxCalculations.Name = "groupBoxCalculations";
            this.groupBoxCalculations.Size = new System.Drawing.Size(568, 208);
            this.groupBoxCalculations.TabIndex = 15;
            this.groupBoxCalculations.TabStop = false;
            this.groupBoxCalculations.Text = "Calculations";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(229, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(18, 128);
            this.toolStrip1.TabIndex = 17;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(15, 125);
            this.toolStripLabel1.Text = "---------------------->";
            // 
            // linkLabelCalcToHere
            // 
            this.linkLabelCalcToHere.AutoSize = true;
            this.linkLabelCalcToHere.Location = new System.Drawing.Point(228, 143);
            this.linkLabelCalcToHere.Name = "linkLabelCalcToHere";
            this.linkLabelCalcToHere.Size = new System.Drawing.Size(102, 13);
            this.linkLabelCalcToHere.TabIndex = 16;
            this.linkLabelCalcToHere.TabStop = true;
            this.linkLabelCalcToHere.Text = "Calc Up to Selected";
            this.toolTip1.SetToolTip(this.linkLabelCalcToHere, "Perform calculations from  the first until the selected one in sequence, using cu" +
        "rrent values of all the parameters.");
            this.linkLabelCalcToHere.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelCalcToHere_LinkClicked);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripText});
            this.statusStrip1.Location = new System.Drawing.Point(3, 183);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(562, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stripText
            // 
            this.stripText.Name = "stripText";
            this.stripText.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBoxParams
            // 
            this.groupBoxParams.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxParams.Controls.Add(this.propertyGrid1);
            this.groupBoxParams.Location = new System.Drawing.Point(349, 1);
            this.groupBoxParams.Name = "groupBoxParams";
            this.groupBoxParams.Size = new System.Drawing.Size(209, 180);
            this.groupBoxParams.TabIndex = 13;
            this.groupBoxParams.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBoxParams, "Vary these parameters and re-run the calculation.\r\nDon\'t forget to Save your para" +
        "meters so that they may be recalled for next time, plus applied to all shares\r\ni" +
        "n the Overview.\r\n");
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
            this.toolTip1.SetToolTip(this.listBoxVariables, "Select a calculation to model with.\r\nNote: Each calculation will acticvate its ow" +
        "n same named \'view\'.");
            this.listBoxVariables.SelectedIndexChanged += new System.EventHandler(this.listBoxVariables_SelectedIndexChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 9000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.IndianRed;
            this.label3.Location = new System.Drawing.Point(7, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 20);
            this.label3.TabIndex = 19;
            this.label3.Text = "ALL-TABLE for:";
            // 
            // SingleAllTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 687);
            this.Controls.Add(this.dgView);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelLeft);
            this.Name = "SingleAllTableForm";
            this.Text = "Single All-Table";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SingleAllTableForm_Load);
            this.panelLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxCalculations.ResumeLayout(false);
            this.groupBoxCalculations.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxParams.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.ListBox listBoxCols;
        private System.Windows.Forms.DataGridView dgView;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioHours;
        private System.Windows.Forms.RadioButton radio5mins;
        private System.Windows.Forms.RadioButton radioDays;
        private System.Windows.Forms.RadioButton radioWeekly;
        private System.Windows.Forms.Label labelShareDesc;
        private System.Windows.Forms.ComboBox comboBoxViews;
        private System.Windows.Forms.LinkLabel linkLabelSaveView;
        private System.Windows.Forms.GroupBox groupBoxParams;
        private System.Windows.Forms.ListBox listBoxVariables;
        private System.Windows.Forms.GroupBox groupBoxCalculations;
        private System.Windows.Forms.LinkLabel linkLabelLock;
        private System.Windows.Forms.Label labelLazy;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stripText;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.LinkLabel linkLabelCalcToHere;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}