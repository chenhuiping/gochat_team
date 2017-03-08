namespace server
{
    partial class Form1
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.txtPNumber = new System.Windows.Forms.TextBox();
            this.Start = new System.Windows.Forms.Button();
            this.End = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(-7, 108);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(746, 646);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // txtPNumber
            // 
            this.txtPNumber.Location = new System.Drawing.Point(27, 36);
            this.txtPNumber.Name = "txtPNumber";
            this.txtPNumber.Size = new System.Drawing.Size(209, 31);
            this.txtPNumber.TabIndex = 1;
            this.txtPNumber.Text = "9000";
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(264, 18);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(129, 66);
            this.Start.TabIndex = 2;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // End
            // 
            this.End.Location = new System.Drawing.Point(399, 18);
            this.End.Name = "End";
            this.End.Size = new System.Drawing.Size(129, 66);
            this.End.TabIndex = 3;
            this.End.Text = "End";
            this.End.UseVisualStyleBackColor = true;
            this.End.Click += new System.EventHandler(this.End_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(534, 18);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(129, 66);
            this.button3.TabIndex = 4;
            this.button3.Text = "send";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 756);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.End);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.txtPNumber);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox txtPNumber;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button End;
        private System.Windows.Forms.Button button3;
    }
}

