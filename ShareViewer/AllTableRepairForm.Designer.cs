namespace ShareViewer
{
    partial class AllTableRepairForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllTableRepairForm));
            this.listBoxRepairShares = new System.Windows.Forms.ListBox();
            this.buttonRepair = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listBoxRepairShares
            // 
            this.listBoxRepairShares.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBoxRepairShares.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxRepairShares.FormattingEnabled = true;
            this.listBoxRepairShares.ItemHeight = 11;
            this.listBoxRepairShares.Location = new System.Drawing.Point(0, 0);
            this.listBoxRepairShares.Name = "listBoxRepairShares";
            this.listBoxRepairShares.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxRepairShares.Size = new System.Drawing.Size(266, 554);
            this.listBoxRepairShares.TabIndex = 0;
            // 
            // buttonRepair
            // 
            this.buttonRepair.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonRepair.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonRepair.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRepair.Image = ((System.Drawing.Image)(resources.GetObject("buttonRepair.Image")));
            this.buttonRepair.Location = new System.Drawing.Point(266, 447);
            this.buttonRepair.Name = "buttonRepair";
            this.buttonRepair.Size = new System.Drawing.Size(173, 107);
            this.buttonRepair.TabIndex = 1;
            this.buttonRepair.Text = "Regenerate AllTables for selected shares";
            this.buttonRepair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonRepair.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Red;
            this.textBox1.Location = new System.Drawing.Point(283, 227);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(144, 188);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "!!!Caution!!! \r\n\r\nA full 100 trading days (which MUST correspond to the same trad" +
    "ing day range of the other All-Tables) must  ALREADY be set on the calendars) ";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(283, 26);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(144, 63);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "Select the shares for which you wish new All-Tables to be generated";
            // 
            // AllTableRepairForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 554);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonRepair);
            this.Controls.Add(this.listBoxRepairShares);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AllTableRepairForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Repair AllTable(s)";
            this.Load += new System.EventHandler(this.AllTableRepairForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonRepair;
        internal System.Windows.Forms.ListBox listBoxRepairShares;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
    }
}