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
    public partial class Salary : Form
    {
        public Salary()
        {
            InitializeComponent();
            GetEmployees();
            GetAttendance();
            GetBonus();
            ShowSalary(); 
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Pathum Wijesekara\source\repos\PayRollManagementSystem\PayRollManagementSystem\PayRollDB.mdf ;Integrated Security=True;Connect Timeout=30");
        private void Clear()
        {
            EmpNameTb.Text = "";
            PressTb.Text = "";
            AbsTb.Text = "";
            ExcusedTb.Text = "";
            //Key = 0;

        }
        private void ShowSalary()
        {
            Con.Open();
            string Query = "Select * from SalaryTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SalaryDGV.DataSource = ds.Tables[0];
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
        private void GetBonus()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select * from BonusTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("BName", typeof(string));
            dt.Load(Rdr);
            BonusIdCb.ValueMember = "BName";
            BonusIdCb.DataSource = dt;
            Con.Close();
        }
        private void GetAttendance()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select * from AttendanceTbl where EmpId="+EmpIdCb.SelectedValue.ToString()+"", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("AttNum", typeof(int));
            dt.Load(Rdr);
            AttNumCb.ValueMember = "AttNum";
            AttNumCb.DataSource = dt;
            Con.Close();
        }
        private void GetAttendanceData()
        {
            Con.Open();
            String Query = " Select * from AttendanceTbl where AttNum=" + AttNumCb.SelectedValue.ToString() + "";

            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)

            {
                PressTb.Text = dr["DayPress"].ToString();
                AbsTb.Text = dr["DayAbs"].ToString();
                ExcusedTb.Text = dr["DayExcused"].ToString();
            }

            Con.Close();
        }
        private void GetEmployeeName()
        {
            Con.Open();
            String Query = " Select * from EmployeeTbl where EmpId=" + EmpIdCb.SelectedValue.ToString() + "";

            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)

            {
                EmpNameTb.Text = dr["EmpName"].ToString();
                BaseSalaryTb.Text = dr["EmpBasSal"].ToString();
            }

            Con.Close();
        }
        private void GetBonusAmt()
        {
            Con.Open();
            String Query = " Select * from BonusTbl where BName='" + BonusIdCb.SelectedValue.ToString() + "'";

            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)

            {
                BonusTb.Text = dr["BAmt"].ToString();
                BonusTb.Text = dr["BAmt"].ToString();
            }

            Con.Close();
        }
        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || PressTb.Text == "" || AbsTb.Text == "" || ExcusedTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    String period = SalDate.Value.Month + "." + SalDate.Value.Year;
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into SalaryTbl(EmpId,EmpName,EmpBasSal,EmpBonus,EmpAdvance,EmpTax,EmpBalance,SalPeriod)values(@EI,@EN,@EBS,@Ebon,@EAD,@ETax,@EBalance,@SPer)", Con);
                    cmd.Parameters.AddWithValue("@EI", EmpIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@EBS", BaseSalaryTb.Text);
                    cmd.Parameters.AddWithValue("@Ebon", BonusTb.Text);
                    cmd.Parameters.AddWithValue("@EAd", AdvanceTb.Text);
                    cmd.Parameters.AddWithValue("@ETax", TotTax);
                    cmd.Parameters.AddWithValue("@EBalance", GrdTot);
                    cmd.Parameters.AddWithValue("@SPer", period);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Salary saved");
                    Con.Close();
                    ShowSalary();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        
        private void EmpIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetEmployeeName();
            GetAttendance();
        }
        

        private void BonusIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetBonusAmt();
        }
        

        private void AttNumCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetAttendanceData();
        }
       

        private void SalaryDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 500, 800);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
      

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Team Decorderz", new Font("Averia", 12, FontStyle.Bold), Brushes.Red, new Point(160, 25));
            e.Graphics.DrawString("PayRoll Management System 1.0", new Font("Averia", 10, FontStyle.Bold), Brushes.Blue, new Point(125, 45));

            String SalNum = SalaryDGV.SelectedRows[0].Cells[0].Value.ToString();
            String EmpId = SalaryDGV.SelectedRows[0].Cells[1].Value.ToString();
            String EmpName = SalaryDGV.SelectedRows[0].Cells[2].Value.ToString();
            String BasSal = SalaryDGV.SelectedRows[0].Cells[3].Value.ToString();
            String Bonus = SalaryDGV.SelectedRows[0].Cells[4].Value.ToString();
            String Advance = SalaryDGV.SelectedRows[0].Cells[5].Value.ToString();
            String Tax = SalaryDGV.SelectedRows[0].Cells[6].Value.ToString();
            String Balance = SalaryDGV.SelectedRows[0].Cells[7].Value.ToString();
            String Period = SalaryDGV.SelectedRows[0].Cells[8].Value.ToString();

            e.Graphics.DrawString("Salary Number: " + SalNum, new Font("Bellota", 10, FontStyle.Bold), Brushes.Blue, new Point(50, 100));
            e.Graphics.DrawString("Employee Id: " + EmpId, new Font("Bellota", 10, FontStyle.Bold), Brushes.Blue, new Point(50, 150));
            e.Graphics.DrawString("Employee Name: " + EmpName, new Font("Bellota", 10, FontStyle.Bold), Brushes.Blue, new Point(250, 150));
            e.Graphics.DrawString("Base Salary: " + BasSal, new Font("Bellota", 10, FontStyle.Bold), Brushes.Blue, new Point(50, 180));
            e.Graphics.DrawString("Bonus: Rs " + Bonus, new Font("Bellota", 8, FontStyle.Bold), Brushes.Blue, new Point(50, 210));
            e.Graphics.DrawString("Advance on Salary: Rs " + Advance, new Font("Bellota", 8, FontStyle.Bold), Brushes.Blue, new Point(50, 240));
            e.Graphics.DrawString("Tax: Rs " + Tax, new Font("Bellota", 8, FontStyle.Bold), Brushes.Blue, new Point(50, 270));
            e.Graphics.DrawString("Total: Rs " + Balance, new Font("Bellota", 8, FontStyle.Bold), Brushes.Blue, new Point(50, 300));
            e.Graphics.DrawString("Period: " + Period, new Font("Bellota", 8, FontStyle.Bold), Brushes.Blue, new Point(50, 330));

            e.Graphics.DrawString("Power by Team Decoderz" + Period, new Font("Bellota", 12, FontStyle.Bold), Brushes.Crimson, new Point(150, 240));
            e.Graphics.DrawString("*********Version 1.0*********" + Period, new Font("Bellota", 12, FontStyle.Bold), Brushes.Crimson, new Point(100, 435));
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

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Homes obj = new Homes();
            obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Bonus obj = new Bonus();
            obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
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
             this.Focus();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
            obj.Show();
            this.Hide();
        }
        int DailyBase = 0, Total = 0, Pres = 0, Abs = 0, Exc = 0;
        double GrdTot = 0, TotTax = 0;
        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            if (BaseSalaryTb.Text == "" || BonusTb.Text == "" || AdvanceTb.Text == "")
            {
                MessageBox.Show("Select The Employee");
            }
            else
            {
                Pres = Convert.ToInt32(PressTb.Text);
                Abs = Convert.ToInt32(AbsTb.Text);
                Exc = Convert.ToInt32(ExcusedTb.Text);
                DailyBase = Convert.ToInt32(BaseSalaryTb.Text) / 28;
                Total = ((DailyBase) * Pres) + ((DailyBase / 2) * Exc);
                double Tax = Total * 0.16;
                TotTax = Total - Tax;
                GrdTot = TotTax + Convert.ToInt32(BonusTb.Text);
                BalanceTb.Text = "Rs" + GrdTot;
            }
        }
    }
}
