using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace PayRollManagementSystem
{
    public partial class Attendances : Form
    {
        public Attendances()
        {
            InitializeComponent();
            ShowAttendance();
            GetEmployees();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Pathum Wijesekara\source\repos\PayRollManagementSystem\PayRollManagementSystem\PayRollDB.mdf ;Integrated Security=True;Connect Timeout=30");
        private void Clear()
        {
            EmpNameTb.Text = "";
            PresenceTb.Text = "";
            AbsTb.Text = "";
            ExcusedTb.Text = "";
            Key = 0;

        }
        private void ShowAttendance()
        {
            Con.Open();
            string Query = "Select * from AttendanceTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AttendanceDGV.DataSource = ds.Tables[0];
            Con.Close();

        }
        private void GetEmployees()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select * from EmployeeTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpId", typeof(int));
            dt.Load(Rdr);
            EmpIdCb.ValueMember = "EmpId";
            EmpIdCb.DataSource = dt;
            Con.Close();
        }
        private void GetEmployeeName()
        {
            if (Con.State == ConnectionState.Closed)
            {
                Con.Open();
            } 
            String Query = " Select * from EmployeeTbl Where EmpId= " + EmpIdCb.SelectedValue.ToString() + "";

            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)

            {
                EmpNameTb.Text = dr["EmpName"].ToString();
            }
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || PresenceTb.Text == "" || ExcusedTb.Text == "" || AbsTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    String Period = AttDate.Value.Month + "-" + AttDate.Value.Year;
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into AttendanceTbl(EmpId,EmpName,DayPress,DayAbs,DayExcused,Period) values (@EI,@EN,@DP,@DA,@DE,@Per)", Con);
                    cmd.Parameters.AddWithValue("@EI", EmpIdCb.Text);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@DP", PresenceTb.Text);
                    cmd.Parameters.AddWithValue("@DA", AbsTb.Text);
                    cmd.Parameters.AddWithValue("@DE", ExcusedTb.Text);
                    cmd.Parameters.AddWithValue("@Per", Period);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Attendance Saved");
                    Con.Close();
                    ShowAttendance();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int Key = 0;
        private void AttendanceDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EmpNameTb.Text = AttendanceDGV.SelectedRows[0].Cells[2].Value.ToString();
            EmpIdCb.SelectedItem = AttendanceDGV.SelectedRows[0].Cells[1].Value.ToString();
            PresenceTb.Text = AttendanceDGV.SelectedRows[0].Cells[3].Value.ToString();
            AbsTb.Text = AttendanceDGV.SelectedRows[0].Cells[4].Value.ToString();
            ExcusedTb.Text = AttendanceDGV.SelectedRows[0].Cells[5].Value.ToString();
            AttDate.Text = AttendanceDGV.SelectedRows[0].Cells[3].Value.ToString();

            if (EmpNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(AttendanceDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EmpIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEmployeeName();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || PresenceTb.Text == "" || ExcusedTb.Text == "" || AbsTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    String Period = AttDate.Value.Month + "-" + AttDate.Value.Year;
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update AttendanceTbl Set EmpId=@EI,EmpName=@EN,DayPress=@DP,DayAbs=@DA,DayExcused=@DE,Period=@Per where AttNum=@AttKey", Con);
                    cmd.Parameters.AddWithValue("@EI", EmpIdCb.Text);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@DP", PresenceTb.Text);
                    cmd.Parameters.AddWithValue("@DA", AbsTb.Text);
                    cmd.Parameters.AddWithValue("@DE", ExcusedTb.Text);
                    cmd.Parameters.AddWithValue("@Per", Period);
                    cmd.Parameters.AddWithValue("@AttKey", Key);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Attendance Updated");
                    Con.Close();
                    ShowAttendance();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Homes obj = new Homes();
            obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
            obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Focus();
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
