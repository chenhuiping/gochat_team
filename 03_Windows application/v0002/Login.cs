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
            public user data { get; set; }
        }

        public class user
        {
            public int UserId { get; set; }
            public string PIN { get; set; }
            public string UserName { get; set; }
            public string profile { get; set; }
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

        public class FriendTable
        {
            public string type { get; set; }
            //public friend[] data { get; set; }
            public List<friend> data { get; set; }
            
        }

        public class friend
        {
            public int UserId { get; set; }
            public string PIN { get; set; }
            public string UserName { get; set; }
            public string profile { get; set; }
            public string Ip { get; set; }
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
            public int ChatId { get; set; }
            public string time { get; set; }
            public string content { get; set; }
            public int from { get; set; }
            public int type { get; set; }
        }

        public string receiveMessage;


        //----------------------------------------------------------------------------------------------------------------
        public UserTable DB_UserTable;
        private List<chat> DB_ChatTable;
        private List<friend> DB_FrientTable;
        private List<message> DB_MessageTable;

        private void initTable()
        {
            DB_UserTable = new UserTable();
            DB_ChatTable = new List<chat>();
            DB_FrientTable = new List<friend>();
            DB_MessageTable = new List<message>();
            Rcheck_chat = false;
            Rcheck_friend = false;
            Rcheck_user = false;
            Rcheck_message = false;
            R_check_history = false;
        }

        // for Websocket connection

        bool ws_socket_flag = true;
        public int port;
        public string ip;
        public bool ws_flag = false;
        ClientWebSocket socket;
        CancellationTokenSource cts;

        public async Task StartAsync()
        {
            //textBox2.Text = "Name please: ";
            string name = Console.ReadLine();
            //-----------------------------------------------------------------------------------------------------------
            //this.Invoke(new set_textbox(text2Set), "connecting");
            //textBox2.Text = "Connecting....";
            cts = new CancellationTokenSource();
            socket = new ClientWebSocket();

            //get ip and port from the ui
            //string wsUri = string.Format(textBox4.Text.Trim());
            //----------------------------------------------------------------------------------------------------------
            string wsUri = "ws://47.91.75.150:1337";
            initTable();
            await socket.ConnectAsync(new Uri(wsUri), cts.Token);
            //MessageBox.Show(socket.State.ToString());
            Debug.WriteLine(socket.State.ToString()+"--------------------------------");
            if (socket.State == WebSocketState.Open)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    button1.Enabled = false;
                    loadingImg.Visible = true;
                }));

                // send username and password to server for verification
                string username = usernameTextBox.Text;
                string password = passwordTextBox.Text;
                send_data(username + "," + password);
                ws_socket_flag = false;


            }
            //this.Invoke(new set_textbox(text2Set), socket.State.ToString());
            //
            Task.Factory.StartNew(
            async () =>
            {
                var rcvBytes = new byte[1024]; //max size of the data is 1024 bytes
                var rcvBuffer = new ArraySegment<byte>(rcvBytes);
                while (true)
                {
                    WebSocketReceiveResult rcvResult =
                    await socket.ReceiveAsync(rcvBuffer, cts.Token);
                    byte[] msgBytes = rcvBuffer.Skip(rcvBuffer.Offset).Take(rcvResult.Count).ToArray();
                    //-----------------------------
                    OnReceive(msgBytes);
                    if (!R_check_history)
                    {
                        if (Rcheck_chat && Rcheck_friend && Rcheck_chat && Rcheck_message)
                        {
                            Rcheck_chat = false;
                            Rcheck_friend = false;
                            Rcheck_user = false;
                            Rcheck_message = false;
                            R_check_history = true;
                            this.Invoke(new creatform(creatMain));
                            Debug.WriteLine("receive complete and creat Main form-------------");
                            DisplayGroupList(DB_ChatTable);
                            DisplayFriendList(DB_FrientTable);
                            DisplayFriendWithchat(DB_FrientTable, DB_ChatTable);
                        }
                    }
                    
                }
            }, cts.Token, TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
            while (true)
            {
                if (ws_socket_flag)
                {
                    cts.Cancel();
                    return;
                }
            }
        }

        //-------------------------------------------------------------------------------------------------
        // receive data from server
        // message type is byte

        public string buffer = "";
        public void OnReceive(byte[] msgBytes)
        {
            string rcvMsg = Encoding.UTF8.GetString(msgBytes);

            if(buffer == "")
            {
                buffer = rcvMsg;
            }
            else
            {
                buffer += rcvMsg;
            }

            Debug.WriteLine(buffer);
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
                deserializeData(buffer);
                buffer = "";
            }
            
        }
        //---------------------------------------------------------------------------------------------------------------
        // deserialize data

        public void deserializeData(string rcvMsg)
        {
            if (rcvMsg.Contains("type\":\"user"))
            {
                Debug.WriteLine("user");

                DB_UserTable = JsonConvert.DeserializeObject<UserTable>(rcvMsg);
                
                Debug.WriteLine(DB_UserTable.type);
                Debug.WriteLine(DB_UserTable.data.UserName);
                Rcheck_user = true;
            }
            else if (rcvMsg.Contains("type\":\"chat"))
            {
                Debug.WriteLine("chat");
                ChatTable temp = JsonConvert.DeserializeObject<ChatTable>(rcvMsg);
                foreach (chat tempH in temp.data)
                {
                    DB_ChatTable.Add(tempH);
                }

                //DisplayGroupList(DB_ChatTable);
                //DisplayFriendWithchat(DB_FrientTable, DB_ChatTable);
                Debug.WriteLine("Chat number" + DB_ChatTable.Count());
                Rcheck_chat = true;
            }
            else if (rcvMsg.Contains("type\":\"friend"))
            {
                Debug.WriteLine("friend");

                FriendTable temp = JsonConvert.DeserializeObject<FriendTable>(rcvMsg);

                /*foreach (friend tempH in temp.data)
                {
                    NewFriendTable.data.Add(tempH);
                }*/
                foreach (friend tempH in temp.data)
                {
                    DB_FrientTable.Add(tempH);
                }

                //DisplayFriendList(DB_FrientTable);
                //DisplayFriendWithchat(DB_FrientTable, DB_ChatTable);

                Debug.WriteLine("friend number : " + DB_FrientTable.Count());
                Rcheck_friend = true;
            }
            else if (rcvMsg.Contains("type\":\"message"))// || rcvMsg.Contains("type\":\"umessage"))
            {
                try
                {
                    if(rcvMsg.Contains("[") && rcvMsg.IndexOf("[") == 25)
                    {
                        //Debug.WriteLine(rcvMsg.IndexOf("["));
                        MessageTable temp = JsonConvert.DeserializeObject<MessageTable>(rcvMsg);
                        foreach (message tempH in temp.data)
                        {
                            DB_MessageTable.Add(tempH);
                        }
                        Rcheck_message = true;
                    }
                    else
                    {
                        rMessageTable temp = JsonConvert.DeserializeObject<rMessageTable>(rcvMsg);
                        DB_MessageTable.Add(temp.data);
                        if (temp.data.ChatId == MainForm.chatbox1.chatID)
                        {
                            if (temp.data.ChatId == DB_UserTable.data.UserId)
                                this.Invoke(new set_chatbox(MainForm.chatbox1.addInMessage), temp.data.content);
                            else if (temp.data.ChatId != DB_UserTable.data.UserId)
                                this.Invoke(new set_chatbox(MainForm.chatbox1.addOutMessage), temp.data.content);
                        }
                    }
                    
                }
                catch(Exception e)
                {
                    Debug.WriteLine("loading message fail"+e);
                }

                Debug.WriteLine("message");
                Debug.WriteLine("message number : " + DB_MessageTable.Count());
            }
        }
        //--------------------------------------------------------------------------------------------------------------


        //------------------------------------------------------------------------------------------------------------------
        // convert text message into bytes before sending it to server
        public void send_data(string str)
        {
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
                ws_socket_flag = true;
            }
            if (ws_socket_flag)
            {
                cts.Cancel();
            }
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
            MainForm.Close();
            ResetConnection();
            this.Show();

        }
        public void ResetConnection()
        {
            if(this.Visible == false && MainForm != null)
            {
                MainForm.Close();
                this.Show();
                this.Invoke(new MethodInvoker(delegate
                {
                    button1.Enabled = true;
                    loadingImg.Visible = false;
                }));


            }
            if (socket.State == WebSocketState.Open)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    button1.Enabled = true;
                    loadingImg.Visible = false;
                }));
                initTable();
                ws_socket_flag = true;
                StartWSClient.Dispose();
                socket = null;
                cts.Cancel();
                cts = null;
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        // to call MainForm after user login successfully
        public delegate void creatform();
        public void creatMain()
        {
            MainForm = new Form1(this);
            MainForm.Show();
            MainForm.usernameLabel.Text = this.username;
            this.Hide();

            newChat = new chatbox(this, 0);
        }
        //--------------------------------------------------------------------------------------------------------------------
        // login button
        private void button1_Click(object sender, EventArgs e)
        {
            if (usernameTextBox.Text != "" && passwordTextBox.Text != "")
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
            else
            {
                MessageBox.Show("Enter your username and password!");
            }
        }
        //-------------------------------------------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------------------------------------
        // function to display friend list

        public delegate void set_friendlist(string message, string date, int friendID, int itemType, string path);
        public void DisplayFriendList(List<friend> fTable)
        {
            foreach (friend temp in fTable)
            {
                this.Invoke(new set_grouplist(MainForm.group_list.addInMessage), temp.UserName, DateTime.Now.ToShortTimeString(), temp.UserId, 0, temp.profile);
            }
        }

        //---------------------------------------------------------------------------------------------------------------
        // function to display friend with chat history

        public delegate void set_friendwithchat(string message, string date, int chatID, int itemType, string path); // to pass data to other form

        private void DisplayFriendWithchat(List<friend> fTable, List<chat> cTable)
        {
            string userID = DB_UserTable.data.UserId.ToString();
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

                var friends = from friend in fTable where friend.UserId.ToString() == friendID select friend;

                foreach (friend friendH in friends)
                {
                    string friendName = friendH.UserName;
                    string picpath = friendH.profile;
                    this.Invoke(new set_friendwithchat(MainForm.friend_list1.addInMessage), friendName, DateTime.Now.ToShortTimeString(), chatId, 0, friendH.profile);
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
                this.Invoke(new set_grouplist(MainForm.group_list.addInMessage), temp.member, DateTime.Now.ToShortTimeString(), temp.ChatId, 0, "");
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        // function to display chat history

        public delegate void set_chatbox(string message);

        public void DisplayChatHistory(int ID)
        {
            int userID = DB_UserTable.data.UserId;
            
            var messages = from message in DB_MessageTable where message.ChatId == ID select message;

            foreach (message messageH in messages)
            {
                //string messageID = messageH.;
                string time = messageH.time;
                string content = messageH.content;
                int sender = messageH.from;
                int type = messageH.type;


                if (sender == userID)
                {
                    if(type == 0)
                    {
                        // display text message
                        this.Invoke(new set_chatbox(MainForm.chatbox1.addOutMessage), content);
                    }
                    else
                    {
                        // display file/image
                        this.Invoke(new set_chatbox(MainForm.chatbox1.addOutPicture), content);
                    }
                    
                }
                else
                {
                    if (type == 0)
                    {
                        // display text message
                        this.Invoke(new set_chatbox(MainForm.chatbox1.addInMessage), content);
                    }
                    else
                    {
                        // display file/image
                        this.Invoke(new set_chatbox(MainForm.chatbox1.addInPicture), content);
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
        public bool Rcheck_friend { get; private set; }
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
        public void createChatHistory(int friendID)
        {
            int userID = DB_UserTable.data.UserId;

            var temp1 = from chatH in DB_ChatTable where chatH.member == (userID.ToString() + "," + friendID.ToString()) select chatH;
            var temp2 = from chatH in DB_ChatTable where chatH.member == (friendID.ToString() + "," + userID.ToString()) select chatH;

            int temp1n = temp1.Count();
            int temp2n = temp2.Count();

            if (temp1n == 0 && temp2n == 0)
            {
                // if there is no chat with this friend, make a new chat

                string message = "chat:" + userID.ToString() + "," + friendID.ToString() + ":0";
                MessageBox.Show(message);

                send_data(message);

                // don't forget to save the new chat to NewChatTable
            }
            else
            {
                // if there is a chat with this friend, display chat history
                MessageBox.Show("chat already exist");
                
            }
            
            
        }
        //----------------------------------------------------------------------------------------------------------------
        // function to close websocket connection
        public void close_all()
        {
            ws_socket_flag = true;
            if (cts != null)
            {
                cts.Cancel();
            }

        }

        //----------------------------------------------------------------------------------------------------------------
        //close button function
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            close_all();
            //LoadingTimer.Enabled = false;
            Environment.Exit(0);
        }

        //----------------------------------------------------------------------------------------------------------------
        // make the windows move
        private bool mouseDown;
        private Point lastLocation;
        private int CountTimer;
        private bool check_init_message;
        private bool Rcheck_message;

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
            string message = "user" + check + username + check + password + check + profile;
            MessageBox.Show(message);
            send_data(message);
            
        }
        //----------------------------------------------------------------------------------------------------------------
        // add new friend
        public delegate void addNewFriend(int ID);
        public void addFriend(int friendID)
        {
            int userID = DB_UserTable.data.UserId;
            string message1 = "friend"+ check + userID.ToString() + check + userID.ToString() + "," + friendID.ToString();
            string message2 = "friend"+ check + friendID.ToString() + check + userID.ToString() + "," + friendID.ToString();

            MessageBox.Show(message1 + message2);

            send_data(message1);
            send_data(message2);
            
            // don't forget to save into newFriendTable
        }
        //----------------------------------------------------------------------------- -----------------------------------
        // add new group
        public void addGroup(string member)
        {
            int userID = DB_UserTable.data.UserId;
            string message = "chat:" + userID.ToString() + "," + member + ":1";

            MessageBox.Show(message);

            send_data(message);

            // don't forget to save into newChatTable
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void LoadingTimer_Tick(object sender, EventArgs e)
        {
            



        }

        private void Loading_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime basetime = DateTime.Now;
            while(true)
            {
                //Thread.Sleep(500);
                TimeSpan span = DateTime.Now - basetime;
                //Debug.WriteLine(span.TotalSeconds);
                if (check_init_message)
                {
                    //CountTimer++;
                    //Debug.WriteLine(CountTimer);
                    if (socket != null)
                    {
                        if (socket.State == WebSocketState.Open)
                        {
                            //Debug.WriteLine(DB_UserTable.data);
                            //Debug.WriteLine(DB_MessageTable);
                            if (DB_UserTable.data != null && DB_MessageTable != null)
                            {
                                this.Invoke(new creatform(creatMain));
                                Debug.WriteLine("receive complete and creat Main form-------------");
                                Debug.WriteLine("time span: "+(int)(DateTime.Now - basetime).TotalSeconds);
                                check_init_message = false;
                                CountTimer = 0;
                                return;
                            }
                            //if (CountTimer > 10)
                            if((int)(DateTime.Now-basetime).TotalSeconds > 10)
                            {
                                ResetConnection();
                                CountTimer = 0;
                                check_init_message = false;
                                return;

                            }
                        }
                        else if (socket.State == WebSocketState.Closed)
                        {
                            MessageBox.Show("Fail connection!");
                            ResetConnection();
                            CountTimer = 0;

                            check_init_message = false;
                            return;

                        }
                    }
                }
            }
        }
    }
}
