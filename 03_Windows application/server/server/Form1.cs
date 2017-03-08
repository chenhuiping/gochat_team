using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCP_handle;

namespace server
{
    public partial class Form1 : Form
    {
        TcpListener server = null;
        TcpClient clientSocket = null;
        static int counter = 0;
        private int PORT;
        private bool isStrated = false;
        

        public Dictionary<TcpClient, string> clientList = new Dictionary<TcpClient, string>();

        public Form1()
        {
            InitializeComponent();

            this.Start.Enabled = true;
            this.End.Enabled = false;
            txtPNumber.Focus();

           // Thread t = new Thread(InitSocket);
            //t.IsBackground = true;
           // t.Start();
        }
        private void InitSocket()
        {
            PORT = Int32.Parse(txtPNumber.Text.Trim());
            server = new TcpListener(IPAddress.Any, PORT);
            clientSocket = default(TcpClient);
            server.Start();
            DisplayText(">> Server Started");

            while (true)
            {
                try
                {
                    counter++;
                    clientSocket = server.AcceptTcpClient();
                    DisplayText(">> Accept connection from client");


                    NetworkStream stream = clientSocket.GetStream();
                    byte[] buffer = new byte[1024];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    string user_name = Encoding.Unicode.GetString(buffer, 0, bytes);
                  
                    user_name = user_name.Substring(0, user_name.IndexOf("$"));

                    clientList.Add(clientSocket, user_name);

                    // send message all user
                    //SendMessageAll(user_name + " Joined ", "", false);

                    handleClient h_client = new handleClient();
                    h_client.OnReceived += new handleClient.MessageDisplayHandler(OnReceived);
                    h_client.OnDisconnected += new handleClient.DisconnectedHandler(h_client_OnDisconnected);
                    h_client.startClient(clientSocket, clientList);
                }
                catch (SocketException se)
                {
                    Trace.WriteLine(string.Format("InitSocket - SocketException : {0}", se.Message));
                    break;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("InitSocket - Exception : {0}", ex.Message));
                    break;
                }
               
            }

            //clientSocket.Close();
            //server.Stop();
        }


        void h_client_OnDisconnected(TcpClient clientSocket)
        {
            if (clientList.ContainsKey(clientSocket))
                clientList.Remove(clientSocket);
        }

        private void OnReceived(string message, string user_name)
        {

            string displayMessage = "From client : " + user_name + " : " + message;
            DisplayText(displayMessage);
            SendMessageAll(message, user_name, true);
        }

        public void SendMessageAll(string message, string user_name, bool flag)
        {
            foreach (var pair in clientList)
            {
                Trace.WriteLine(string.Format("tcpclient : {0} user_name : {1}", pair.Key, pair.Value));

                TcpClient client = pair.Key as TcpClient;
                NetworkStream stream = client.GetStream();
                byte[] buffer = null;

                if (flag)
                {
                    buffer = Encoding.Unicode.GetBytes(user_name + " says : " + message);
                }
                else
                {
                    buffer = Encoding.Unicode.GetBytes(message);
                }

                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
        }

        private void DisplayText(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.BeginInvoke(new MethodInvoker(delegate
                {
                    richTextBox1.AppendText(text + Environment.NewLine);
                }));
            }
            else
                richTextBox1.AppendText(text + Environment.NewLine);
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (txtPNumber.Text.Trim() == "")
            {
                MessageBox.Show("insert port number");
                txtPNumber.Focus();
                return;
            }
            this.Start.Enabled = false;
            this.End.Enabled = true;
            Thread t = new Thread(InitSocket);
            t.IsBackground = true;
            t.Start();
        }

        private void End_Click(object sender, EventArgs e)
        {
            this.Start.Enabled = true;
            this.End.Enabled = false;

            if (clientSocket != null)
            {
                clientSocket.Close();
                clientSocket = null;
            }

            if (server != null)
            {
                server.Stop();
                server = null;
            }
            counter = 0;
        }
    }
}
