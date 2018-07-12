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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBoxCols = new System.Windows.Forms.ListBox();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonInitialView = new System.Windows.Forms.Button();
            this.labelCurrentDateRange = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listBoxCols);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(130, 450);
            this.panel1.TabIndex = 0;
            // 
            // listBoxCols
            // 
            this.listBoxCols.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBoxCols.FormattingEnabled = true;
            this.listBoxCols.Location = new System.Drawing.Point(0, 0);
            this.listBoxCols.Name = "listBoxCols";
            this.listBoxCols.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxCols.Size = new System.Drawing.Size(130, 450);
            this.listBoxCols.TabIndex = 0;
            this.listBoxCols.SelectedIndexChanged += new System.EventHandler(this.listBoxCols_SelectedIndexChanged);
            // 
            // dgView
            // 
            this.dgView.AllowUserToDeleteRows = false;
            this.dgView.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgView.Location = new System.Drawing.Point(130, 82);
            this.dgView.Name = "dgView";
            this.dgView.ReadOnly = true;
            this.dgView.Size = new System.Drawing.Size(670, 368);
            this.dgView.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.labelCurrentDateRange);
            this.panel2.Controls.Add(this.buttonInitialView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(130, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(670, 82);
            this.panel2.TabIndex = 2;
            // 
            // buttonInitialView
            // 
            this.buttonInitialView.Location = new System.Drawing.Point(11, 10);
            this.buttonInitialView.Name = "buttonInitialView";
            this.buttonInitialView.Size = new System.Drawing.Size(75, 42);
            this.buttonInitialView.TabIndex = 0;
            this.buttonInitialView.Text = "Initial View";
            this.buttonInitialView.UseVisualStyleBackColor = true;
            this.buttonInitialView.Click += new System.EventHandler(this.buttonInitialView_Click);
            // 
            // labelCurrentDateRange
            // 
            this.labelCurrentDateRange.AutoSize = true;
            this.labelCurrentDateRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrentDateRange.Location = new System.Drawing.Point(95, 10);
            this.labelCurrentDateRange.Name = "labelCurrentDateRange";
            this.labelCurrentDateRange.Size = new System.Drawing.Size(140, 20);
            this.labelCurrentDateRange.TabIndex = 1;
            this.labelCurrentDateRange.Text = "current date range";
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
            // SingleAllTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBoxCols;
        private System.Windows.Forms.DataGridView dgView;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonInitialView;
        private System.Windows.Forms.Label labelCurrentDateRange;
        private System.Windows.Forms.Label label1;
    }
}