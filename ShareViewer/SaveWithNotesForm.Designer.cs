namespace ShareViewer
{
    partial class SaveWithNotesForm
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
            this.textBoxNotes = new System.Windows.Forms.TextBox();
            this.labelNotes = new System.Windows.Forms.Label();
            this.buttonYes = new System.Windows.Forms.Button();
            this.linkLabelSummarizeParams = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // textBoxNotes
            // 
            this.textBoxNotes.AcceptsReturn = true;
            this.textBoxNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNotes.Location = new System.Drawing.Point(7, 21);
            this.textBoxNotes.Multiline = true;
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxNotes.Size = new System.Drawing.Size(765, 259);
            this.textBoxNotes.TabIndex = 0;
            // 
            // labelNotes
            // 
            this.labelNotes.AutoSize = true;
            this.labelNotes.Location = new System.Drawing.Point(4, 5);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(35, 13);
            this.labelNotes.TabIndex = 1;
            this.labelNotes.Text = "Notes";
            // 
            // buttonYes
            // 
            this.buttonYes.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonYes.Location = new System.Drawing.Point(665, 294);
            this.buttonYes.Name = "buttonYes";
            this.buttonYes.Size = new System.Drawing.Size(107, 66);
            this.buttonYes.TabIndex = 2;
            this.buttonYes.Text = "Add Notes";
            this.buttonYes.UseVisualStyleBackColor = true;
            // 
            // linkLabelSummarizeParams
            // 
            this.linkLabelSummarizeParams.AutoSize = true;
            this.linkLabelSummarizeParams.Location = new System.Drawing.Point(10, 294);
            this.linkLabelSummarizeParams.Name = "linkLabelSummarizeParams";
            this.linkLabelSummarizeParams.Size = new System.Drawing.Size(212, 13);
            this.linkLabelSummarizeParams.TabIndex = 3;
            this.linkLabelSummarizeParams.TabStop = true;
            this.linkLabelSummarizeParams.Text = "Include CURRENT Parameters in the notes";
            this.linkLabelSummarizeParams.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSummarizeParams_LinkClicked);
            // 
            // SaveWithNotesForm
            // 
            this.AcceptButton = this.buttonYes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 387);
            this.Controls.Add(this.linkLabelSummarizeParams);
            this.Controls.Add(this.buttonYes);
            this.Controls.Add(this.labelNotes);
            this.Controls.Add(this.textBoxNotes);
            this.Name = "SaveWithNotesForm";
            this.Text = "Add Notes to Saved Overview";
            this.Load += new System.EventHandler(this.SaveWithNotesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelNotes;
        private System.Windows.Forms.Button buttonYes;
        public System.Windows.Forms.TextBox textBoxNotes;
        private System.Windows.Forms.LinkLabel linkLabelSummarizeParams;
    }
}