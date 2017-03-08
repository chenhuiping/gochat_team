using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCP_handle;

namespace chat_list
{
    public partial class Form1 : Form
    {
        //defining loginForm
        public Login loginForm;
        public Form1(Login loginForm)
        {
            this.loginForm = loginForm;
            InitializeComponent();

            this.chatbox1 = new chat_list.chatbox(loginForm);
            this.chat.Controls.Add(this.chatbox1);

            //this.chatbox1 = new chat_list.chatbox(loginForm);
            this.chatbox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chatbox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatbox1.Location = new System.Drawing.Point(386, 0);
            this.chatbox1.Name = "chatbox1";
            this.chatbox1.Size = new System.Drawing.Size(717, 707);
            this.chatbox1.TabIndex = 0;


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        
        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chatbox1.Dispose();
            this.chatbox1 = new chat_list.chatbox(this.loginForm);
            this.chat.Controls.Add(this.chatbox1);
            this.chatbox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chatbox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatbox1.Location = new System.Drawing.Point(386, 0);
            this.chatbox1.Name = "chatbox1";
            this.chatbox1.Size = new System.Drawing.Size(717, 600);
            this.chatbox1.TabIndex = 0;
        }

        //to move windows
        private bool mouseDown;
        private Point lastLocation;
        
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        
        private void header1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void header1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void header1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        //Log Out Button Function
        private void label1_Click(object sender, EventArgs e)
        {
            loginForm.Show();
            this.Dispose();
        }

        private void container_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            
        }
    }
}
