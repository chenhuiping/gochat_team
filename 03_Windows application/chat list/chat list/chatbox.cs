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
    public partial class chatbox : UserControl
    {
        chatmesg bbl_old = new chatmesg();
        public chatbox()
        {
            //if (!this.DesignMode)
            //{
               InitializeComponent();
            // }

            bbl_old.Top = 0 - bbl_old.Height + 10;
        }

        int curtop = 10;
        int lst = 0;
        


        public void addInMessage(string message, string time)
        { chatmesg bbl = new chat_list.chatmesg(message, time, msgtype.In);
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
            addInMessage("Hello world", "00:00");
            addOutMessage("Hello world to you too", "00:00");
            panel1.VerticalScroll.Value = panel1.VerticalScroll.Maximum;
        }
        public void clear()
        {
            bbl_old = new chatmesg();

        }
    }
}
