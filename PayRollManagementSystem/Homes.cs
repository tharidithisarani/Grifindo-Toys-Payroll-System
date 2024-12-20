using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayRollManagementSystem
{
    public partial class Homes : Form
    {
        public Homes()
        {
            InitializeComponent();
            CountEmpployees();
            CountManagers();
            SumSalary();
            CountSeniors();
            SumBonus();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Pathum Wijesekara\source\repos\PayRollManagementSystem\PayRollManagementSystem\PayRollDB.mdf ;Integrated Security=True;Connect Timeout=30");
        private void CountEmpployees()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from EmployeeTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            EmpLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountManagers()
        {
            String Pos = "Manager";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from EmployeeTbl where EmpPos='" + Pos + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ManagerLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountSeniors()
        {
            String Pos = "Senior";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from EmployeeTbl where EmpPos='" + Pos + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SeniorLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void SumSalary()
        {

            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum(EmpBalance) from salaryTbl ", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SalaryLbl.Text = "Rs " + dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void SumBonus()
        {

            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum(EmpBonus) from salaryTbl ", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BonusLbl.Text = "Rs " + dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Attendances obj = new Attendances();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Salary obj = new Salary();
            obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Bonus obj = new Bonus();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Advances obj = new Advances();
            obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
            obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Salary obj = new Salary();
            obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Attendances obj = new Attendances();
            obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Bonus obj = new Bonus();
            obj.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Salary obj = new Salary();
            obj.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}
