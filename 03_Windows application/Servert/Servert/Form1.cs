using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Servert
{
    public partial class Form1 : Form
    {
        private TcpListener ServerTCPListner;
        private static long ConnectedSockedCount = 0;
        private Hashtable HTSocketHolder = new Hashtable();
        private Hashtable HTThreadHolder = new Hashtable();

        private int MaximumConnected = 5000;
        private Socket TCPListnerAcceptSocket;
        private int PORT;


        private string txttemp = null;

        


        Byte[] Standart_img_by;
        Byte[] Compose_img_by;
        int count = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void senddata(string bit)
        {
            if (TCPListnerAcceptSocket.Connected)
            {
                Byte[] SendBuffer;
                string strBuffer;

                //strBuffer = String.Format("{0}", txtMsg.Text.Trim());
                strBuffer = bit;
                SendBuffer = System.Text.Encoding.Unicode.GetBytes(strBuffer);
                TCPListnerAcceptSocket.Send(SendBuffer, 0, SendBuffer.Length, 0);
                txtMsg.Text = "sent data to client";
            }
        }
        private void btn_strat_Click(object sender, EventArgs e)
        {
            if (txtPNumber.Text.Trim() == "")
            {
                MessageBox.Show("insert port number");
                txtPNumber.Focus();
                return;
            }

            PORT = Int32.Parse(txtPNumber.Text.Trim());
            ServerTCPListner = new TcpListener(PORT);
            ServerTCPListner.Start();

            txtMsg.Text = "start socket server";

            Thread TCPServerThread = new Thread(new ThreadStart(WaitingFromClientConnect));

            HTThreadHolder.Add(ConnectedSockedCount, TCPServerThread);

            TCPServerThread.Start();

            this.btn_send.Enabled = false;
            this.btn_send.Enabled = true;
        }
        private void StopTheThread(long RealConnectedSocket)
        {
            Thread RemoveThread = (Thread)HTThreadHolder[RealConnectedSocket];

            lock (this)
            {
                HTSocketHolder.Remove(RealConnectedSocket);
                HTThreadHolder.Remove(RealConnectedSocket);
            }
            RemoveThread.Abort();
        }
        private void ReadingServerSocket()
        {
            long RealConnectedSocket = ConnectedSockedCount;
            Socket ReadServerSocket = (Socket)HTSocketHolder[RealConnectedSocket];

            while (true)
            {
                if (ReadServerSocket.Connected)
                {
                    Byte[] ReceiveByte = new Byte[1024];
                    try
                    {
                        int nVlaue = ReadServerSocket.Receive(ReceiveByte, ReceiveByte.Length, 0);
                        if (nVlaue > 0)
                        {
                            String ReceiveData = System.Text.Encoding.UTF8.GetString( ReceiveByte);

                            this.txtMsg.Invoke(new MethodInvoker(delegate () { txtMsg.AppendText("you : " + ReceiveData + "\n"); }));

                     
 /*
                            if (ReceiveByte[0] == 'U')
                            {
                               
                                //txtMsg.Text = "receive userdata";
                                if (ReceiveByte[1] == 'C')
                                {
                                    senddata("TS");
                                    Thread.Sleep(1);
                                }
                                if (ReceiveByte[1] == 'O')
                                {

                                }
                            }
                            ReceiveData = System.Text.Encoding.UTF8.GetString(ReceiveByte);
                            txttemp = ReceiveData;
                       */
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (!ReadServerSocket.Connected)
                        {

                            txttemp = "client end";
                            break;
                        }
                    }
                }
            }

            StopTheThread(RealConnectedSocket);
        }
        private void WaitingFromClientConnect()
        {
            while (true)
            {
                TCPListnerAcceptSocket = ServerTCPListner.AcceptSocket();

                if (ConnectedSockedCount < 5000)
                {
                    Interlocked.Increment(ref ConnectedSockedCount);
                }
                else
                {
                    ConnectedSockedCount = 1;
                }

                if (HTSocketHolder.Count < MaximumConnected)
                {
                    while (HTSocketHolder.Contains(ConnectedSockedCount))
                    {
                        Interlocked.Increment(ref ConnectedSockedCount);
                    }

                    Thread myServerThread = new Thread(new ThreadStart(ReadingServerSocket));

                    lock (this)
                    {
                        HTSocketHolder.Add(ConnectedSockedCount, TCPListnerAcceptSocket);
                        HTThreadHolder.Add(ConnectedSockedCount, myServerThread);
                    }

                    myServerThread.Start();
                    txttemp = "Client connect";
                }
            }
        }
        private void btn_end_Click(object sender, EventArgs e)
        {
            if (ServerTCPListner != null)
            {
                ServerTCPListner.Stop();
            }

            MessageBox.Show("server ended.!!");

            foreach (Socket SocketStart in HTSocketHolder.Values)
            {
                if (SocketStart.Connected)
                {
                    SocketStart.Close();
                }
            }

            foreach (Thread ThreadStart in HTThreadHolder.Values)
            {
                if (ThreadStart.IsAlive)
                {
                    ThreadStart.Abort();
                }
            }

            Application.Exit();
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            //if (TCPListnerAcceptSocket.Connected)
            if (TCPListnerAcceptSocket != null)
            {

                if (TCPListnerAcceptSocket.Connected)
                {
                    Byte[] SendBuffer;
                    //                string strBuffer;

                    SendBuffer = null;// = bitmapToByteArray();

                    TCPListnerAcceptSocket.Send(SendBuffer, 0, SendBuffer.Length, 0);
                    //strBuffer = String.Format("{0}", txtMsg.Text.Trim());
                    //                SendBuffer = System.Text.Encoding.Unicode.GetBytes(strBuffer);



                    //TCPListnerAcceptSocket.Send(SendBuffer, 0, SendBuffer.Length, 0);
                    txtMsg.Text = "sent to the client";
                }
            }
        }
    }
}
