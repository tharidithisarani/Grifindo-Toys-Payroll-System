using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace PayRollManagementSystem
{
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
            showEmployee();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Pathum Wijesekara\source\repos\PayRollManagementSystem\PayRollManagementSystem\PayRollDB.mdf ;Integrated Security=True;Connect Timeout=30");
        private void Clear()
        {
            EmpNameTb.Text = "";
            EmpAddTb.Text = "";
            EmpPhoneTb.Text = "";
            EmpSalTb.Text = "";
            EmpGenCb.SelectedIndex = 0;
            EmpPosCb.SelectedIndex = 0;
            EmpQualCb.SelectedIndex = 0;
            Key = 0;
        }
        private void showEmployee()
        {
            Con.Open();
            string Query = "Select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Bilder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeeDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            {
                if (EmpNameTb.Text == "" || EmpPhoneTb.Text == "" || EmpGenCb.SelectedIndex == -1 || EmpAddTb.Text == "" || EmpSalTb.Text == "" || EmpQualCb.SelectedIndex == -1)
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("insert into employeeTbl(EmpName,EmpGen,EmpDOB,EmpPhone,EmpAdd,EmpPos,JoinDate,EmpQual,EmpBasSal)values(@EN,@EG,@ED,@EP,@EA,@EPos,@JD,@EQ,@EBS)", Con);
                        cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                        cmd.Parameters.AddWithValue("@EG", EmpGenCb.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@ED", EmpDOB.Value.Date);
                        cmd.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                        cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                        cmd.Parameters.AddWithValue("@EPos", EmpPosCb.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@JD", JDate.Value.Date);
                        cmd.Parameters.AddWithValue("@EQ", EmpQualCb.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@EBS", EmpSalTb.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Employee Saved");
                        Con.Close();
                        showEmployee();
                        Clear();
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpPhoneTb.Text == "" || EmpGenCb.SelectedIndex == -1 || EmpAddTb.Text == "" || EmpSalTb.Text == "" || EmpQualCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update employeeTbl set EmpName=@EN,EmpGen=@EG,EmpDOB=@ED,EmpPhone=@EP,EmpAdd=@EA,EmpPos=@EPos,JoinDate=@EQ,EmpQual=@JD,EmpBasSal=@EBS Where EmpId=@EmpKey", Con);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@EG", EmpGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@ED", EmpDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                    cmd.Parameters.AddWithValue("@EPos", EmpPosCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@JD", JDate.Value.Date);
                    cmd.Parameters.AddWithValue("@EQ", EmpQualCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EBS", EmpSalTb.Text);
                    cmd.Parameters.AddWithValue("@EmpKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Updated");
                    Con.Close();
                    showEmployee();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from EmployeeTbl Where EmpId=@EmpKey", Con);
                    cmd.Parameters.AddWithValue("@EmpKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee Deleted");
                    Con.Close();
                    showEmployee();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key=0;
        private void EmployeeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EmpNameTb.Text = EmployeeDGV.SelectedRows[0].Cells[1].Value.ToString();
            EmpGenCb.SelectedItem = EmployeeDGV.SelectedRows[0].Cells[2].Value.ToString();
            EmpDOB.Text = EmployeeDGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpPhoneTb.Text = EmployeeDGV.SelectedRows[0].Cells[4].Value.ToString();
            EmpAddTb.Text = EmployeeDGV.SelectedRows[0].Cells[5].Value.ToString();
            EmpPosCb.SelectedItem = EmployeeDGV.SelectedRows[0].Cells[6].Value.ToString();
            JDate.Text = EmployeeDGV.SelectedRows[0].Cells[7].Value.ToString();
            EmpQualCb.SelectedItem = EmployeeDGV.SelectedRows[0].Cells[8].Value.ToString();
            EmpSalTb.Text = EmployeeDGV.SelectedRows[0].Cells[9].Value.ToString();
            if (EmpNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(EmployeeDGV.SelectedRows[0].Cells[1].Value.ToString());
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Bonus obj = new Bonus();
            obj.Show();
            this.Hide();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Homes obj = new Homes();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Advances obj = new Advances();
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

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Homes obj = new Homes();
            obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Focus();
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

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Salary obj = new Salary();
            obj.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Salary obj = new Salary();
            obj.Show();
            this.Hide();
        }
    }
}
