using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayRollManagementSystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || UPasswordTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else if (UnameTb.Text == "tharidi" && UPasswordTb.Text == "123")
            {
                Homes obj = new Homes();
                obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Admin Or Password");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
