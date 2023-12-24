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

namespace GuestManagementSystem
{
    public partial class User : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ali\Documents\GuestHouse.mdf;Integrated Security=True;Connect Timeout=30");

        public User()
        {
            InitializeComponent();
            ShowUser();
        }

        private void ShowUser()
        { 
            con.Open();
            string query = "select * from UserTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (UNameTxt.Text =="" ||  UPhoneTxt.Text =="" || UPassTxt.Text == "")
            {
                MessageBox.Show("Missing Information.");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into UserTable(UName, Uphone, UPassword) values(@UN, @UP, @UPA)", con);

                    cmd.Parameters.AddWithValue("@UN", UNameTxt.Text);
                    cmd.Parameters.AddWithValue("@UP", UPhoneTxt.Text);
                    cmd.Parameters.AddWithValue("@UPA", UPassTxt.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Saved.");
                    con.Close();
                    ShowUser();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        int key = 0;
        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UNameTxt.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            UPhoneTxt.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
            UPassTxt.Text = UserDGV.SelectedRows[0].Cells[3].Value.ToString();
            if(UNameTxt.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(UserDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (UNameTxt.Text == "" || UPhoneTxt.Text == "" || UPassTxt.Text == "")
            {
                MessageBox.Show("Missing Information.");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update UserTable Set UName=@UN, Uphone=@UP, UPassword=@UPA where UId=@UKey", con);

                    cmd.Parameters.AddWithValue("@UN", UNameTxt.Text);
                    cmd.Parameters.AddWithValue("@UP", UPhoneTxt.Text);
                    cmd.Parameters.AddWithValue("@UPA", UPassTxt.Text);
                    cmd.Parameters.AddWithValue("@UKey", key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User informations updated.");
                    con.Close();
                    ShowUser();
                    Reset();    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Reset()
        {
            UNameTxt.Text= string.Empty;
            UPhoneTxt.Text= string.Empty;
            UPassTxt.Text= string.Empty;
            key = 0;
        }
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select user.");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from UserTable where UId=@UKey", con);
                    cmd.Parameters.AddWithValue("@UKey", key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted.");
                    con.Close();
                    ShowUser();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
