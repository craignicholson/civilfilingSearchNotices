namespace civilfiling_searchNotices
{
    partial class fmrMain
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
            this.dtSearchNotices = new System.Windows.Forms.DateTimePicker();
            this.rtbSearchNotices = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtSearchNotices
            // 
            this.dtSearchNotices.Location = new System.Drawing.Point(23, 34);
            this.dtSearchNotices.Name = "dtSearchNotices";
            this.dtSearchNotices.Size = new System.Drawing.Size(765, 31);
            this.dtSearchNotices.TabIndex = 1;
            // 
            // rtbSearchNotices
            // 
            this.rtbSearchNotices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbSearchNotices.Location = new System.Drawing.Point(23, 135);
            this.rtbSearchNotices.Name = "rtbSearchNotices";
            this.rtbSearchNotices.Size = new System.Drawing.Size(1323, 625);
            this.rtbSearchNotices.TabIndex = 2;
            this.rtbSearchNotices.Text = "";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1156, 798);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 92);
            this.button1.TabIndex = 3;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_ClickAsync);
            // 
            // fmrMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1371, 917);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rtbSearchNotices);
            this.Controls.Add(this.dtSearchNotices);
            this.Name = "fmrMain";
            this.Text = "Search Notices";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dtSearchNotices;
        private System.Windows.Forms.RichTextBox rtbSearchNotices;
        private System.Windows.Forms.Button button1;
    }
}

