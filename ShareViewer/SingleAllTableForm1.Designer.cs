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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgView = new System.Windows.Forms.DataGridView();
            this.listBoxCols = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
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
            // dgView
            // 
            this.dgView.AllowUserToDeleteRows = false;
            this.dgView.AllowUserToResizeRows = false;
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgView.Location = new System.Drawing.Point(130, 0);
            this.dgView.Name = "dgView";
            this.dgView.ReadOnly = true;
            this.dgView.Size = new System.Drawing.Size(670, 450);
            this.dgView.TabIndex = 1;
            // 
            // listBoxCols
            // 
            this.listBoxCols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxCols.FormattingEnabled = true;
            this.listBoxCols.Location = new System.Drawing.Point(0, 0);
            this.listBoxCols.Name = "listBoxCols";
            this.listBoxCols.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxCols.Size = new System.Drawing.Size(130, 450);
            this.listBoxCols.TabIndex = 0;
            this.listBoxCols.SelectedIndexChanged += new System.EventHandler(this.listBoxCols_SelectedIndexChanged);
            // 
            // SingleAllTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgView);
            this.Controls.Add(this.panel1);
            this.Name = "SingleAllTableForm";
            this.Text = "Single All-Table";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SingleAllTableForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBoxCols;
        private System.Windows.Forms.DataGridView dgView;
    }
}