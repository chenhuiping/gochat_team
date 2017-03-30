using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chat_list
{
    public partial class SearchFriend : Form
    {
        // define mainform
        public Form1 MainForm;

        // define user details
        public UserDetail userDetail2;

        public SearchFriend(Form1 MainForm)
        {
            this.MainForm = MainForm;
            
            InitializeComponent();
            // 
            // userDetail2
            // 
            this.userDetail2 = new chat_list.UserDetail(this.MainForm);
            this.userDetail2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(164)))), ((int)(((byte)(147)))));
            this.userDetail2.Location = new System.Drawing.Point(0, 0);
            this.userDetail2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.userDetail2.Name = "userDetail2";
            this.userDetail2.Size = new System.Drawing.Size(493, 617);
            this.userDetail2.TabIndex = 0;
            this.userDetail2.Dock = System.Windows.Forms.DockStyle.Fill;


            this.panel1.Controls.Add(this.userDetail2);
        }

        //--------------------------------------------------------------------------------------------
        internal void userdetail_fromLogin(Login.user temp)
        {
            userDetail2.setUserData(temp);
            //userDetail2.set_image("ws://47.91.75.150:1337/" + temp.profile);
            //userDetail2.set_userID(temp.UserName);

        }
        //------------------------------------------------------------------------------------------
        // close the window
        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        //------------------------------------------------------------------------------------------
        // search button
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            MainForm.searchUser(textBox1.Text);
        }
        //---------------------------------------------------------------------------------------------------------------- 
        //make the windows move
        private bool mouseDown;
        private Point lastLocation;

        private void SearchFriend_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void SearchFriend_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void SearchFriend_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }
    }
}
