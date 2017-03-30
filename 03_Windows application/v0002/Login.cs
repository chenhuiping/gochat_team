using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
//using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCP_handle;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;

namespace chat_list
{
    public partial class Login : Form
    {
        public string username;
        string check = "$";
        //for calling chat form - defining chat room
        private Form1 MainForm;
        //defining chatbox
        private chatbox newChat;
        // for calling sign up page
        private sign_up SignUpForm;

        private bool loginCheck;

        public Login()
        {
            InitializeComponent();
            username = usernameTextBox.Text;
        }
        //---------------------------------------------------------------------------------------------------------
        // class and variable to deserialize data
        public class UserTable
        {
            public string type { get; set; }
            public List<user> data { get; set; }
        }

        internal void add_usertable_from_addFriend(user userData)
        {
            this.Invoke(new set_grouplist(MainForm.friend_list1.addInMessage), userData.UserName, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture), userData.UserId, 0, userData.profile);
            DB_UserTable.Add(userData);
            userData = null;
            
        }

        public class addUserTable
        {
            public string type { get; set; }
            public user data { get; set; }
        }
        public class user
        {
            public int UserId { get; set; }
            public string PIN { get; set; }
            public string UserName { get; set; }
            public string profile { get; set; }
            public string ip { get; set; }
        }
        public class FriendTable
        {
            public string type { get; set; }
            //public friend[] data { get; set; }
            public friend data { get; set; }

        }
        public class friend
        {
            public int UserId { get; set; }
            public string FriendId { get; set; }
        }

        public class ChatTable
        {
            public string type { get; set; }
            //public chat[] data { get; set; }
            public List<chat> data { get; set; }
           
        }
        public class chat
        {
            public int ChatId { get; set; }
            public string member { get; set; }
            public int type { get; set; }
        }

        public class rChatTable
        {
            public string type { get; set; }
            public chat data { get; set; }
            //public List<chat> data { get; set; }

        }

        public class MessageTable
        {
            public string type { get; set; }
            //public message[] data { get; set; }
            public List<message> data { get; set; }
            
        }
        public class rMessageTable
        {
            public string type { get; set; }
            //public message[] data { get; set; }
            public message data { get; set; }

        }
        public class message
        {
            public int Id { get; set; }
            public int ChatId { get; set; }
            public string time { get; set; }
            public string content { get; set; }
            public int from { get; set; }
            public int type { get; set; }
        }

        public string receiveMessage;
        //----------------------------------------------------------------------------------------------------------------
        public List<user> DB_UserTable;
        private List<chat> DB_ChatTable;
        //private List<friend> DB_FrientTable;
        private List<message> DB_MessageTable;
        private user newFriend;

 
        // for Websocket connection

        bool ws_socket_flag = true;
        public int port;
        public string ip;
        public bool ws_flag = false;
        ClientWebSocket socket;
        CancellationTokenSource cts;

        private string initMessage;
        private void set_loading()
        {
            this.Invoke(new MethodInvoker(delegate
            {
                button1.Enabled = false;
                loadingImg.Visible = true;
            }));
        }
        private void init_connect()
        {
            if (socket.State == WebSocketState.Open)
            { 
                // send username and password to server for verification
                //string username = usernameTextBox.Text;
                //string password = passwordTextBox.Text;
                send_data(initMessage);
                ws_socket_flag = false;
            }
        }
        private void initTable()
        {
            DB_UserTable = new List<user>();
            DB_ChatTable = new List<chat>();
            DB_MessageTable = new List<message>();
            Rcheck_chat = false;
            Rcheck_user = false;
            Rcheck_message = false;
            R_check_history = false;
            loginCheck = false;
        }
        private void call_mainform()
        {
            if (!R_check_history)
            {
                if (Rcheck_chat && Rcheck_user && Rcheck_message && loginCheck)
                {
                    Rcheck_chat = false;
                    Rcheck_user = false;
                    Rcheck_message = false;
                    R_check_history = true;
                    this.Invoke(new creatform(creatMain));
                    Debug.WriteLine("receive complete and creat Main form-------------");
                    DisplayGroupList(DB_ChatTable);
                    DisplayFriendList(DB_UserTable);
                    //DisplayFriendWithchat(DB_UserTable, DB_ChatTable);
                }
            }
        }
        public async Task StartAsync()
        {
            cts = new CancellationTokenSource();
            CancellationTokenSource cts_timer = new CancellationTokenSource(10000);
            socket = new ClientWebSocket();

            string wsUri = "ws://47.91.75.150:1337";
            //string wsUri = "ws://localhost:1337";
            initTable();
            try
            {
                set_loading();
                await socket.ConnectAsync(new Uri(wsUri), cts_timer.Token);
            }
            catch (Exception e)
            {
                ResetConnection();
                Debug.WriteLine("socket open error");
            }
            Debug.WriteLine(socket.State.ToString()+"--------------------------------");
            init_connect();
            Task.Factory.StartNew(
            async () =>
            {
                var rcvBytes = new byte[1024]; //max size of the data is 1024 bytes
                var rcvBuffer = new ArraySegment<byte>(rcvBytes);
                while (true)
                {
                    try
                    {
                        WebSocketReceiveResult rcvResult =
                    await socket.ReceiveAsync(rcvBuffer, cts.Token);
                        byte[] msgBytes = rcvBuffer.Skip(rcvBuffer.Offset).Take(rcvResult.Count).ToArray();
                        //-----------------------------
                        OnReceive(msgBytes);
                        call_mainform();
                    }
                    catch(Exception e)
                    {
                        cts.Cancel();
                        Debug.WriteLine("connection dead!");
                        ResetConnection();
                    }
                }
            }, cts.Token, TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }

        //-------------------------------------------------------------------------------------------------
        // receive data from server
        // message type is byte

        public string buffer = "";
        public void OnReceive(byte[] msgBytes)
        {
            string rcvMsg = Encoding.UTF8.GetString(msgBytes);
            Debug.WriteLine(rcvMsg);
            if(buffer == "")
            {
                buffer = rcvMsg;
            }
            else
            {
                buffer += rcvMsg;
            }
            int r_count = 0;
            int l_count = 0;
            foreach (byte temp in buffer)
            {
                if (temp == '{')
                    l_count++;
                if (temp == '}')
                    r_count++;
            }

            if (l_count - r_count == 0)
            {
                Debug.WriteLine(buffer);
                deserializeData(buffer);
                buffer = "";
            }
            
        }
        //---------------------------------------------------------------------------------------------------------------
        // deserialize data
        public delegate void sendUserinfo(user temp);

        public delegate void set_newGroupID(int ID);
        public void deserializeData(string rcvMsg)
        {
            if (rcvMsg.Contains("type\":\"user"))
            {
                UserTable temp = JsonConvert.DeserializeObject<UserTable>(rcvMsg);
                int count = 0;
                foreach (user tempH in temp.data)
                {
                    if (count == 0)
                    {
                        count++;
                        userinfo = tempH;
                    }
                    else
                    {
                        DB_UserTable.Add(tempH);

                    }
                }
                Debug.WriteLine("user");
                Debug.WriteLine(userinfo.UserName);
                Rcheck_user = true;
            }
            else if (rcvMsg.Contains("type\":\"searchFriend"))
            {
                if (rcvMsg.ToString().Contains("This user is already your friend"))
                {
                    //MainForm.SFriend.Close();
                    Debug.WriteLine("This user is already your friend");
                    MessageBox.Show("This user is already your friend");
                } else if (rcvMsg.ToString().Contains("wrong : no user"))
                {
                    Debug.WriteLine("wrong : no user");
                    MessageBox.Show("wrong : no user");
                }
                else
                {
                    addUserTable temp = JsonConvert.DeserializeObject<addUserTable>(rcvMsg);
                    this.Invoke(new sendUserinfo(MainForm.sendUserInfo), temp.data);
                    newFriend = temp.data;
                }

                //DB_UserTable.Add(temp);
            }
            else if (rcvMsg.Contains("type\":\"chat"))
            {
                if (rcvMsg.Contains("data\":\"No chat list in the database"))
                {
                    Debug.WriteLine("No chat list in the database");
                    Rcheck_chat = true;
                }
                else
                {
                    ChatTable temp = JsonConvert.DeserializeObject<ChatTable>(rcvMsg);
                    foreach (chat tempH in temp.data)
                    {
                        DB_ChatTable.Add(tempH);
                    }
                    Debug.WriteLine("chat deserialize complete");
                    Rcheck_chat = true;
                }

            }/*
            else if (rcvMsg.Contains("type\":\"friend"))
            {
                FriendTable temp = JsonConvert.DeserializeObject<FriendTable>(rcvMsg);
                foreach (friend tempH in temp.data)
                {
                    DB_FrientTable.Add(tempH);
                }                
                Debug.WriteLine("friend deserialize complete");
                Rcheck_friend = true;
            }*/
            else if (rcvMsg.Contains("type\":\"message"))// || rcvMsg.Contains("type\":\"umessage"))
            {
                if (rcvMsg.Contains("data\":\"No message list in the database"))
                {
                    Debug.WriteLine("No message list in the database");
                    Rcheck_message = true;
                }
                else
                {
                    try
                    {
                        if (rcvMsg.Contains("[") && rcvMsg.IndexOf("[") == 25)
                        {
                            MessageTable temp = JsonConvert.DeserializeObject<MessageTable>(rcvMsg);
                            foreach (message tempH in temp.data)
                            {
                                DB_MessageTable.Add(tempH);
                            }
                            Debug.WriteLine("message history deserialize complete");
                            Rcheck_message = true;
                        }
                        else
                        {

                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("loading message fail" + e);
                    }
                }

            }
            else if (rcvMsg.Contains("\"type\":\"umessage"))
            {
                rMessageTable temp = JsonConvert.DeserializeObject<rMessageTable>(rcvMsg);
                DB_MessageTable.Add(temp.data);
                string sender = "";

                // get sender
                var sendtemp = from user in DB_UserTable where user.UserId == temp.data.@from select user;

                foreach (user u in sendtemp)
                {
                    sender = u.UserName;
                }

                if (temp.data.ChatId == MainForm.chatbox1.chatID)
                {
                    if (temp.data.from == userinfo.UserId)
                        this.Invoke(new set_chatbox(MainForm.chatbox1.addOutMessage), temp.data.content, sender, temp.data.time);
                    else if (temp.data.from != userinfo.UserId)
                        this.Invoke(new set_chatbox(MainForm.chatbox1.addInMessage), temp.data.content, sender, temp.data.time);
                }
                else
                {
                    foreach(var item in MainForm.friend_list1.store_list)
                    {
                        if(temp.data.from == item.userID)
                        {//------------------------change color when the message is coming
                            item.BackColor = Color.AliceBlue;
                        }
                    }
                    //MainForm.friend_list1.store_list
                }
                Debug.WriteLine("message realtime deserialize complete");
            } else if (rcvMsg.Contains("\"type\":\"uchat"))
            {
                if (rcvMsg.Contains("\"data\":\"Failed to create a new chat"))
                {
                    Debug.WriteLine("Failed to create a new chat");
                }
                else
                {
                    rChatTable temp = JsonConvert.DeserializeObject<rChatTable>(rcvMsg);
                    DB_ChatTable.Add(temp.data);

                    if (temp.data.type == 1)
                    {
                        this.Invoke(new set_grouplist(MainForm.group_list.addInMessage), temp.data.member, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture), temp.data.ChatId, 2, "");
                        this.Invoke(new set_newGroupID(MainForm.view_newGroup), temp.data.ChatId);
                    }
                    else
                    {
                        this.Invoke(new set_existChat(MainForm.ChatHistory), temp.data.ChatId);
                    }
                }
                
                
            }else if (rcvMsg.Contains("\"type\":\"ufriend"))
            {
                FriendTable temp = JsonConvert.DeserializeObject<FriendTable>(rcvMsg);

                if (temp.data.UserId == userinfo.UserId && temp.data.FriendId == newFriend.UserId.ToString())
                {
                    // add temp.friendId to friend list
                    add_usertable_from_addFriend(newFriend);
                }
            }else if (rcvMsg.Contains("\"type\":\"register"))
            {
                if (rcvMsg.ToString().Contains("please change another username"))
                {
                    // if username already exist
                    Debug.WriteLine("please change another username");
                    MessageBox.Show("please change another username");
                    this.Invoke(new createSignUpForm(createSignUp));
                }
                else if (rcvMsg.ToString().Contains("succeed"))
                {
                    Debug.WriteLine("You are now register!");
                    MessageBox.Show("You are now register!");
                    initMessage = "";
                    ResetConnection();
                    // close sign up form and open login form
                }
                
            }
            else if (rcvMsg.ToString().Contains("\"type\":\"logincheck"))
            {
                if (rcvMsg.ToString().Contains("\"data\":\"true"))
                {
                    loginCheck = true;
                    Debug.WriteLine("Login success!");
                }
                else if (rcvMsg.ToString().Contains("wrong password"))
                {
                    loginCheck = false;
                    MessageBox.Show("Wrong Password!");
                }
                else if (rcvMsg.ToString().Contains("wrong username"))
                {
                    loginCheck = false;
                    MessageBox.Show("Wrong Username!");
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------
        // convert text message into bytes before sending it to server
        public void send_data(string str)
        {
            if (str == "logout")
                str += "$" + userinfo.UserName;
            Debug.WriteLine("senddata" + str);
            byte[] sendBytes = Encoding.UTF8.GetBytes(str);
            SendAsync(sendBytes);     
        }
        
        //-------------------------------------------------------------------------------------------------------------------
        // send data in bytes to server
        // if the connection is opened, it will call by anywhere in class
        public async Task SendAsync(byte[] message)
        {
            if (socket != null && socket.State == WebSocketState.Open)
            {
                var sendBuffer = new ArraySegment<byte>(message);
                await socket.SendAsync(sendBuffer, WebSocketMessageType.Text, endOfMessage: true, cancellationToken: cts.Token);
            }
            else
            {
                MessageBox.Show("No connection to server");
                ResetConnection();
            }
            /*if (ws_socket_flag)
            {
                cts.Cancel();
            }*/
        }
        //-------------------------------------------------------------------------------------------------------------------
        // save message to NewMessageTable
        private void addToMessageTable(string message)
        {
            int messageNumber = DB_MessageTable.Count();

            MessageTable temp = JsonConvert.DeserializeObject<MessageTable>(message);

            foreach (message tempH in temp.data)
            {
                DB_MessageTable.Add(tempH);
            }


        }
        //-------------------------------------------------------------------------------------------------------------------
        // create new connection
        public void newConnectWs()
        {
            if (socket == null)
            {

            }
            else
            {
                ResetConnection();
            }
            connectWS();
        }
        //--------------------------------------------------------------------------------------------------------------------
        // check whether the connection is open or not
        public void connectWS()
        {
            if (socket == null)
            {
                //run backgroundworker
                //-----------------------------------------------------------------
                this.StartWSClient.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Already connected!!");
            }
        }
        //-------------------------------------------------------------------------------------------------------------------
       //use sign out
        public void signOut()
        {
            this.loginCheck = false;
            MainForm.Close();
            ResetConnection();
            this.Show();

        }
        //--------------------------------------------------------------------------------------------------------------------
        // to reset connection
        public void ResetConnection()
        {
            try
            {
                if (this.Visible == false && MainForm != null)
                {
                    MainForm.Close();
                    this.Show();
                }
                if (socket.State == WebSocketState.Open)
                {

                }
                initTable();
                StartWSClient.Dispose();
                cts.Cancel();
                cts = null;
                socket = null;
                this.Invoke(new MethodInvoker(delegate
                {
                    button1.Enabled = true;
                    loadingImg.Visible = false;
                }));
            }
            catch(Exception e)
            {
                Debug.WriteLine("ResetConnection error: " + e);
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        // to call MainForm after user login successfully
        public delegate void creatform();
        public void creatMain()
        {
            MainForm = new Form1(this);
            MainForm.Show();
            //MainForm.usernameLabel.Text = this.username;
            this.Hide();

            newChat = new chatbox(this, 0);
        }
        //--------------------------------------------------------------------------------------------------------------------
        // login button
        private void button1_Click(object sender, EventArgs e)
        {
            if (usernameTextBox.Text != "" && passwordTextBox.Text != "")
            {
                initMessage = usernameTextBox.Text + "," + passwordTextBox.Text;
                newConnectWs();
            }
            else
            {
                MessageBox.Show("Enter your username and password!");
            }
        }
        //-------------------------------------------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------------------------------------
        // function to display friend list

        public delegate void set_friendlist(string message, string date, int groupID, int itemType, string path);
        public void DisplayFriendList(List<user> uTable)
        {
            foreach (user temp in uTable)
            {
                //this.Invoke(new set_grouplist(MainForm.friend_list1.addInMessage) ,temp.UserName, DateTime.Now.ToShortTimeString(), temp.UserId, 0, temp.profile);
                this.Invoke(new set_friendlist(MainForm.friend_list1.addInMessage), temp.UserName, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture), temp.UserId, 1, temp.profile);
            }
        }

        //---------------------------------------------------------------------------------------------------------------
        // function to display friend with chat history

        public delegate void set_friendwithchat(string message, string date, int chatID, int itemType, string path); // to pass data to other form

        private void DisplayFriendWithchat(List<user> uTable, List<chat> cTable)
        {
            string userID = userinfo.UserId.ToString();
            string friendID = "";

            // filter friend who has chat history
            var friendWithChat = from chat in cTable where chat.type == 0 select chat;

            char[] delimiter = { ',' };

            foreach (chat chatH in friendWithChat)
            {
                int chatId = chatH.ChatId;
                string[] splitText = chatH.member.Split(delimiter);

                if (splitText[0] != userID)
                {
                    friendID = splitText[0];
                }
                else
                {
                    friendID = splitText[1];
                }

                var friends = from friend in uTable where friend.UserId.ToString() == friendID select friend;

                foreach (user friendH in friends)
                {
                    string friendName = friendH.UserName;
                    string picpath = friendH.profile;
                    this.Invoke(new set_friendwithchat(MainForm.friend_list1.addInMessage), friendName, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture), chatId, 0, friendH.profile);
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        // function to display grouplist

        public delegate void set_grouplist(string message, string date, int groupID, int itemType, string path);

        public void DisplayGroupList(List<chat> cTable)
        {
            foreach(chat temp in cTable)
            {
                this.Invoke(new set_grouplist(MainForm.group_list.addInMessage), temp.member, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture), temp.ChatId, 2, "");
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        // function to display chat history

        public delegate void set_chatbox(string message, string username, string time);

        public void DisplayChatHistory(int ID)
        {
            int userID = userinfo.UserId;
            
            var messages = from message in DB_MessageTable where message.ChatId == ID select message;

            foreach (message messageH in messages)
            {
                //string messageID = messageH.;
                string time = messageH.time;
                string content = messageH.content;
                int sender = messageH.from;
                int type = messageH.type;
                string senderName = "";

                // get sender
                var sendtemp = from user in DB_UserTable where user.UserId == sender select user;

                foreach (user u in sendtemp)
                {
                    senderName = u.UserName;
                }

                if (sender == userID)
                {
                    if(type == 0)
                    {
                        // display text message
                        this.Invoke(new set_chatbox(MainForm.chatbox1.addOutMessage), content, senderName, time);
                    }
                    else
                    {
                        // display file/image
                        this.Invoke(new set_chatbox(MainForm.chatbox1.addOutPicture), content, senderName, time);
                    }
                    
                }
                else
                {
                    if (type == 0)
                    {
                        // display text message
                        this.Invoke(new set_chatbox(MainForm.chatbox1.addInMessage), content, senderName, time);
                    }
                    else
                    {
                        // display file/image
                        this.Invoke(new set_chatbox(MainForm.chatbox1.addInPicture), content, senderName, time);
                    }
                }
            }

        }
        //--------------------------------------------------------------------------------------------------------------
        // function to display text message
        private void DisplayText(string receiveText)
        {
            this.newChat.getReceiveMessage = receiveText;
            this.Invoke(new set_chatbox(MainForm.chatbox1.addInMessage), receiveText);
        }
        //---------------------------------------------------------------------------------------------------------------
        // function to display file/image
        private void DisplayImage(string receiveText)
        {

        }
        //---------------------------------------------------------------------------------------------------------------
        //variable for sent message --> don't forget to delete!
        private string sentMessage; //--> TCP

        // function to get message from chatBox
        public string getSendMessage
        {
            get { return sentMessage; }
            set { sentMessage = value; }
        }

        public bool Rcheck_user { get; private set; }
        public bool Rcheck_chat { get; private set; }
        public bool R_check_history { get; private set; }


        //---------------------------------------------------------------------------------------------------------------
        // function to send file/image
        /*public void sendFile(string longFileName, string shortFileName)
        {
            int bufferSize = 1024;
            byte[] buffer = null;
            byte[] header = null;


            FileStream fs = new FileStream(longFileName, FileMode.Open);
            bool read = true;

            int bufferCount = Convert.ToInt32(Math.Ceiling((double)fs.Length / (double)bufferSize));
            string headerStr = "Content-length:" + fs.Length.ToString() + "\r\nFilename:" + @"./" + shortFileName + "\r\n";
            header = new byte[bufferSize];
            Array.Copy(Encoding.Unicode.GetBytes(headerStr), header, Encoding.Unicode.GetBytes(headerStr).Length);
            if (clientSocket.Connected)
            {
                clientSocket.SendTimeout = 600000;
                clientSocket.ReceiveTimeout = 600000;
                clientSocket.Client.Send(header);
                for (int i = 0; i < bufferCount; i++)
                {
                    buffer = new byte[bufferSize];
                    int size = fs.Read(buffer, 0, bufferSize);

                    clientSocket.Client.Send(buffer, size, SocketFlags.Partial);

                }

                //clientSocket.Client.Close();

                fs.Close();
            }
            else
            {
                MessageBox.Show("Something wrong!");
            }

        }
        */
        //----------------------------------------------------------------------------------------------------------------
        // function to create new chat history
        public delegate void set_createChatH(int friendID);
        public delegate void set_existChat(int chatID);
        public void createChatHistory(int friendID)
        {
            int userID = userinfo.UserId;

            var temp1 = from chatH in DB_ChatTable where chatH.member == (userID.ToString() + "," + friendID.ToString()) select chatH;
            var temp2 = from chatH in DB_ChatTable where chatH.member == (friendID.ToString() + "," + userID.ToString()) select chatH;

            int temp1n = temp1.Count();
            int temp2n = temp2.Count();

            if (temp1n == 0 && temp2n == 0)
            {
                // if there is no chat with this friend, make a new chat

                string message = "chat" + check + userID.ToString() + "," + friendID.ToString() + check + "0";
                //MessageBox.Show(message);

                send_data(message);

                // don't forget to save the new chat to NewChatTable
                /*
                

                if (NewChatTable.data.Count() > chatN)
                {
                    int indexC = NewChatTable.data.Count() - 1;
                    this.Invoke(new set_existChat(MainForm.viewExistChat), NewChatTable.data[indexC].ChatId);
                }
                 */
            }
            else
            {
                // if there is a chat with this friend, display chat history

                //MessageBox.Show("chat already exist");

                if (temp1n != 0)
                {
                    foreach (chat h in temp1)
                    {
                        this.Invoke(new set_existChat(MainForm.ChatHistory), h.ChatId);
                    }
                }
                else if (temp2n != 0)
                {
                    foreach (chat h in temp2)
                    {
                        this.Invoke(new set_existChat(MainForm.ChatHistory), h.ChatId);
                    }
                }

            }
            
            
        }

        //----------------------------------------------------------------------------------------------------------------
        //close button function
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ResetConnection();
            Application.Exit();
        }

        //----------------------------------------------------------------------------------------------------------------
        // make the windows move
        private bool mouseDown;
        private Point lastLocation;
        private bool check_init_message;
        private bool Rcheck_message;
        public user userinfo;

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
        //--------------------------------------------------------------------------------------------------------------
        // background worker to start websocket connection
        private void StartWSClient_DoWork(object sender, DoWorkEventArgs e)
        {
            StartAsync();
            check_init_message = true;
            //Loading.RunWorkerAsync();
        }
        //--------------------------------------------------------------------------------------------------------------
        // for textbox

        private void usernameTextBox_Click(object sender, EventArgs e)
        {
            usernameTextBox.Text = "";
        }

        private void passwordTextBox_Click(object sender, EventArgs e)
        {
            passwordTextBox.Text = "";
            passwordTextBox.UseSystemPasswordChar = true;
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            passwordTextBox.UseSystemPasswordChar = true;
        }
        //----------------------------------------------------------------------------------------------------------------
        // call sign up form
        private void label3_Click(object sender, EventArgs e)
        {
            this.Invoke(new createSignUpForm(createSignUp));

        }

        public delegate void createSignUpForm();
        public void createSignUp()
        {
            SignUpForm = new sign_up(this);
            SignUpForm.Show();
            this.Hide();

           
        }

        public delegate void createAccount(string username, string password, string profile);
        public void createNewAccount(string username, string password, string profile)
        {
            string message = "register" + check + username + check + password + check + profile;
            //MessageBox.Show(message);
            /*if (socket.State == WebSocketState.Open)
            {
                send_data(message);
            }*/

            initMessage = message;
        }
        //---------------------------------------------------------------------------------------------------------------
        public void searchFriend(string str)
        {
            string message = "search" + check + userinfo.UserId + check + str;
            send_data(message);

        }
        //----------------------------------------------------------------------------------------------------------------
        // add new friend
        public delegate void addNewFriend(int ID);
        public void addFriend(int friendID)
        {
            int userID = userinfo.UserId;
            
            string message = "friend" + check + userID.ToString() + check + friendID.ToString();
            send_data(message);
            message = "friend" + check + friendID.ToString() + check + userID.ToString();
            send_data(message);
            
        }
        //----------------------------------------------------------------------------- -----------------------------------
        // add new group
        //send the request to server
        public void addGroup(string member)
        {
            //int userID = userinfo.UserId;
            string message = "chat" + check + member + check + "1";
            
            send_data(message);

            // don't forget to save into newChatTable
        }

        //review the group member before send it
        public delegate void set_groupmember(string message, string date, int friendID, int itemType, string path);
        public void ReviewGroupMember(string username, int userID, string profile)
        {

            this.Invoke(new set_groupmember(MainForm.NewGroup.member_list.addInMessage), username, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture), userID, 0, profile);
        }
        //--------------------------------------------------------------------------------------------------------------------
        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            enterKey(e);
        }
        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            enterKey(e);
        }

        private void usernameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            enterKey(e);
        }

        private void LoadingTimer_Tick(object sender, EventArgs e)
        {
            



        }

        private void Loading_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime basetime = DateTime.Now;
            
        }

        private void enterKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(socket!=null)
                {
                    MessageBox.Show("socket alreay work");
                }
                else
                    button1.PerformClick();
            }
        }

        
    }
}
