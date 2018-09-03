namespace ShareViewer
{
    partial class AutoOpsForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelUpToDateStatus = new System.Windows.Forms.Label();
            this.labelLastDate = new System.Windows.Forms.Label();
            this.labelLastIntake = new System.Windows.Forms.Label();
            this.dtInsteadDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCatchUp = new System.Windows.Forms.Button();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.stripText = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.groupBoxMode = new System.Windows.Forms.GroupBox();
            this.radioButtonManual = new System.Windows.Forms.RadioButton();
            this.radioButtonAuto = new System.Windows.Forms.RadioButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dgOps = new System.Windows.Forms.DataGridView();
            this.timerAuto = new System.Windows.Forms.Timer(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelUpToDateStatus);
            this.panel1.Controls.Add(this.labelLastDate);
            this.panel1.Controls.Add(this.labelLastIntake);
            this.panel1.Controls.Add(this.dtInsteadDate);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonCatchUp);
            this.panel1.Controls.Add(this.statusStrip2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBoxMode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(890, 100);
            this.panel1.TabIndex = 0;
            // 
            // labelUpToDateStatus
            // 
            this.labelUpToDateStatus.AutoSize = true;
            this.labelUpToDateStatus.Location = new System.Drawing.Point(353, 28);
            this.labelUpToDateStatus.Name = "labelUpToDateStatus";
            this.labelUpToDateStatus.Size = new System.Drawing.Size(61, 13);
            this.labelUpToDateStatus.TabIndex = 8;
            this.labelUpToDateStatus.Text = "up to date?";
            // 
            // labelLastDate
            // 
            this.labelLastDate.AutoSize = true;
            this.labelLastDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLastDate.ForeColor = System.Drawing.Color.Green;
            this.labelLastDate.Location = new System.Drawing.Point(412, 7);
            this.labelLastDate.Name = "labelLastDate";
            this.labelLastDate.Size = new System.Drawing.Size(20, 17);
            this.labelLastDate.TabIndex = 7;
            this.labelLastDate.Text = "...";
            // 
            // labelLastIntake
            // 
            this.labelLastIntake.AutoSize = true;
            this.labelLastIntake.Location = new System.Drawing.Point(352, 9);
            this.labelLastIntake.Name = "labelLastIntake";
            this.labelLastIntake.Size = new System.Drawing.Size(55, 13);
            this.labelLastIntake.TabIndex = 6;
            this.labelLastIntake.Text = "last intake";
            // 
            // dtInsteadDate
            // 
            this.dtInsteadDate.Location = new System.Drawing.Point(351, 46);
            this.dtInsteadDate.Name = "dtInsteadDate";
            this.dtInsteadDate.Size = new System.Drawing.Size(227, 20);
            this.dtInsteadDate.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(718, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Under development!!!";
            // 
            // buttonCatchUp
            // 
            this.buttonCatchUp.Location = new System.Drawing.Point(583, 45);
            this.buttonCatchUp.Name = "buttonCatchUp";
            this.buttonCatchUp.Size = new System.Drawing.Size(282, 24);
            this.buttonCatchUp.TabIndex = 3;
            this.buttonCatchUp.Text = "<--- (Start) catch up by processing for this date manually";
            this.buttonCatchUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCatchUp.UseVisualStyleBackColor = true;
            this.buttonCatchUp.Click += new System.EventHandler(this.buttonCatchUp_Click);
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripText});
            this.statusStrip2.Location = new System.Drawing.Point(0, 78);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(890, 22);
            this.statusStrip2.SizingGrip = false;
            this.statusStrip2.TabIndex = 2;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // stripText
            // 
            this.stripText.Name = "stripText";
            this.stripText.Size = new System.Drawing.Size(22, 17);
            this.stripText.Text = "Ok";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelTime);
            this.groupBox1.Controls.Add(this.labelDate);
            this.groupBox1.Location = new System.Drawing.Point(170, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(166, 59);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Date | Time";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(10, 37);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(26, 13);
            this.labelTime.TabIndex = 1;
            this.labelTime.Text = "time";
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(10, 18);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(28, 13);
            this.labelDate.TabIndex = 0;
            this.labelDate.Text = "date";
            // 
            // groupBoxMode
            // 
            this.groupBoxMode.Controls.Add(this.radioButtonManual);
            this.groupBoxMode.Controls.Add(this.radioButtonAuto);
            this.groupBoxMode.Location = new System.Drawing.Point(17, 10);
            this.groupBoxMode.Name = "groupBoxMode";
            this.groupBoxMode.Size = new System.Drawing.Size(147, 59);
            this.groupBoxMode.TabIndex = 0;
            this.groupBoxMode.TabStop = false;
            this.groupBoxMode.Text = "Mode";
            // 
            // radioButtonManual
            // 
            this.radioButtonManual.AutoCheck = false;
            this.radioButtonManual.AutoSize = true;
            this.radioButtonManual.Checked = true;
            this.radioButtonManual.Location = new System.Drawing.Point(70, 24);
            this.radioButtonManual.Name = "radioButtonManual";
            this.radioButtonManual.Size = new System.Drawing.Size(60, 17);
            this.radioButtonManual.TabIndex = 1;
            this.radioButtonManual.TabStop = true;
            this.radioButtonManual.Text = "Manual";
            this.radioButtonManual.UseVisualStyleBackColor = true;
            this.radioButtonManual.Click += new System.EventHandler(this.radioButtonManual_Click);
            // 
            // radioButtonAuto
            // 
            this.radioButtonAuto.AutoCheck = false;
            this.radioButtonAuto.AutoSize = true;
            this.radioButtonAuto.Location = new System.Drawing.Point(14, 24);
            this.radioButtonAuto.Name = "radioButtonAuto";
            this.radioButtonAuto.Size = new System.Drawing.Size(47, 17);
            this.radioButtonAuto.TabIndex = 0;
            this.radioButtonAuto.Text = "Auto";
            this.radioButtonAuto.UseVisualStyleBackColor = true;
            this.radioButtonAuto.Click += new System.EventHandler(this.radioButtonAuto_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 472);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(890, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dgOps
            // 
            this.dgOps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgOps.Location = new System.Drawing.Point(0, 100);
            this.dgOps.Name = "dgOps";
            this.dgOps.Size = new System.Drawing.Size(890, 372);
            this.dgOps.TabIndex = 3;
            // 
            // timerAuto
            // 
            this.timerAuto.Interval = 1000;
            this.timerAuto.Tick += new System.EventHandler(this.timerAuto_Tick);
            // 
            // AutoOpsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 494);
            this.Controls.Add(this.dgOps);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.MinimizeBox = false;
            this.Name = "AutoOpsForm";
            this.Text = "ShareViewer Auto Ops";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AutoOpsForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxMode.ResumeLayout(false);
            this.groupBoxMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView dgOps;
        private System.Windows.Forms.GroupBox groupBoxMode;
        private System.Windows.Forms.RadioButton radioButtonManual;
        private System.Windows.Forms.RadioButton radioButtonAuto;
        private System.Windows.Forms.Timer timerAuto;
        private System.Windows.Forms.Button buttonCatchUp;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel stripText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtInsteadDate;
        private System.Windows.Forms.Label labelLastDate;
        private System.Windows.Forms.Label labelLastIntake;
        private System.Windows.Forms.Label labelUpToDateStatus;
    }
}