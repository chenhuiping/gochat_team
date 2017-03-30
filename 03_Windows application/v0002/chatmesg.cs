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
    public partial class chatmesg : UserControl
    {
        public chatmesg()
        {
            
                InitializeComponent();
            //lets add the function which adjust the bubble height
        }
       
        public void setimage(string filepath)
        {
            Debug.WriteLine(filepath);
            if (filepath[0] == '.')
            {
                filepath = filepath.Substring(1, filepath.Length - 1);
            }
            if (filepath[0] != '/')
            {
                filepath = "/" + filepath;
            }
            Debug.WriteLine(filepath);

            pictureBox1.ImageLocation = "http://47.91.75.150:1337" + filepath;
            while(LoadCompleted)
            {
                
            }
        }

        public chatmesg(string message, msgtype messagetype, string username, string time, int type)
        {

            InitializeComponent();
            if (message.Contains("/"))
            {
                if(type == 1)
                {
                    //Image img = Image.FromFile(message);
                    //pictureBox1.Image = img;
                    Debug.WriteLine(message);
                    if (message[0] == '.')
                    {
                        message = message.Substring(1, message.Length - 1);
                    }
                    if(message[0] != '/')
                    {
                        message = "/" + message;
                    }
                    Debug.WriteLine(message);

                    pictureBox1.ImageLocation = "ws://47.91.75.150:1337" + message;

                    pictureBox1.Visible = true;
                    pictureBox1.Refresh();
                }
                else if(type == 2)
                {
                   
                }
                //Debug.WriteLine("filepath!!!!!!");
                // Image img = Image.FromFile(message);
                //pictureBox1.Image = img;
                //pictureBox1.ImageLocation = message;

                // pictureBox1.Visible = true;
                // pictureBox1.Refresh();

                //does the work here 
                //storing and retreiving values from datadase

                //pictureBox1.Visible = false;
            }
            else {
                lblUser.Text = username;
                Iblmessage.Text = message;
                Ibltime.Text = time;
                if (messagetype.ToString() == "In")
                { //incoming message
                    this.BackColor = Color.White;
                }
                else
                { //outgoing message
                    this.BackColor = Color.FromArgb(157,234,128);
                }
                //lets add the function which adjust the bubble height
                
            }
            Setheight();
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

        private void chatmesg_Load(object sender, EventArgs e)
        {
            Setheight();
        }
        private bool LoadCompleted = false;
        private void pictureBox1_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            LoadCompleted = true;
        }
    }
    public enum msgtype
    {
        In,
        Out
    }
}
