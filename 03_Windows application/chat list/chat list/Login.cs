using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCP_handle;

namespace chat_list
{
    public partial class Login : Form
    {

        //for TCP connection
        /*
        private TcpClient client;
        public StreamReader STR;
        public StreamWriter STW;
        public String text_to_send;
        public string recieve;
        */
        TcpClient clientSocket = new TcpClient();
        NetworkStream stream = default(NetworkStream);

        string message = string.Empty;

        private int PORT;// = Int32.Parse(txtPNumber.Text.Trim());
        private String IP;

        //for calling chat form - defining chat room
        private Form1 MainForm;

        //defining chatbox
        private chatbox newChat;

        //variable for sent message
        private string sentMessage;

        public Login()
        {
            InitializeComponent();
        }

        private void usernameTextBox_Click(object sender, EventArgs e)
        {
            usernameTextBox.Text = "";
        }

        private void passwordTextBox_Click(object sender, EventArgs e)
        {
            passwordTextBox.Text = "";
            passwordTextBox.UseSystemPasswordChar = true;
        }

        private void usernameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (usernameTextBox.Text != "" && passwordTextBox.Text != "")
            {
                if (usernameTextBox.Text == passwordTextBox.Text)
                {
                    /*Form1 MainForm = new Form1(this);
                    MainForm.Show();
                    this.Hide();
                    */

                    //make a connection to server

                    PORT = Int32.Parse("9000");

                    string myHostname = Dns.GetHostName();
                    string myIP = Dns.GetHostByName(myHostname).AddressList[0].ToString();
                    IP = myIP;
                    clientSocket.Connect(IP, PORT);
                    stream = clientSocket.GetStream();

                    //message = "Connected to Chat Server";
                    //DisplayText(message);
                    
                    if (clientSocket.Connected)
                    {
                        MessageBox.Show("Connected to Server");
                        MainForm = new Form1(this);
                        MainForm.Show();
                        this.Hide();

                        newChat = new chatbox(this);

                        //defining the username
                        byte[] buffer = Encoding.Unicode.GetBytes("Anis" + "$");
                        stream.Write(buffer, 0, buffer.Length);
                        stream.Flush();

                        Thread t_handler = new Thread(GetMessage);
                        t_handler.IsBackground = true;
                        t_handler.Start();
                    }                
                                      
                }
                else
                {
                    MessageBox.Show("Wrong username or password!");
                }
            }
            else
            {
                MessageBox.Show("Enter your username and password!");
            }
        }
               

        public void GetMessage()
        {
            while (true)
            {
                stream = clientSocket.GetStream();
                int BUFFERSIZE = clientSocket.ReceiveBufferSize;
                byte[] buffer = new byte[BUFFERSIZE];
                int bytes = stream.Read(buffer, 0, buffer.Length);

                string message = Encoding.Unicode.GetString(buffer, 0, bytes);
                //MessageBox.Show(message);
                //this.newChat.addInMessage(message, DateTime.Now.ToShortTimeString());
                
                DisplayText(message);
                //chatbox newChat = new chatbox();
            }
        }

        private void DisplayText(string receiveText)
        {
            this.newChat.getReceiveMessage = receiveText;
            //           MessageBox.Show(receiveText);
            MainForm.chatbox1.temp_recieve = receiveText;
            //MainForm.chatbox1.addInMessage(receiveText, DateTime.Now.ToShortTimeString());
            this.newChat.addInMessage(receiveText, DateTime.Now.ToShortTimeString());
            /*
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.BeginInvoke(new MethodInvoker(delegate
                {
                    richTextBox1.AppendText(text + Environment.NewLine);
                }));
            }
            else
                richTextBox1.AppendText(text + Environment.NewLine);
            */
        }
        

        //get message from chatBox
        public string getSendMessage
        {
            get { return sentMessage; }
            set { sentMessage = value; }
        }



        //send message
        public void sendMessage()
        {
            byte[] buffer = Encoding.Unicode.GetBytes(sentMessage + "$");
            //MessageBox.Show(sentMessage);
            
            if (clientSocket.Connected)
            {
               //MessageBox.Show("Client connected");
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
            else
            {
                MessageBox.Show("Something wrong!");
            }


        }


        //close button function
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }


        // make the windows move
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
    }
}
