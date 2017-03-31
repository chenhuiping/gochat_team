using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chat_list
{
    public partial class ViewPicture : Form
    {
        public ViewPicture()
        {
            InitializeComponent();
        }

        private bool LoadCompleted = false;

        

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void pictureBox1_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            LoadCompleted = true;
        }

        public static void SaveImageCapture(System.Drawing.Image image)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.FileName = "Image";// Default file name
            s.DefaultExt = ".Jpg";// Default file extension
            s.Filter = "JPG|*.jpg|GIF|*.gif|PNG|*.png|BMP|*.bmp"; // Filter files by extension
            

            // Below are two examples of setting the initial (default) folder - choose one

            // 1. example of setting the default folder to a special folder
            s.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            // 2 example of setting the default folder to an absolute path
            //s.InitialDirectory = ("C:\\Temp");

            // setting the RestoreDirectory property to true causes the
            // dialog to restore the current directory before closing
            s.RestoreDirectory = true;
            // Show save file dialog box
            // Process save file dialog box results
            if (s.ShowDialog() == DialogResult.OK)
            {
                // Save Image
                string filename = s.FileName;
                // the using statement causes the FileStream's dispose method to be
                // called when the object goes out of scope
                using (System.IO.FileStream fstream = new System.IO.FileStream(filename, System.IO.FileMode.Create))
                {
                    image.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    fstream.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveImageCapture(pictureBox1.Image);
        }
    }
}
