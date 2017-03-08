using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
//using System.Threading.Tasks;

namespace TCP_handle
{
    class handleClient
    {
        TcpClient clientSocket = null;
        public Dictionary<TcpClient, string> clientList = null;

        public void startClient(TcpClient clientSocket, Dictionary<TcpClient, string> clientList)
        {
            this.clientSocket = clientSocket;
            this.clientList = clientList;

            Thread t_hanlder = new Thread(doChat);
            t_hanlder.IsBackground = true;
            t_hanlder.Start();
        }

        public delegate void MessageDisplayHandler(string message, string user_name);
        public event MessageDisplayHandler OnReceived;

        public delegate void DisconnectedHandler(TcpClient clientSocket);
        public event DisconnectedHandler OnDisconnected;

        private void doChat()
        {
            NetworkStream stream = null;
            try
            {
                byte[] buffer = new byte[1024];
                string msg = string.Empty;
                int bytes = 0;
                int MessageCount = 0;
                string filename = "";
                int filesize = 0;
                int bufferSize = 1024;
                while (true)
                {
                  
                        MessageCount++;
                    stream = clientSocket.GetStream();
                    bytes = stream.Read(buffer, 0, buffer.Length);
                    msg = Encoding.Unicode.GetString(buffer, 0, bytes);
                    if (msg.Contains("$"))
                    { 
                        msg = msg.Substring(0, msg.IndexOf("$"));

                    if (OnReceived != null)
                        OnReceived(msg, clientList[clientSocket].ToString());
                    }
                    else
                    {
                        string[] splitted = msg.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                        Dictionary<string, string> headers = new Dictionary<string, string>();
                        foreach (string s in splitted)
                        {
                            if (s.Contains(":"))
                            {
                                headers.Add(s.Substring(0, s.IndexOf(":")), s.Substring(s.IndexOf(":") + 1));
                            }

                        }
                        //Get filesize from header
                        filesize = Convert.ToInt32(headers["Content-length"]);
                        //Get filename from header
                        filename = headers["Filename"];

                        int bufferCount = Convert.ToInt32(Math.Ceiling((double)filesize / (double)bufferSize));


                        FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);

                        while (filesize > 0)
                        {
                            buffer = new byte[bufferSize];

                            int size = stream.Read(buffer, 0, buffer.Length);

                            fs.Write(buffer, 0, size);

                            filesize -= size;
                        }


                        fs.Close();
                    }
                }
            }
            catch (SocketException se)
            {
                Trace.WriteLine(string.Format("doChat - SocketException : {0}", se.Message));

                if (clientSocket != null)
                {
                    if (OnDisconnected != null)
                        OnDisconnected(clientSocket);

                    clientSocket.Close();
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("doChat - Exception : {0}", ex.Message));

                if (clientSocket != null)
                {
                    if (OnDisconnected != null)
                        OnDisconnected(clientSocket);

                    clientSocket.Close();
                    stream.Close();
                }
            }
        }

    }
}
