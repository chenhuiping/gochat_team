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
       
        public chatmesg(string message, string time, msgtype messagetype)
        {

            InitializeComponent();
            Iblmessage.Text = message;
            Ibltime.Text = time;
            if (messagetype.ToString () == "In")
            { //incoming message
                this.BackColor = Color.FromArgb(0,164,147);
            }
            else
            { //outgoing message
                this.BackColor = Color.Gray;
            }
            //lets add the function which adjust the bubble height
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
        }
    }
    public enum msgtype
    {
        In,
        Out
    }
}
