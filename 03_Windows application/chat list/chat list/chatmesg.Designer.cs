namespace chat_list
{
    partial class chatmesg
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Iblmessage = new System.Windows.Forms.Label();
            this.Ibltime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Iblmessage
            // 
            this.Iblmessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Iblmessage.AutoSize = true;
            this.Iblmessage.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Iblmessage.Location = new System.Drawing.Point(40, 22);
            this.Iblmessage.Name = "Iblmessage";
            this.Iblmessage.Size = new System.Drawing.Size(62, 18);
            this.Iblmessage.TabIndex = 0;
            this.Iblmessage.Text = "label1";
            // 
            // Ibltime
            // 
            this.Ibltime.AutoSize = true;
            this.Ibltime.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Ibltime.Location = new System.Drawing.Point(99, 62);
            this.Ibltime.Name = "Ibltime";
            this.Ibltime.Size = new System.Drawing.Size(62, 18);
            this.Ibltime.TabIndex = 1;
            this.Ibltime.Text = "label1";
            // 
            // chatmesg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(147)))));
            this.Controls.Add(this.Ibltime);
            this.Controls.Add(this.Iblmessage);
            this.Name = "chatmesg";
            this.Size = new System.Drawing.Size(624, 121);
            this.Load += new System.EventHandler(this.chatmesg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Iblmessage;
        private System.Windows.Forms.Label Ibltime;
    }
}
