﻿namespace chat_list
{
    partial class friend_item
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(friend_item));
            this.userName = new System.Windows.Forms.Label();
            this.userInformation = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // userName
            // 
            this.userName.AutoSize = true;
            this.userName.Location = new System.Drawing.Point(23, 116);
            this.userName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(70, 25);
            this.userName.TabIndex = 0;
            this.userName.Text = "label1";
            // 
            // userInformation
            // 
            this.userInformation.AutoSize = true;
            this.userInformation.Location = new System.Drawing.Point(175, 18);
            this.userInformation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.userInformation.Name = "userInformation";
            this.userInformation.Size = new System.Drawing.Size(70, 25);
            this.userInformation.TabIndex = 1;
            this.userInformation.Text = "label2";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(28, 18);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(63, 81);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // friend_item
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.userInformation);
            this.Controls.Add(this.userName);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "friend_item";
            this.Size = new System.Drawing.Size(440, 155);
            this.Click += new System.EventHandler(this.friend_item_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.friend_item_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label userName;
        private System.Windows.Forms.Label userInformation;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
