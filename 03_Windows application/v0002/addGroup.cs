using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chat_list
{
    public partial class addGroup : Form
    {
        public Form1 MainForm;

        public friend_list member_list;
        public addGroup(Form1 MainForm)
        {
            InitializeComponent();

            this.MainForm = MainForm;
            
            // initialize friend list user control
            this.member_list = new chat_list.friend_list(this.MainForm);
            this.member_list.AutoScroll = true;
            this.member_list.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.member_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.member_list.getReceiveMessage = null;
            this.member_list.Location = new System.Drawing.Point(0, 0);
            this.member_list.Name = "member_list";
            this.member_list.Size = new System.Drawing.Size(372, 645);
            this.member_list.TabIndex = 1; //0


            // panel1
            //
            this.panel3.Controls.Add(this.member_list);
            
        }
        //----------------------------------------------------------------------------------------------------------------
        // print friend
        public void printFriend()
        {
            //Debug.WriteLine(this.member_list.)
        }
        //----------------------------------------------------------------------------------------------------------------
        // close button
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            foreach (var temp in this.MainForm.friend_list1.store_list)
            {
                temp.BackColor = Color.FromArgb(46, 50, 56);
            }
            this.MainForm.add_grouplist = false;

            this.Close();
        }

        // 'create group' button
        public delegate void callPassMember();
        private void button1_Click(object sender, EventArgs e)
        {
            this.MainForm.add_grouplist = false;
            this.Invoke(new callPassMember(MainForm.passMemberID));
            this.MainForm.Show();
            this.MainForm.add_grouplist = false;
            foreach(var temp in this.MainForm.friend_list1.store_list)
            {
                temp.BackColor = Color.FromArgb(46, 50, 56);
            }
            
            this.Close();
        }
        //---------------------------------------------------------------------------------------------------------------- 
        //make the windows move
        private bool mouseDown;
        private Point lastLocation;
                        
        private void addGroup_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void addGroup_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void addGroup_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void header_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void header_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void header_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        //-----------------------------------------------------------------------------------------------------------------------
    }
}
