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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(chatmesg));
            this.Iblmessage = new System.Windows.Forms.Label();
            this.Ibltime = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Iblmessage
            // 
            this.Iblmessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Iblmessage.AutoSize = true;
            this.Iblmessage.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Iblmessage.Location = new System.Drawing.Point(42, 28);
            this.Iblmessage.Name = "Iblmessage";
            this.Iblmessage.Size = new System.Drawing.Size(51, 20);
            this.Iblmessage.TabIndex = 0;
            this.Iblmessage.Text = "label1";
            // 
            // Ibltime
            // 
            this.Ibltime.AutoSize = true;
            this.Ibltime.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Ibltime.Location = new System.Drawing.Point(168, 81);
            this.Ibltime.Name = "Ibltime";
            this.Ibltime.Size = new System.Drawing.Size(51, 20);
            this.Ibltime.TabIndex = 1;
            this.Ibltime.Text = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(57, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.pictureBox1.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pictureBox1_LoadCompleted);
            // 
            // chatmesg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(147)))));
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Ibltime);
            this.Controls.Add(this.Iblmessage);
            this.Name = "chatmesg";
            this.Size = new System.Drawing.Size(624, 134);
            this.Load += new System.EventHandler(this.chatmesg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Iblmessage;
        private System.Windows.Forms.Label Ibltime;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
