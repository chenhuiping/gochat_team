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
using System.Net;
using System.IO;

namespace chat_list
{
    public partial class chatmesg : UserControl
    {

        public chatmesg()
        {
            
                InitializeComponent();
            //lets add the function which adjust the bubble height
        }
       
        public void setimage(string filepath)
        {
            //Debug.WriteLine(filepath);
            if (filepath[0] == '.')
            {
                filepath = filepath.Substring(1, filepath.Length - 1);
            }
            if (filepath[0] != '/')
            {
                filepath = "/" + filepath;
            }
            ftpPath = "ftp://47.91.75.150/" + filepath;
            string filename=null;
            Debug.WriteLine(filepath);
            if (filepath.Contains("/"))
                filename = filepath.Substring(filepath.LastIndexOf("/") + 1);
            else if (filepath.Contains("\\"))
                filename = filepath.Substring(filepath.LastIndexOf("\\") + 1);

            this.filename = filename;

            if (this.type == 1)
            {
                
                pictureBox1.ImageLocation = "http://47.91.75.150/" + "gochat" + filepath;
                while (LoadCompleted)
                {

                }
                pictureBox1.Visible = true;

            }
            else
            {
                pictureBox1.Visible = true;
            }
            pictureBox1.Refresh();
            
            
        }

        public chatmesg(string message, msgtype messagetype, string username, string time, int type)
        {
            this.type = type;
             InitializeComponent();
            
            if(type == 1)
            {
                //Image img = Image.FromFile(message);
                //pictureBox1.Image = img;
                //Debug.WriteLine(message);
                if (message[0] == '.')
                {
                    message = message.Substring(1, message.Length - 1);
                }
                if(message[0] != '/')
                {
                    message = "/" + message;
                }
                //Debug.WriteLine(message);

                this.message = message;

                //pictureBox1.ImageLocation = "http://47.91.75.150" + message;

                //pictureBox1.Visible = true;
                //pictureBox1.Refresh();
                Iblmessage.Text = message.Substring(message.LastIndexOf("/") + 1);

            }
            else if(type == 2)
            {
                Iblmessage.Text = message.Substring(message.LastIndexOf("/") + 1);

            }
            else {
                //lets add the function which adjust the bubble height
                Iblmessage.Text = message;
            }
            lblUser.Text = username;
            //Iblmessage.Text = message;
            Ibltime.Text = time;
            if (messagetype.ToString() == "In")
            { //incoming message
                this.BackColor = Color.White;
            }
            else
            { //outgoing message
                this.BackColor = Color.FromArgb(157, 234, 128);
            }
            Setheight();
        }

        void getFilename(string str)
        {

        }


        void Setheight()
        {
            Size maxSize = new Size(495, int.MaxValue);
            Graphics g = CreateGraphics();
            Iblmessage.Top = lblUser.Bottom + 5;
            SizeF size = g.MeasureString(Iblmessage.Text, Iblmessage.Font, Iblmessage.Width);
            Iblmessage.Height = int.Parse(Math.Round(size.Height + 2, 0).ToString());
            //Ibltime.Top = Iblmessage.Bottom;

            this.Height = Iblmessage.Bottom + 10;
            //this.Height = Ibltime.Bottom + 10;

        }

        void SetpicHeight()
        {

        }

        private void chatmesg_Load(object sender, EventArgs e)
        {
            Setheight();
        }
        private bool LoadCompleted = false;
        private int type;
        private string message;
        private string ftpPath;
        private string filename;

        private void pictureBox1_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            LoadCompleted = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "File download Client";
            dlg.FileName = filename;
            //txtFile.Text = dlg.FileName;
            string fileToUpload;


            if (dlg.ShowDialog() == DialogResult.OK)
                fileToUpload = dlg.FileName;
            else
                fileToUpload = string.Empty;

            string localPath = fileToUpload;

            string fileName = this.filename;

            try
            {
                FtpWebRequest requestFileDownload = (FtpWebRequest)WebRequest.Create(ftpPath);
                requestFileDownload.Credentials = new NetworkCredential("user", "user");
                requestFileDownload.Method = WebRequestMethods.Ftp.DownloadFile;

                FtpWebResponse responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse();

                Stream responseStream = responseFileDownload.GetResponseStream();
                FileStream writeStream = new FileStream(localPath + fileName, FileMode.Create);

                int Length = 2048;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);

                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                }

                responseStream.Close();
                writeStream.Close();

                requestFileDownload = null;
                responseFileDownload = null;
            }
            catch (Exception ea)
            {
                Debug.WriteLine(ea);
            }
            
        }
    }
    public enum msgtype
    {
        In,
        Out
    }
}
