using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace chat_list
{
    public partial class UserDetail : UserControl
    {
        public Login.user UserData { get; private set; }
        public Form1 MainForm { get; private set; }

        public UserDetail(Form1 MainForm)
        {
            this.MainForm = MainForm;
            InitializeComponent();
        }
        private void set_image(string str)
        {
            this.Invoke(new MethodInvoker(delegate { pictureBox1.ImageLocation = str; }));
            Debug.WriteLine("imageLocation: " + str);
        }
        private void set_userName(string str)
        {
            this.Invoke(new MethodInvoker(delegate { label1.Text = str; }));
            label1.Text = str;
            Debug.WriteLine("userName: " + str);


        }

        // add friend click
        private void button1_Click(object sender, EventArgs e)
        {
            MainForm.loginForm.addFriend(UserData.UserId);
            //MainForm.loginForm.add_usertable_from_addFriend(UserData);
            
        }

        internal void setUserData(Login.user temp)
        {
            this.UserData = temp;
            set_image(temp.profile);
            set_userName(temp.UserName);
        }
    }
}
