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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.listBoxCols = new System.Windows.Forms.ListBox();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.panelTop = new System.Windows.Forms.Panel();
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
            this.groupBoxParams = new System.Windows.Forms.GroupBox();
            this.groupBoxMisc = new System.Windows.Forms.GroupBox();
            this.checkBoxLastDay = new System.Windows.Forms.CheckBox();
            this.checkBoxOverwriteAPs = new System.Windows.Forms.CheckBox();
            this.groupBoxScope = new System.Windows.Forms.GroupBox();
            this.radioButtonThisShare = new System.Windows.Forms.RadioButton();
            this.radioButtonAllShares = new System.Windows.Forms.RadioButton();
            this.listBoxVariables = new System.Windows.Forms.ListBox();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.panelTop.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxCalculations.SuspendLayout();
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
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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
            // linkLabelLock
            // 
            this.linkLabelLock.AutoSize = true;
            this.linkLabelLock.Location = new System.Drawing.Point(7, 38);
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
            this.label1.Location = new System.Drawing.Point(7, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "<-- ctrl-click to add/remove columns";
            // 
            // groupBoxCalculations
            // 
            this.groupBoxCalculations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            // groupBoxParams
            // 
            this.groupBoxParams.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxParams.Location = new System.Drawing.Point(349, 0);
            this.groupBoxParams.Name = "groupBoxParams";
            this.groupBoxParams.Size = new System.Drawing.Size(209, 206);
            this.groupBoxParams.TabIndex = 13;
            this.groupBoxParams.TabStop = false;
            // 
            // groupBoxMisc
            // 
            this.groupBoxMisc.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.groupBoxMisc.Controls.Add(this.checkBoxLastDay);
            this.groupBoxMisc.Controls.Add(this.checkBoxOverwriteAPs);
            this.groupBoxMisc.Location = new System.Drawing.Point(231, 93);
            this.groupBoxMisc.Name = "groupBoxMisc";
            this.groupBoxMisc.Size = new System.Drawing.Size(112, 87);
            this.groupBoxMisc.TabIndex = 14;
            this.groupBoxMisc.TabStop = false;
            // 
            // checkBoxLastDay
            // 
            this.checkBoxLastDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxLastDay.AutoSize = true;
            this.checkBoxLastDay.Location = new System.Drawing.Point(10, 44);
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
            this.checkBoxOverwriteAPs.Location = new System.Drawing.Point(10, 19);
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
            this.groupBoxScope.Location = new System.Drawing.Point(231, 19);
            this.groupBoxScope.Name = "groupBoxScope";
            this.groupBoxScope.Size = new System.Drawing.Size(112, 66);
            this.groupBoxScope.TabIndex = 9;
            this.groupBoxScope.TabStop = false;
            this.groupBoxScope.Text = "Scope";
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
            this.listBoxVariables.Items.AddRange(new object[] {
            "Delete Lazy shares",
            "Make Slow (Five minutes) Prices SP",
            "Find direction and Turning",
            "Find Five minutes Gradients Figure PGF",
            "Make High Line HL",
            "Make Low Line LL",
            "Make Slow Volumes SV",
            "Slow Volume Figure SVFac",
            "Slow Volume Figure SVFbd"});
            this.listBoxVariables.Location = new System.Drawing.Point(13, 15);
            this.listBoxVariables.Name = "listBoxVariables";
            this.listBoxVariables.Size = new System.Drawing.Size(207, 160);
            this.listBoxVariables.TabIndex = 11;
            this.listBoxVariables.SelectedIndexChanged += new System.EventHandler(this.listBoxVariables_SelectedIndexChanged);
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
    }
}