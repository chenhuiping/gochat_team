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
    public partial class friend_item : UserControl
    {
        private Form1 MainForm;

        public int userID;
        private int itemType; // 0 = friend with chat, 1 = friend without chat, 2 = group chat
        public string UserName;
        public string pathprofile;

        public friend_item(Form1 MainForm)
        {
            
            InitializeComponent();
        }
        public friend_item(Form1 MainForm, string message, string time, msgtype1 messagetype, int userID, int itemType, string path)
        {
            InitializeComponent();
            //Debug.WriteLine(message + userID + path);
            this.MainForm = MainForm;
            this.userID = userID;
            this.UserName = message;
            this.itemType = itemType;
            this.pathprofile = path;
            if (path != "")
                this.pictureBox1.ImageLocation = "http://47.91.75.150/" + path;
            userName.Text = message;
            userInformation.Text = time;
            if (messagetype.ToString() == "In")
            { //incoming message
                this.BackColor = Color.FromArgb(0, 164, 147);

            }

            //lets add the function which adjust the bubble height
            Setheight();
        }
        void Setheight()
        {
            Size maxSize = new Size(495, int.MaxValue);
            Graphics g = CreateGraphics();
            SizeF size = g.MeasureString(userName.Text, userName.Font, userName.Width);
            userName.Height = int.Parse(Math.Round(size.Height + 2, 0).ToString());
            userInformation.Top = userName.Bottom;
            this.Height = userInformation.Bottom + 10;

        }
        private void friend_item_Load(object sender, EventArgs e)
        {
            Setheight();
        }

        
        //private chatbox chatbox1;

        // property to display chat history
        //public Delegate chatHistoryPointer;

        public delegate void set_chatbox(int message);
        public delegate void set_createChatH(int ID);

        //-----------------------------------------------------------------------------------------------------------------
        // what happens when we click friend_item
        public void friend_item_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(0, 164, 147);

            if (MainForm.add_grouplist)
            {
                // change the colour to red it - after we click add group
                this.BackColor = Color.Red;
                if (this.itemType == 2)
                {
                    // for exist group
                    MainForm.add_strfriendList(this.UserName);
                }
                else
                {
                    // for new member
                    MainForm.add_strfriendList(this.userID.ToString());
                }

                
                MainForm.displayMember(this.UserName, this.userID, this.pathprofile);
            }
            else if (itemType == 0 || itemType == 2)
            {
                // print chat history
                this.Invoke(new set_chatbox(MainForm.ChatHistory), userID);
            }
            else
            {
                // create new chat history
                //MessageBox.Show("Create new chat history");
                this.Invoke(new set_createChatH(MainForm.passRequestNewChat), userID);
            }

            
                    
        }

        private void friend_item_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
    public enum msgtype1
    {
        In,
        Out
    }
}
