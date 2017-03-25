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
        //public UserDetails userDetailsForm;

        public SearchFriend(Form1 MainForm)
        {
            this.MainForm = MainForm;
            
            InitializeComponent();
        }

        // close button
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //-----------------------------------------------------------------------------------------------------------------
        // search button
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // search function here! -> not finished
            MainForm.searchUser(textBox1.Text);
            
            // display the result
            //userDetailsForm = new UserDetails(this.MainForm);
            //userDetailsForm.Show();
            //userDetailsForm.label1.Text = textBox1.Text;
            //this.Close();
        }
        //----------------------------------------------------------------------------------------------------------------
        // add button
        private void userDetail1_Load(object sender, EventArgs e)
        {
            //MainForm.addFriend();
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

       
    }
}
