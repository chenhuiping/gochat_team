using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCP_handle;
using System.Net.NetworkInformation;
using System.IO;
using System.Diagnostics;
using System.Globalization;

namespace chat_list
{
    public partial class chatbox : UserControl
    {
        private chatmesg bbl_old;// = new chatmesg();
        private static string fileName = "";
        
        //defining loginForm
        public Login loginForm;
        public int chatID;
        private string Message;
        string check = "$";
        private string url = "ftp://47.91.75.150:21//uploads//";


        //variable for passing receive message
        private string receiveMessage;

        //pass message from login form
        public string getReceiveMessage
        {
            get { return receiveMessage; }
            set { receiveMessage = value; }
            
        }

        public object Enviroment { get; private set; }

        public void chatbox_visible(bool enable)
        {
            if(enable)
            {
                panel5.Visible = true;
            }
            else
            {
                panel5.Visible = false;
            }
        }
     

        public chatbox(Login loginForm, int chatID)
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            bbl_old = new chatmesg();
            this.loginForm = loginForm;
            this.chatID = chatID;
            InitializeComponent();

            bbl_old.Top = 0 - bbl_old.Height + 10;
            Message = textBox1.Text;
        }


        public string temp_recieve;

        //------------------------------------------------------------------------------------------------------------------
        // display received picture/file
        public void addInPicture(string message, string username, string time, int type)
        {
            chatmesg bbl = new chat_list.chatmesg(message, msgtype.In, username, time, type);
            //message = "https://www.w3schools.com/css/img_fjords.jpg";
            bbl.setimage(message);
            //while (bbl.p)
            bbl.Location = bubble1.Location;
            bbl.Size = bubble1.Size;
            bbl.Anchor = bubble1.Anchor;
            //if (bbl.Location != bubble1.Location)

            bbl.Top = bbl_old.Bottom + 10;
            //curtop = bbl.Bottom + 10;
            panel5.Controls.Add(bbl);
            panel5.VerticalScroll.Value = panel5.VerticalScroll.Maximum;

            bbl_old = bbl;// safe the last added object

        }
        //------------------------------------------------------------------------------------------------------------------
        // display received text message
        public void addInMessage(string message, string username, string time, int type)
        {
            //if (message.Contains(" says : "))

            if (type == 1)
            {
                addInPicture(message, username, time, type);
            }
            else if (type == 2)
            {

            }
            else
            {
                chatmesg bbl = new chat_list.chatmesg(message, msgtype.In, username, time, type);
                //message = "https://www.w3schools.com/css/img_fjords.jpg";
                //bbl.setimage(message);
                bbl.Location = bubble1.Location;
                bbl.Size = bubble1.Size;
                bbl.Anchor = bubble1.Anchor;
                //if (bbl.Location != bubble1.Location)

                bbl.Top = bbl_old.Bottom + 10;
                //curtop = bbl.Bottom + 10;
                panel5.Controls.Add(bbl);
                panel5.VerticalScroll.Value = panel5.VerticalScroll.Maximum;

                bbl_old = bbl;// safe the last added object     
            }
                      
        }
        //------------------------------------------------------------------------------------------------------------------
        // display sent picture/file
        public void addOutPicture(string message, string username, string time, int type)
        {

            chatmesg bbl = new chat_list.chatmesg(message, msgtype.Out, username, time, type);
            //message = "https://www.w3schools.com/css/img_fjords.jpg";
            bbl.setimage(message);
            //while (bbl.p)
            bbl.Location = bubble1.Location;
            bbl.Left += 20;
            bbl.Size = bubble1.Size;
            bbl.Anchor = bubble1.Anchor;
            //if (bbl.Location != bubble1.Location)

            bbl.Top = bbl_old.Bottom + 10;
            //curtop = bbl.Bottom + 10;
            panel5.Controls.Add(bbl);
            panel5.VerticalScroll.Value = panel5.VerticalScroll.Maximum;

            bbl_old = bbl;// safe the last added object

            


        }

        //------------------------------------------------------------------------------------------------------------------
        // display sent text message
        public void addOutMessage(string message, string username, string time, int type)
        {

            if (type == 1)
            {
                addOutPicture(message, username, time, type);
            }
            else if (type == 2)
            {

            }
            else
            {
                chatmesg bbl = new chat_list.chatmesg(message, msgtype.Out, username, time, type);
                bbl.Location = bubble1.Location;
                bbl.Left += 20;
                bbl.Size = bubble1.Size;
                bbl.Anchor = bubble1.Anchor;

                bbl.Top = bbl_old.Bottom + 10;
                //curtop = bbl.Bottom + 10;
                panel5.Controls.Add(bbl);
                panel5.VerticalScroll.Value = panel5.VerticalScroll.Maximum;
                bbl_old = bbl;
                // safe the last added object
            }
        }
        //------------------------------------------------------------------------------------------------------------------
        // button to send text message
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            send_data();                      
        }
        private void send_data()
        {
            string tTex = textBox1.Text;
            tTex = tTex.Replace(System.Environment.NewLine, "");

            
            if (tTex != null)
            {
                
                this.loginForm.send_data("message" + check + this.chatID + check + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture) + check + tTex + check + loginForm.userinfo.UserId + check + "0");
            }
            textBox1.Text = "";

            //make sure the scroll works
            panel5.VerticalScroll.Value = panel5.VerticalScroll.Maximum;
        }
        //-------------------------------------------------------------------------------------------------------------------
    
        //-------------------------------------------------------------------------------------------------------------------
        // button to choose file/picture

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG|*.jpg|GIF|*.gif|PNG|*.png|BMP|*.bmp";
            dlg.Title = "File Sharing Client";
            //txtFile.Text = dlg.FileName;
            string fileToUpload;

            if (dlg.ShowDialog() == DialogResult.OK)
                fileToUpload = dlg.FileName;
            else
                fileToUpload = string.Empty;
            this.loginForm.setImageInforToSend(fileToUpload, "1", url, chatID.ToString());



            
            //MessageBox.Show("File Sent");
            //loginForm.sendFile(fileName, shortFileName);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                send_data();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            //dlg.Filter = "JPG|*.jpg|GIF|*.gif|PNG|*.png|BMP|*.bmp";
            dlg.Title = "File Sharing Client";
            //txtFile.Text = dlg.FileName;
            string fileToUpload;

            if (dlg.ShowDialog() == DialogResult.OK)
                fileToUpload = dlg.FileName;
            else
                fileToUpload = string.Empty;
            this.loginForm.setImageInforToSend(fileToUpload, "2", url, chatID.ToString());
        }
    }
}
