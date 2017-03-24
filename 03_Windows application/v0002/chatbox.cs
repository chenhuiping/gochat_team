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

        //variable for passing receive message
        private string receiveMessage;

        //pass message from login form
        public string getReceiveMessage
        {
            get { return receiveMessage; }
            set { receiveMessage = value; }
            
        }

        public chatbox(Login loginForm, int chatID)
        {
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
        public void addInPicture(string message)
        {
            chatmesg bbl = new chat_list.chatmesg(message, msgtype.In);
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
        public void addInMessage(string message)
        {
            //if (message.Contains(" says : "))
            
            chatmesg bbl = new chat_list.chatmesg(message, msgtype.In);
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
        //------------------------------------------------------------------------------------------------------------------
        // display sent picture/file
        public void addOutPicture(string message)
        {
            chatmesg bbl = new chat_list.chatmesg(message, msgtype.Out);
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
        public void addOutMessage(string message)
        {
            chatmesg bbl = new chat_list.chatmesg(message, msgtype.Out);
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
        //------------------------------------------------------------------------------------------------------------------
        // button to send text message
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
            //send text message to server
            //this.loginForm.send_data("{ \"type\":\"message\", \"data\":[{\"ChatId\":" + this.chatID + ",\"time\":\"" + DateTime.Now.ToShortTimeString() + "\", \"content\":\"" + textBox1.Text + "\",\"from\":" + loginForm.DB_UserTable.data.UserId + ",\"type\":0}]}");
            this.loginForm.send_data("message" + check + this.chatID + check + DateTime.Now.ToShortTimeString() + check + textBox1.Text + check + loginForm.DB_UserTable.data.UserId + check + "0");

            //this.loginForm.getSendMessage = textBox1.Text;
            //this.loginForm.sendMessage();

            //display the sent message in chatbox
            if (textBox1.Text != null)
            {

            //addOutMessage(textBox1.Text);
            }

            //clear the text box
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
            dlg.ShowDialog();
            //txtFile.Text = dlg.FileName;
            fileName = dlg.FileName;
            //string shortFileName = Path.GetFileName(fileName);
            //string ipAddress = txtIPAddress.Text;
            //int port = int.Parse(txtHost.Text);
            //string fileName = txtFile.Text;
            string shortFileName = Path.GetFileName(fileName);
            //Task.Factory.StartNew(() => SendFile(ipAddress, port, fileName, shortFileName));
            MessageBox.Show("File Sent");
            //loginForm.sendFile(fileName, shortFileName);
        }

       
    }
}
