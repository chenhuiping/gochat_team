using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wsClientForm
{
    public partial class Form1 : Form
    {
        bool ws_socket_flag = false;
        public int port;
        public string ip;
        public bool ws_flag = false;
        ClientWebSocket socket;
        CancellationTokenSource cts;

        //use delegate to handle UI
        //when the thread try to handle UI, it is not allowed.
        public delegate void set_textbox(string str);
        
        public Form1()
        {
            InitializeComponent();
            
            textBox4.Text = "ws://echo.websocket.org"; //message echo test
            //textBox2.Text = "ws://192.168.0.26:1337"; 

        }

        //when click the connect button, StartWSclient will start as a thread (by background worker)
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Trim() != "" )
            {
                ip = textBox4.Text.Trim();
                if(socket == null)
                {
                    //run backgroundworker
                    this.StartWSClient.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Already connected!!");
                }
            }
            else
            {
                MessageBox.Show("input IP and port");
            }
            
        }
        public async Task StartAsync()
        {
            //textBox2.Text = "Name please: ";
            string name = Console.ReadLine();
            this.Invoke(new set_textbox(text2Set), "connecting");
            //textBox2.Text = "Connecting....";
            cts = new CancellationTokenSource();
            socket = new ClientWebSocket();

            //get ip and port from the ui
            string wsUri = string.Format(textBox4.Text.Trim());
            await socket.ConnectAsync(new Uri(wsUri), cts.Token);
            
            this.Invoke(new set_textbox(text2Set), socket.State.ToString());
            //
            Task.Factory.StartNew(
            async () =>
            {
                var rcvBytes = new byte[1024];
                var rcvBuffer = new ArraySegment<byte>(rcvBytes);
                while (true)
                {
                    WebSocketReceiveResult rcvResult =
                    await socket.ReceiveAsync(rcvBuffer, cts.Token);
                    byte[] msgBytes = rcvBuffer.Skip(rcvBuffer.Offset).Take(rcvResult.Count).ToArray();
                    //-----------------------------
                    OnReceive(msgBytes);
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
        //receive data from server
        //message type is byte
        public void OnReceive(byte[] msgBytes)
        {
            string rcvMsg = Encoding.UTF8.GetString(msgBytes);
            this.Invoke(new set_textbox(text2Set), "Received: {0}" + rcvMsg);
        }
        private void text2Set(string str)
        {
            textBox2.AppendText(str);
            textBox2.AppendText("\r\n");
        }     
        //send data 
        //if the connection is opened, it will call by anywhere in class
        public async Task SendAsync(byte[] message)
        {
            if (socket != null && socket.State == WebSocketState.Open)
            {               
                var sendBuffer = new ArraySegment<byte>(message);
                await socket.SendAsync(sendBuffer, WebSocketMessageType.Text, endOfMessage: true, cancellationToken: cts.Token);
            }
            if(ws_socket_flag)
            {
                cts.Cancel();
            }
        }
        //when the form is closing stop the Websocket connection
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ws_socket_flag = true;
            if(cts != null)
                cts.Cancel();
        }
        //start Websocket connection
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            StartAsync();
        }
        //send data from textbox1
        private void button2_Click(object sender, EventArgs e)
        {
            //max size of the data is 1024 bytes
            byte[] sendBytes = Encoding.UTF8.GetBytes(textBox1.Text.Trim());
            SendAsync(sendBytes);
        }
        //test data send
        private void button3_Click(object sender, EventArgs e)
        {
            string temp;
            temp = "a";
            for (int i = 0; i<1024; i++)
            {
                temp += "a";
            }
            int size = temp.Length;
            byte[] sendBytes = Encoding.UTF8.GetBytes(temp);
            SendAsync(sendBytes);
        }
    }
}
