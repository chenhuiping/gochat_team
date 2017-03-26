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
    public partial class friend_list : UserControl
    {
        private friend_item bbl_old;

        //defining loginForm
        public Login loginForm;

        public string temp_recieve;
        public Form1 mainForm;

        public void set_default_color()
        {
            /*foreach(friend_item temp in bbl_saved)
            {
                this.BackColor = Color.FromArgb(0, 164, 147);
            }*/
        }

        public friend_list(Form1 mainForm)
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            this.mainForm = mainForm;
            InitializeComponent();
            this.fItem = new chat_list.friend_item(mainForm);
            bbl_old = new friend_item(mainForm);
            bbl_old.Top = 0 - bbl_old.Height + 10;

            //addInMessage("user 1", "00:00", 0);

            //make sure the scroll works
            panel1.VerticalScroll.Value = panel1.VerticalScroll.Maximum;

        }
                     
        //variable for passing receive message
        private string receiveMessage;

        //pass message from login form
        public string getReceiveMessage
        {
            get { return receiveMessage; }
            set { receiveMessage = value; }

        }

        public void addInMessage(string message, string time, int userID, int itemType, string path)
        {
            friend_item bbl = new chat_list.friend_item(mainForm, message, time,  msgtype1.In, userID, itemType, path);
            bbl.Location = fItem.Location;
            bbl.Size = fItem.Size;
            bbl.Anchor = fItem.Anchor;
            //if (bbl.Location != bubble1.Location)

            bbl.Top = bbl_old.Bottom + 1;
            //curtop = bbl.Bottom + 10;
            panel1.Controls.Add(bbl);
            panel1.VerticalScroll.Value = panel1.VerticalScroll.Maximum;

            bbl_old = bbl;// save the last added object

        }

       

        
    }
}
