using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            pictureBox1.ImageLocation = filepath;
            while(LoadCompleted)
            {
                
            }
        }

        public chatmesg(string message, msgtype messagetype)
        {

            InitializeComponent();
            if (message.Contains("/"))
            {
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
                Iblmessage.Text = message;
                Ibltime.Text = DateTime.Now.ToShortTimeString();
                if (messagetype.ToString() == "In")
                { //incoming message
                    this.BackColor = Color.FromArgb(0, 164, 147);
                }
                else
                { //outgoing message
                    this.BackColor = Color.Gray;
                }
                //lets add the function which adjust the bubble height
                
            }
            Setheight();
        }
        
       
        void Setheight()
            { Size maxSize = new Size(495, int.MaxValue);
                Graphics g = CreateGraphics();
                SizeF size = g.MeasureString(Iblmessage.Text, Iblmessage.Font, Iblmessage.Width);
            Iblmessage.Height = int.Parse(Math.Round(size.Height + 2, 0).ToString());
            Ibltime.Top = Iblmessage.Bottom;
                this.Height = Ibltime.Bottom + 10;

            }

        private void chatmesg_Load(object sender, EventArgs e)
        {
            Setheight();
            //PictureBox pictureBox1 = new PictureBox();
            //pictureBox1.Location = new System.Drawing.Point(194, 10);
            //pictureBox1.Name = "pictureBox1";
            //pictureBox1.Size = new System.Drawing.Size(108, 92);
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
