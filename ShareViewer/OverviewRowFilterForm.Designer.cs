namespace ShareViewer
{
    partial class OverviewRowFilterForm
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
            this.dgViewFilters = new System.Windows.Forms.DataGridView();
            this.buttonApply = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stripText = new System.Windows.Forms.ToolStripStatusLabel();
            this.checkBoxLazies = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewFilters)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgViewFilters
            // 
            this.dgViewFilters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgViewFilters.Location = new System.Drawing.Point(0, 0);
            this.dgViewFilters.Name = "dgViewFilters";
            this.dgViewFilters.Size = new System.Drawing.Size(264, 569);
            this.dgViewFilters.TabIndex = 1;
            this.dgViewFilters.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewFilters_CellClick);
            this.dgViewFilters.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewFilters_CellValidated);
            this.dgViewFilters.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgViewFilters_DataError);
            // 
            // buttonApply
            // 
            this.buttonApply.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonApply.Location = new System.Drawing.Point(0, 507);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(264, 40);
            this.buttonApply.TabIndex = 0;
            this.buttonApply.Text = "Apply";
            this.buttonApply.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 547);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(264, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stripText
            // 
            this.stripText.Name = "stripText";
            this.stripText.Size = new System.Drawing.Size(16, 17);
            this.stripText.Text = "...";
            // 
            // checkBoxLazies
            // 
            this.checkBoxLazies.AutoSize = true;
            this.checkBoxLazies.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.checkBoxLazies.Location = new System.Drawing.Point(0, 490);
            this.checkBoxLazies.Name = "checkBoxLazies";
            this.checkBoxLazies.Size = new System.Drawing.Size(264, 17);
            this.checkBoxLazies.TabIndex = 3;
            this.checkBoxLazies.Text = "exclude Lazy shares";
            this.checkBoxLazies.UseVisualStyleBackColor = true;
            // 
            // OverviewRowFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(264, 569);
            this.Controls.Add(this.checkBoxLazies);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dgViewFilters);
            this.MaximizeBox = false;
            this.Name = "OverviewRowFilterForm";
            this.ShowIcon = false;
            this.Text = "Filters";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OverviewRowFilterForm_FormClosing);
            this.Load += new System.EventHandler(this.OverviewRowFilterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewFilters)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgViewFilters;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stripText;
        private System.Windows.Forms.CheckBox checkBoxLazies;
    }
}