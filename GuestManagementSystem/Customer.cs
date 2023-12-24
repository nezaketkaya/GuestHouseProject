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

namespace GuestManagementSystem
{
    public partial class Customer : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ali\Documents\GuestHouse.mdf;Integrated Security=True;Connect Timeout=30");

        public Customer()
        {
            InitializeComponent();
            ShowCustomer();
        }

        private void Reset()
        {
            CUNameTxt.Text = string.Empty;
            CPhoneTxt.Text = string.Empty;
            GenderCmbx.SelectedIndex = -1;
            CusDateTimePicker.Value = DateTime.Now;
            key = 0;
        }
        private void ShowCustomer()
        {
            con.Open();
            string query = "select * from CustomerTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        int key = 0;
        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CUNameTxt.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
            CPhoneTxt.Text = CustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
            GenderCmbx.SelectedItem = CustomerDGV.SelectedRows[0].Cells[3].Value.ToString();
            CusDateTimePicker.Value = DateTime.Parse(CustomerDGV.SelectedRows[0].Cells[4].Value.ToString());

            if (CUNameTxt.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CustomerDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CUNameTxt.Text == "" || CPhoneTxt.Text == "" || GenderCmbx.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information.");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTable(CusName, CusPhone, CusGender, CusDOB) values(@CN, @CP, @CG, @CD)", con);

                    cmd.Parameters.AddWithValue("@CN", CUNameTxt.Text);
                    cmd.Parameters.AddWithValue("@CP", CPhoneTxt.Text);
                    cmd.Parameters.AddWithValue("@CG", GenderCmbx.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CD", CusDateTimePicker.Value.Date);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Saved.");
                    con.Close();
                    ShowCustomer();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CUNameTxt.Text == "" || CPhoneTxt.Text == "" || GenderCmbx.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information.");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update CustomerTable set CusName=@CN, CusPhone=@CP, CusGender=@CG, CusDOB=@CD where CusId = @CKey", con);

                    cmd.Parameters.AddWithValue("@CN", CUNameTxt.Text);
                    cmd.Parameters.AddWithValue("@CP", CPhoneTxt.Text);
                    cmd.Parameters.AddWithValue("@CG", GenderCmbx.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CD", CusDateTimePicker.Value.Date);
                    cmd.Parameters.AddWithValue("@CKey", key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated.");
                    con.Close();
                    ShowCustomer();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select customer.");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from CustomerTable where CusId=@CKey", con);
                    cmd.Parameters.AddWithValue("@CKey", key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted.");
                    con.Close();
                    ShowCustomer();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Booking booking = new Booking();
            booking.Show();
            this.Hide();
        }
    }
}
