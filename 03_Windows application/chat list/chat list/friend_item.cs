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
    public partial class friend_item : UserControl
    {
        public friend_item()
        {
            InitializeComponent();
        }
        public friend_item(string message, string time, msgtype1 messagetype)
        {
            InitializeComponent();
            userName.Text = message;
            userInformation.Text = time;
            if (messagetype.ToString() == "In")
            { //incoming message
                this.BackColor = Color.FromArgb(0, 164, 147);

            }

            //lets add the function which adjust the bubble height
            Setheight();
        }
        void Setheight()
        {
            Size maxSize = new Size(495, int.MaxValue);
            Graphics g = CreateGraphics();
            SizeF size = g.MeasureString(userName.Text, userName.Font, userName.Width);
            userName.Height = int.Parse(Math.Round(size.Height + 2, 0).ToString());
            userInformation.Top = userName.Bottom;
            this.Height = userInformation.Bottom + 10;

        }
        private void friend_item_Load(object sender, EventArgs e)
        {
            Setheight();
        }
    }
    public enum msgtype1
    {
        In,
        Out
    }
}
