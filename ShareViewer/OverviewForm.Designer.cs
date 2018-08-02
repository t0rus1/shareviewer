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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.stripText = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgOverview = new System.Windows.Forms.DataGridView();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOverview)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripText});
            this.statusStrip.Location = new System.Drawing.Point(0, 665);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1101, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip";
            // 
            // stripText
            // 
            this.stripText.Name = "stripText";
            this.stripText.Size = new System.Drawing.Size(22, 17);
            this.stripText.Text = "Ok";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1101, 100);
            this.panel1.TabIndex = 1;
            // 
            // dgOverview
            // 
            this.dgOverview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgOverview.Location = new System.Drawing.Point(0, 100);
            this.dgOverview.Name = "dgOverview";
            this.dgOverview.Size = new System.Drawing.Size(1101, 565);
            this.dgOverview.TabIndex = 2;
            // 
            // OverviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 687);
            this.Controls.Add(this.dgOverview);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip);
            this.Name = "OverviewForm";
            this.Text = "Overview";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.OverviewForm_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOverview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel stripText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgOverview;
    }
}