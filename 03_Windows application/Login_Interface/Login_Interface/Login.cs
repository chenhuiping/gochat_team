using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Interface
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox3.Select();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            //textBox1.UseSystemPasswordChar = true;
            textBox1.Text = "";
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }
             

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox1.Text != "")
            {
                if (textBox2.Text == "admin" && textBox1.Text == "admin")
                {
                    Main mainForm = new Main();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong ID or password!");
                }
            }
            else
            {
                MessageBox.Show("Enter your username and password!");
            }

            


            
            
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

       
    }
}
