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
            this.Width = 257;
            Debug.WriteLine("form width: " + this.Width);
            //--------------------------------------------------------------------------------------------------------------
            // chat history initialization (in tabPageChat)
            // initialize chat list user control
            this.friend_list1 = new chat_list.friend_list(this);
            this.friend_list1.AutoScroll = true;
            this.friend_list1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.friend_list1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.friend_list1.getReceiveMessage = null;
            this.friend_list1.Location = new System.Drawing.Point(0, 0);
            this.friend_list1.Name = "friend_list1";
            this.friend_list1.Size = new System.Drawing.Size(372, 645);
            this.friend_list1.TabIndex = 1; //0

            // initialize panel 4
            this.panel4.Controls.Add(this.friend_list1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(372, 645);
            this.panel4.TabIndex = 0; // 1

           
            //---------------------------------------------------------------------------------------------------------------
            // group list initialization (in tabPageGroup)

            // initialize group list user control
            this.group_list = new chat_list.friend_list(this);
            this.group_list.AutoScroll = true;
            this.group_list.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.group_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.group_list.getReceiveMessage = null;
            this.group_list.Location = new System.Drawing.Point(0, 0);
            this.group_list.Name = "group_list";
            this.group_list.Size = new System.Drawing.Size(372, 645);
            this.group_list.TabIndex = 1; //0

            //initialize panel 7
            this.panel7.Controls.Add(this.group_list);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(372, 645);
            this.panel7.TabIndex = 0; //1
            //---------------------------------------------------------------------------------------------------------------
            // friend list initialization (in tabPageFriend)

            // initialize friend list user control
            this.friend_list2 = new chat_list.friend_list(this);
            this.friend_list2.AutoScroll = true;
            this.friend_list2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.friend_list2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.friend_list2.getReceiveMessage = null;
            this.friend_list2.Location = new System.Drawing.Point(0, 0);
            this.friend_list2.Name = "friend_list2";
            this.friend_list2.Size = new System.Drawing.Size(372, 645);
            this.friend_list2.TabIndex = 1; //0

          
            //--------------------------------------------------------------------------------------------------------------
            // chatbox initialization

            this.chatbox1 = new chat_list.chatbox(loginForm, 0);
            this.chat.Controls.Add(this.chatbox1);

            //this.chatbox1 = new chat_list.chatbox(loginForm);
            this.chatbox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chatbox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatbox1.Location = new System.Drawing.Point(386, 0);
            this.chatbox1.Name = "chatbox1";
            this.chatbox1.Size = new System.Drawing.Size(717, 707);
            this.chatbox1.TabIndex = 0;

            //--------------------------------------------------------------------------------------------------------------

            

            ToolTip tt1 = new ToolTip();
            tt1.SetToolTip(addGroupButton, "Add Group");
            ToolTip tt2 = new ToolTip();
            tt2.SetToolTip(pictureBox2, "Sign Out");
            ToolTip tt3 = new ToolTip();
            tt3.SetToolTip(addFriendButton, "Add Friend");
        }

        internal void searchUser(string str)
        {
            loginForm.searchFriend(str);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Rectangle lasttabrect = tabControl1.GetTabRect(tabControl1.TabPages.Count - 1);
            RectangleF emptyspacerect = new RectangleF(
                    lasttabrect.X + lasttabrect.Width + tabControl1.Left,
                    tabControl1.Top + lasttabrect.Y,
                    tabControl1.Width - (lasttabrect.X + lasttabrect.Width),
                    lasttabrect.Height);

            Brush b = Brushes.BlueViolet; // the color you want
            e.Graphics.FillRectangle(b, emptyspacerect);
        }



        //-------------------------------------------------------------------------------------------------------------------
        // close button
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Width = 257;

            //loginForm.ResetConnection();

            //Application.Exit();
        }

        //------------------------------------------------------------------------------------------------------------------
        // clear button --> don't forget to delete!
        private void button1_Click(object sender, EventArgs e)
        {
            chatbox1.Dispose();
            this.chatbox1 = new chat_list.chatbox(this.loginForm, 0);
            this.chat.Controls.Add(this.chatbox1);
            this.chatbox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chatbox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatbox1.Location = new System.Drawing.Point(386, 0);
            this.chatbox1.Name = "chatbox1";
            this.chatbox1.Size = new System.Drawing.Size(717, 600);
            this.chatbox1.TabIndex = 0;
        }

        //-----------------------------------------------------------------------------------------------------------------
        // display chat history
        public delegate void set_chathistory(int friendID);
        public void ChatHistory(int ID)
        {
            
            // clear and display the chatbox            
            chatbox1.Dispose();
            this.chatbox1 = new chat_list.chatbox(this.loginForm, ID);
            
            this.chat.Controls.Add(this.chatbox1);
            this.chatbox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chatbox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatbox1.Location = new System.Drawing.Point(386, 0);
            this.chatbox1.Name = "chatbox1";
            this.chatbox1.Size = new System.Drawing.Size(717, 600);
            this.chatbox1.TabIndex = 0;

            this.chatbox1.chatbox_visible(false);
            this.Width = 257;

            this.Invoke(new set_chathistory(loginForm.DisplayChatHistory), ID);

            this.chatbox1.chatbox_visible(true);
            this.Width = 728;

        }
        //------------------------------------------------------------------------------------------------------------------
        // pass request to create new chat history
        public delegate void set_createChatH(int friendID);
        public void passRequestNewChat(int friendID)
        {
            this.Invoke(new set_createChatH(loginForm.createChatHistory), friendID);
        }
        //------------------------------------------------------------------------------------------------------------------
        // move windows
        private bool mouseDown;
        private Point lastLocation;
        public bool add_grouplist;

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

        internal void sendUserInfo(Login.user temp)
        {
            SFriend.userdetail_fromLogin(temp);
        }

        private void header1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        //------------------------------------------------------------------------------------------------------------------
        // Log Out Button Function
        private void label1_Click(object sender, EventArgs e)
        {
            loginForm.signOut();
            this.Dispose();
        }
        //-------------------------------------------------------------------------------------------------------------------
        // add friend button --> not finished!
        public delegate void set_addFriend(int friendID);
        private void addFriendButton_Click(object sender, EventArgs e)
        {
            //int friendID = 0;
            //this.Invoke(new set_addFriend(loginForm.addFriend), friendID);
            SFriend = new SearchFriend(this);
            SFriend.Show();
        }

        //-------------------------------------------------------------------------------------------------------------------
        // add group button
        public addGroup NewGroup;

        public delegate void set_addGroup(string member);
        private void addGroupButton_Click(object sender, EventArgs e)
        {
            add_grouplist = true;

            strfriendList = new List<string>();

            // display addGroup form
            NewGroup = new addGroup(this);
            NewGroup.Show();

            // go to tabpageChat
            this.tabControl1.SelectedTab = tabPageChat;
                        
        }
        

        // display member
        public delegate void set_member(string str, int ID, string path);
        public void displayMember(string str, int ID, string path)
        {
            this.Invoke(new set_member(loginForm.ReviewGroupMember), str, ID, path);
        }

        // send data to login form
        public delegate void set_memberId(string memberID);
        public void passMemberID()
        {
            //string memberID = loginForm.userinfo.UserId.ToString();
            string temp = loginForm.userinfo.UserId.ToString();

            foreach (string s in strfriendList)
            {
               temp += "," + s;                
            }

            var temp1 = temp.Split(',').ToList();
            var temp2 = temp1.Distinct().ToList();
            var sortemp2 = temp2.OrderBy(x => x).ToList();
            string memberID = String.Join(",", sortemp2);
            
            this.Invoke(new set_memberId(loginForm.addGroup), memberID);
                        
        }
                
        // display chat history for the new group
        public void view_newGroup(int ID)
        {
            this.tabControl1.SelectedTab = tabPageGroup;
            ChatHistory(ID);
        }
        //-------------------------------------------------------------------------------------------------------------------
        
        //-------------------------------------------------------------------------------------------------------------------

        public void add_list_from_friendlist(string str)
        {

        }
        private List<string> strfriendList;

        public SearchFriend SFriend { get; private set; }

        // variable for passing receive data --> trial!
        //private string receiveMessage;
        
        public bool add_strfriendList(string str)
        {
            bool already_added = true;
            foreach (var temp in strfriendList)
            {
                if (str == temp)
                    already_added = false;
            }
            if(already_added)
                strfriendList.Add(str);

            return already_added;
                        
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void label2_Click(object sender, EventArgs e)
        {

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
        

        // trial button
        private void button2_Click(object sender, EventArgs e)
        {
            //friend_list1.addInMessage("new user", "00;00", 0, 0);
        }

        private void pictureBox5_Click_1(object sender, EventArgs e)
        {
            add_grouplist = false;
            //send data of the friend list
            friend_list1.set_default_color();
            strfriendList = null;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (SFriend != null)
                SFriend.Close();
            if (NewGroup != null)
                NewGroup.Close();
            
            loginForm.send_data("logout");
            loginForm.signOut();
            
            this.Dispose();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SFriend != null)
                SFriend.Close();
            if (NewGroup != null)
                NewGroup.Close();

            //loginForm.send_data("logout");
            //loginForm.signOut();
        }
    }
}
