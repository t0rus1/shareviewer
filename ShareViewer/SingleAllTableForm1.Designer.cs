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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBoxCols = new System.Windows.Forms.ListBox();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioDays = new System.Windows.Forms.RadioButton();
            this.radioHours = new System.Windows.Forms.RadioButton();
            this.radio5mins = new System.Windows.Forms.RadioButton();
            this.buttonSaveView = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonInitialView = new System.Windows.Forms.Button();
            this.radioWeekly = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listBoxCols);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(130, 670);
            this.panel1.TabIndex = 0;
            // 
            // listBoxCols
            // 
            this.listBoxCols.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBoxCols.FormattingEnabled = true;
            this.listBoxCols.Location = new System.Drawing.Point(0, 0);
            this.listBoxCols.Name = "listBoxCols";
            this.listBoxCols.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxCols.Size = new System.Drawing.Size(130, 670);
            this.listBoxCols.TabIndex = 0;
            this.listBoxCols.SelectedIndexChanged += new System.EventHandler(this.listBoxCols_SelectedIndexChanged);
            // 
            // dgView
            // 
            this.dgView.AllowUserToDeleteRows = false;
            this.dgView.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgView.EnableHeadersVisualStyles = false;
            this.dgView.Location = new System.Drawing.Point(130, 106);
            this.dgView.Name = "dgView";
            this.dgView.ReadOnly = true;
            this.dgView.Size = new System.Drawing.Size(670, 564);
            this.dgView.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.buttonSaveView);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.buttonInitialView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(130, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(670, 106);
            this.panel2.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioWeekly);
            this.groupBox2.Controls.Add(this.radioDays);
            this.groupBox2.Controls.Add(this.radioHours);
            this.groupBox2.Controls.Add(this.radio5mins);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(567, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(103, 106);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Vertically";
            // 
            // radioDays
            // 
            this.radioDays.AutoSize = true;
            this.radioDays.Location = new System.Drawing.Point(8, 60);
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
            this.radioHours.Location = new System.Drawing.Point(8, 38);
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
            // buttonSaveView
            // 
            this.buttonSaveView.Location = new System.Drawing.Point(113, 13);
            this.buttonSaveView.Name = "buttonSaveView";
            this.buttonSaveView.Size = new System.Drawing.Size(96, 23);
            this.buttonSaveView.TabIndex = 4;
            this.buttonSaveView.Text = "Save view as...";
            this.buttonSaveView.UseVisualStyleBackColor = true;
            this.buttonSaveView.Click += new System.EventHandler(this.buttonSaveView_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "<-- ctrl-click to add/remove columns";
            // 
            // buttonInitialView
            // 
            this.buttonInitialView.Location = new System.Drawing.Point(11, 10);
            this.buttonInitialView.Name = "buttonInitialView";
            this.buttonInitialView.Size = new System.Drawing.Size(75, 28);
            this.buttonInitialView.TabIndex = 0;
            this.buttonInitialView.Text = "Initial View";
            this.buttonInitialView.UseVisualStyleBackColor = true;
            this.buttonInitialView.Click += new System.EventHandler(this.buttonInitialView_Click);
            // 
            // radioWeekly
            // 
            this.radioWeekly.AutoSize = true;
            this.radioWeekly.Location = new System.Drawing.Point(8, 82);
            this.radioWeekly.Name = "radioWeekly";
            this.radioWeekly.Size = new System.Drawing.Size(93, 17);
            this.radioWeekly.TabIndex = 3;
            this.radioWeekly.TabStop = true;
            this.radioWeekly.Text = "Weekly bands";
            this.radioWeekly.UseVisualStyleBackColor = true;
            this.radioWeekly.CheckedChanged += new System.EventHandler(this.radioWeekly_CheckedChanged);
            // 
            // SingleAllTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 670);
            this.Controls.Add(this.dgView);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "SingleAllTableForm";
            this.Text = "Single All-Table";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SingleAllTableForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBoxCols;
        private System.Windows.Forms.DataGridView dgView;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonInitialView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSaveView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioHours;
        private System.Windows.Forms.RadioButton radio5mins;
        private System.Windows.Forms.RadioButton radioDays;
        private System.Windows.Forms.RadioButton radioWeekly;
    }
}