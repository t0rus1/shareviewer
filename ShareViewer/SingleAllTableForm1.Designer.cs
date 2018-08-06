﻿namespace ShareViewer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.listBoxCols = new System.Windows.Forms.ListBox();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.panelTop = new System.Windows.Forms.Panel();
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stripText = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxParams = new System.Windows.Forms.GroupBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.groupBoxMisc = new System.Windows.Forms.GroupBox();
            this.checkBoxLastDay = new System.Windows.Forms.CheckBox();
            this.checkBoxOverwriteAPs = new System.Windows.Forms.CheckBox();
            this.groupBoxScope = new System.Windows.Forms.GroupBox();
            this.radioButtonThisShare = new System.Windows.Forms.RadioButton();
            this.radioButtonAllShares = new System.Windows.Forms.RadioButton();
            this.listBoxVariables = new System.Windows.Forms.ListBox();
            this.linkLabelCalcToHere = new System.Windows.Forms.LinkLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.panelTop.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxCalculations.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBoxParams.SuspendLayout();
            this.groupBoxMisc.SuspendLayout();
            this.groupBoxScope.SuspendLayout();
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
            dataGridViewCellStyle28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle28;
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle29.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle29.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle29.NullValue = null;
            dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle29.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle29;
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle30.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle30.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle30.Format = "N3";
            dataGridViewCellStyle30.NullValue = null;
            dataGridViewCellStyle30.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle30.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgView.DefaultCellStyle = dataGridViewCellStyle30;
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
            // labelLazy
            // 
            this.labelLazy.AutoSize = true;
            this.labelLazy.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLazy.ForeColor = System.Drawing.Color.IndianRed;
            this.labelLazy.Location = new System.Drawing.Point(7, 44);
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
            this.labelShareDesc.Location = new System.Drawing.Point(7, 5);
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
            this.groupBoxCalculations.Controls.Add(this.linkLabelCalcToHere);
            this.groupBoxCalculations.Controls.Add(this.statusStrip1);
            this.groupBoxCalculations.Controls.Add(this.groupBoxParams);
            this.groupBoxCalculations.Controls.Add(this.groupBoxMisc);
            this.groupBoxCalculations.Controls.Add(this.groupBoxScope);
            this.groupBoxCalculations.Controls.Add(this.listBoxVariables);
            this.groupBoxCalculations.Location = new System.Drawing.Point(294, 3);
            this.groupBoxCalculations.Name = "groupBoxCalculations";
            this.groupBoxCalculations.Size = new System.Drawing.Size(568, 208);
            this.groupBoxCalculations.TabIndex = 15;
            this.groupBoxCalculations.TabStop = false;
            this.groupBoxCalculations.Text = "Calculations";
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
            // groupBoxMisc
            // 
            this.groupBoxMisc.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.groupBoxMisc.Controls.Add(this.checkBoxLastDay);
            this.groupBoxMisc.Controls.Add(this.checkBoxOverwriteAPs);
            this.groupBoxMisc.Enabled = false;
            this.groupBoxMisc.Location = new System.Drawing.Point(231, 112);
            this.groupBoxMisc.Name = "groupBoxMisc";
            this.groupBoxMisc.Size = new System.Drawing.Size(112, 62);
            this.groupBoxMisc.TabIndex = 14;
            this.groupBoxMisc.TabStop = false;
            this.groupBoxMisc.Visible = false;
            // 
            // checkBoxLastDay
            // 
            this.checkBoxLastDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxLastDay.AutoSize = true;
            this.checkBoxLastDay.Location = new System.Drawing.Point(10, 37);
            this.checkBoxLastDay.Name = "checkBoxLastDay";
            this.checkBoxLastDay.Size = new System.Drawing.Size(90, 17);
            this.checkBoxLastDay.TabIndex = 13;
            this.checkBoxLastDay.Text = "Last day Only";
            this.checkBoxLastDay.UseVisualStyleBackColor = true;
            // 
            // checkBoxOverwriteAPs
            // 
            this.checkBoxOverwriteAPs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxOverwriteAPs.AutoSize = true;
            this.checkBoxOverwriteAPs.Location = new System.Drawing.Point(10, 12);
            this.checkBoxOverwriteAPs.Name = "checkBoxOverwriteAPs";
            this.checkBoxOverwriteAPs.Size = new System.Drawing.Size(93, 17);
            this.checkBoxOverwriteAPs.TabIndex = 12;
            this.checkBoxOverwriteAPs.Text = "Overwrite APs";
            this.checkBoxOverwriteAPs.UseVisualStyleBackColor = true;
            // 
            // groupBoxScope
            // 
            this.groupBoxScope.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.groupBoxScope.Controls.Add(this.radioButtonThisShare);
            this.groupBoxScope.Controls.Add(this.radioButtonAllShares);
            this.groupBoxScope.Enabled = false;
            this.groupBoxScope.Location = new System.Drawing.Point(231, 11);
            this.groupBoxScope.Name = "groupBoxScope";
            this.groupBoxScope.Size = new System.Drawing.Size(112, 56);
            this.groupBoxScope.TabIndex = 9;
            this.groupBoxScope.TabStop = false;
            this.groupBoxScope.Text = "Scope";
            this.groupBoxScope.Visible = false;
            // 
            // radioButtonThisShare
            // 
            this.radioButtonThisShare.AutoSize = true;
            this.radioButtonThisShare.Checked = true;
            this.radioButtonThisShare.Location = new System.Drawing.Point(11, 35);
            this.radioButtonThisShare.Name = "radioButtonThisShare";
            this.radioButtonThisShare.Size = new System.Drawing.Size(94, 17);
            this.radioButtonThisShare.TabIndex = 1;
            this.radioButtonThisShare.TabStop = true;
            this.radioButtonThisShare.Text = "Just this Share";
            this.radioButtonThisShare.UseVisualStyleBackColor = true;
            // 
            // radioButtonAllShares
            // 
            this.radioButtonAllShares.AutoSize = true;
            this.radioButtonAllShares.Location = new System.Drawing.Point(11, 16);
            this.radioButtonAllShares.Name = "radioButtonAllShares";
            this.radioButtonAllShares.Size = new System.Drawing.Size(72, 17);
            this.radioButtonAllShares.TabIndex = 0;
            this.radioButtonAllShares.Text = "All Shares";
            this.radioButtonAllShares.UseVisualStyleBackColor = true;
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
            // linkLabelCalcToHere
            // 
            this.linkLabelCalcToHere.AutoSize = true;
            this.linkLabelCalcToHere.Location = new System.Drawing.Point(225, 85);
            this.linkLabelCalcToHere.Name = "linkLabelCalcToHere";
            this.linkLabelCalcToHere.Size = new System.Drawing.Size(118, 13);
            this.linkLabelCalcToHere.TabIndex = 16;
            this.linkLabelCalcToHere.TabStop = true;
            this.linkLabelCalcToHere.Text = "Calc All to Current Calc.";
            this.toolTip1.SetToolTip(this.linkLabelCalcToHere, "Perform calculations from  the first until the selected one in sequence, using cu" +
        "rrent values of all the parameters.");
            this.linkLabelCalcToHere.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelCalcToHere_LinkClicked);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 9000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
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
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxParams.ResumeLayout(false);
            this.groupBoxMisc.ResumeLayout(false);
            this.groupBoxMisc.PerformLayout();
            this.groupBoxScope.ResumeLayout(false);
            this.groupBoxScope.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBoxScope;
        private System.Windows.Forms.RadioButton radioButtonThisShare;
        private System.Windows.Forms.RadioButton radioButtonAllShares;
        private System.Windows.Forms.GroupBox groupBoxMisc;
        private System.Windows.Forms.CheckBox checkBoxLastDay;
        private System.Windows.Forms.CheckBox checkBoxOverwriteAPs;
        private System.Windows.Forms.GroupBox groupBoxCalculations;
        private System.Windows.Forms.LinkLabel linkLabelLock;
        private System.Windows.Forms.Label labelLazy;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stripText;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.LinkLabel linkLabelCalcToHere;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}