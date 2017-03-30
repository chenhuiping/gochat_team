using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace chat_list
{
    public partial class sign_up : Form
    {
        public Login loginForm;
        private string username;
        private string password;
        private string profile;

        private static string fileName = "";

        public sign_up(Login loginForm)
        {
            this.loginForm = loginForm;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
         private bool mouseDown;
        private Point lastLocation;
        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG|*.jpg|GIF|*.gif|PNG|*.png|BMP|*.bmp";
            dlg.Title = "File Sharing Client";
            dlg.ShowDialog();
            //txtFile.Text = dlg.FileName;
            fileName = dlg.FileName;
            photo.ImageLocation = fileName;
            string shortFileName = Path.GetFileName(fileName);
            //Task.Factory.StartNew(() => SendFile(ipAddress, port, fileName, shortFileName));
            //MessageBox.Show("File Sent");
            
        }

        private void label6_Click(object sender, EventArgs e)
        {
            loginForm.Show();
            this.Dispose();
        }
        //---------------------------------------------------------------------------------------------
        // 'sign up' button

        public delegate void createAccount(string username, string password, string profile);
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            
            
            // send user details to server
            //loginForm.sendFile(fileName, shortFileName);
            this.loginForm.getSendMessage = UName.Text;
            if (pw1.Text == pw2.Text)
            {
                username = UName.Text;
                password = pw1.Text;
                profile = fileName;
                this.Invoke(new createAccount(loginForm.createNewAccount), username, password, profile);
               // this.loginForm.getSendMessage = pw1.Text;

            }
            // open connection
            loginForm.newConnectWs();

            // close this form
            loginForm.Show();
            this.Dispose();
        }
        
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            UName.Text = String.Empty;
            pw1.Text = String.Empty;
            pw2.Text = String.Empty;
        }
    }
}
