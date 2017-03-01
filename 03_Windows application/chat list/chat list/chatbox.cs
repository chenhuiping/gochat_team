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

namespace chat_list
{
    public partial class chatbox : UserControl
    {
        private chatmesg bbl_old;// = new chatmesg();

        //defining loginForm
        public Login loginForm;

        private string Message;

        //variable for passing receive message
        private string receiveMessage;

        //pass message from login form
        public string getReceiveMessage
        {
            get { return receiveMessage; }
            set { receiveMessage = value; }
            
        }

        public chatbox(Login loginForm)
        {
            //if (!this.DesignMode)
            //{
            bbl_old = new chatmesg();
            this.loginForm = loginForm;
            InitializeComponent();
            // }

            bbl_old.Top = 0 - bbl_old.Height + 10;
            Message = textBox1.Text;
        }
        public void init()
        {
            bbl_old = new chatmesg();
            //this.loginForm = loginForm;
            InitializeComponent();
        }

        int curtop = 10;
        int lst = 0;

        public string temp_recieve;



        public void addInMessage(string message, string time)
        {

            chatmesg bbl = new chat_list.chatmesg(message, time, msgtype.In);
            bbl.Location = bubble1.Location;
            bbl.Size = bubble1.Size;
            bbl.Anchor = bubble1.Anchor;
            //if (bbl.Location != bubble1.Location)
            
                bbl.Top = bbl_old.Bottom + 10;
                //curtop = bbl.Bottom + 10;
                panel1.Controls.Add(bbl);
                panel1.VerticalScroll.Value = panel1.VerticalScroll.Maximum;

                bbl_old = bbl;// safe the last added object
          
            
        }

        public void addOutMessage(string message, string time)
        {
            chatmesg bbl = new chat_list.chatmesg(message, time, msgtype.Out);
            bbl.Location = bubble1.Location;
            bbl.Left += 20;
            bbl.Size = bubble1.Size;
            bbl.Anchor = bubble1.Anchor;
           
                bbl.Top = bbl_old.Bottom + 10;
                //curtop = bbl.Bottom + 10;
                panel1.Controls.Add(bbl);
                panel1.VerticalScroll.Value = panel1.VerticalScroll.Maximum;
                bbl_old = bbl;
            // safe the last added object
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.loginForm.getSendMessage = textBox1.Text;
            this.loginForm.sendMessage();

            //addInMessage("Hello world", "00:00");

            //display the sent message in chatbox
            addOutMessage(textBox1.Text, DateTime.Now.ToShortTimeString());

            //clear the text box
            textBox1.Text = "";

            //make sure the scroll works
            panel1.VerticalScroll.Value = panel1.VerticalScroll.Maximum;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(temp_recieve != null)
            {
                addInMessage(temp_recieve, DateTime.Now.ToShortTimeString());
                temp_recieve = null;
            }
        }


        /*
        public void clear()
        {
            bbl_old = new chatmesg();
            
        }*/
    }
}
