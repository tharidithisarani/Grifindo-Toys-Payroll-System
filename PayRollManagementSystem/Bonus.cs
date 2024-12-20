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
    public partial class Bonus : Form
    {
        public Bonus()
        {
            InitializeComponent();
            showBonus();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Pathum Wijesekara\source\repos\PayRollManagementSystem\PayRollManagementSystem\PayRollDB.mdf ;Integrated Security=True;Connect Timeout=30");
        private void Clear()
        {
            BNameTb.Text = "";
            BAmountTb.Text = "";
            Key = 0;

        }
        private void showBonus()
        {
            Con.Open();
            string Query = "Select * from BonusTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BonusDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (BNameTb.Text == "" || BAmountTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BonusTbl(BName,BAmt) values (@BN,@BA)", Con);
                    cmd.Parameters.AddWithValue("@BN", BNameTb.Text);
                    cmd.Parameters.AddWithValue("@BA", BAmountTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bonus Saved");
                    Con.Close();
                    showBonus();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key=0;
        private void BonusDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BNameTb.Text = BonusDGV.SelectedRows[0].Cells[1].Value.ToString();
            BAmountTb.Text = BonusDGV.SelectedRows[0].Cells[2].Value.ToString();
            if (BNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(BonusDGV.SelectedRows[0].Cells[1].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (BNameTb.Text == "" || BAmountTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update BonusTbl Set BName=@BN ,BAmt=@BA Where BID=@BKey", Con);
                    cmd.Parameters.AddWithValue("@BN", BNameTb.Text);
                    cmd.Parameters.AddWithValue("@BA", BAmountTb.Text);
                    cmd.Parameters.AddWithValue("@BKey", Key);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bonus Updated");
                    Con.Close();
                    showBonus();
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

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select The Bonus");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from BonusTbl Where BID=@BKey", Con);
                    cmd.Parameters.AddWithValue("@BKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bonus Deleted");
                    Con.Close();
                    showBonus();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Homes obj = new Homes();
            obj.Show();
            this.Hide();
        }
    }
}
