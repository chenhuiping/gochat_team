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
        public friend_list()
        {
            InitializeComponent();
            bbl_old = new friend_item();
            bbl_old.Top = 0 - bbl_old.Height + 10;
            // while (1 + 1 == 2)
            addInMessage("Hello world", "00:00");
            addInMessage("Hell", "00:00");
        }
        public void addInMessage(string message, string time)
        {
            friend_item bbl = new chat_list.friend_item(message, time, msgtype1.In);
            bbl.Location = fItem.Location;
            bbl.Size = fItem.Size;
            bbl.Anchor = fItem.Anchor;
            //if (bbl.Location != bubble1.Location)

            bbl.Top = bbl_old.Bottom + 1;
            //curtop = bbl.Bottom + 10;
            panel1.Controls.Add(bbl);
            panel1.VerticalScroll.Value = panel1.VerticalScroll.Maximum;

            bbl_old = bbl;// safe the last added object


        }

        private void fItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
