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
    public partial class Advances : Form
    {
        public Advances()
        {
            InitializeComponent();
            showAdvance();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Pathum Wijesekara\source\repos\PayRollManagementSystem\PayRollManagementSystem\PayRollDB.mdf ;Integrated Security=True;Connect Timeout=30");
        private void Clear()
        {
            ANameTb.Text = "";
            AAmountTb.Text = "";
            Key = 0;
        }
        private void showAdvance()
        {
            Con.Open();
            string Query = "Select * from AdvanceTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AdvanceDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ANameTb.Text == "" || AAmountTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into AdvanceTbl(AName,AAmt) values (@AN,@AA)", Con);
                    cmd.Parameters.AddWithValue("@AN", ANameTb.Text);
                    cmd.Parameters.AddWithValue("@AA", AAmountTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Advance Saved");
                    Con.Close();
                    showAdvance();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void AdvanceDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ANameTb.Text = AdvanceDGV.SelectedRows[0].Cells[1].Value.ToString();
            AAmountTb.Text = AdvanceDGV.SelectedRows[0].Cells[2].Value.ToString();
            if (ANameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(AdvanceDGV.SelectedRows[0].Cells[1].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ANameTb.Text == "" || AAmountTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update AdvanceTbl Set AName=@AN ,AAmt=@AA Where AID=@AKey", Con);
                    cmd.Parameters.AddWithValue("@AN", ANameTb.Text);
                    cmd.Parameters.AddWithValue("@AA", AAmountTb.Text);
                    cmd.Parameters.AddWithValue("@AKey", Key);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Advance Updated");
                    Con.Close();
                    showAdvance();
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
                MessageBox.Show("Select The Advance");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from AdvanceTbl Where AID=@AKey", Con);
                    cmd.Parameters.AddWithValue("@AKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Advance Deleted");
                    Con.Close();
                    showAdvance();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Homes obj = new Homes();
            obj.Show();
            this.Hide();
        }
    }
}
