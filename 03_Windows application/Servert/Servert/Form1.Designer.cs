namespace Servert
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
            this.btn_strat = new System.Windows.Forms.Button();
            this.btn_end = new System.Windows.Forms.Button();
            this.txtPNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_strat
            // 
            this.btn_strat.Location = new System.Drawing.Point(299, 34);
            this.btn_strat.Name = "btn_strat";
            this.btn_strat.Size = new System.Drawing.Size(104, 54);
            this.btn_strat.TabIndex = 0;
            this.btn_strat.Text = "start";
            this.btn_strat.UseVisualStyleBackColor = true;
            this.btn_strat.Click += new System.EventHandler(this.btn_strat_Click);
            // 
            // btn_end
            // 
            this.btn_end.Location = new System.Drawing.Point(299, 104);
            this.btn_end.Name = "btn_end";
            this.btn_end.Size = new System.Drawing.Size(104, 62);
            this.btn_end.TabIndex = 1;
            this.btn_end.Text = "end";
            this.btn_end.UseVisualStyleBackColor = true;
            this.btn_end.Click += new System.EventHandler(this.btn_end_Click);
            // 
            // txtPNumber
            // 
            this.txtPNumber.Location = new System.Drawing.Point(144, 70);
            this.txtPNumber.Name = "txtPNumber";
            this.txtPNumber.Size = new System.Drawing.Size(100, 28);
            this.txtPNumber.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "port";
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(37, 197);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(373, 158);
            this.txtMsg.TabIndex = 4;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(37, 115);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(117, 51);
            this.btn_send.TabIndex = 5;
            this.btn_send.Text = "send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 426);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPNumber);
            this.Controls.Add(this.btn_end);
            this.Controls.Add(this.btn_strat);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_strat;
        private System.Windows.Forms.Button btn_end;
        private System.Windows.Forms.TextBox txtPNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.Button btn_send;
    }
}

