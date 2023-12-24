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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            CountBooked();
            CountCustomer();
            CountBooking();
            GetCustomer();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ali\Documents\GuestHouse.mdf;Integrated Security=True;Connect Timeout=30");

        int free, booked;
        int Bper, Fper;
        private void CountBooked()
        {
            string status = "Booked";
            
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from RoomTable where RStatus='"+status+"'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            free = 20 - Convert.ToInt32(dt.Rows[0][0].ToString());
            booked = Convert.ToInt32(dt.Rows[0][0].ToString());
            Bper = (booked / 20)*100;
            Fper = (free / 20)*100;
            BookedLbl.Text = dt.Rows[0][0].ToString() + " Booked Rooms";
            AvailableLbl.Text = free + " Free Rooms";
            freeRoomsLbl.Text = free+"";
            BookedProgbar.Value = Bper;
            AvailProgbar.Value = Fper;
            FreeRoomProgress.Value = Fper;
            con.Close();
        }

        private void CountCustomer()
        {
            string status = "Booked";

            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from CustomerTable", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            
            CusNumLbl.Text = dt.Rows[0][0].ToString() + " Customers";
            
            con.Close();
        }

        private void GetCustomer()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select CusId from CustomerTable", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CusId", typeof(int));
            dt.Load(rdr);
            CusIdCmbx.ValueMember = "CusId";
            CusIdCmbx.DataSource = dt;
            con.Close();
        }

        
        private void GetCusName()
        {
            con.Open();
            string query = "select * from CustomerTable where CusId=" + CusIdCmbx.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                CNameTxt.Text = dr["CusName"].ToString();
            }
            con.Close();
        }

        string Rtype;
        int RCost;
        private void GetRoomType()
        {
            con.Open();
            string query = "select * from RoomTable where RId=" + RoomNum + "";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
               Rtype = dr["RType"].ToString();
                RCost = Convert.ToInt32(dr["RCost"].ToString());
            }
            con.Close();
        }
        int RoomNum = 0;

        private void CusIdCmbx_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCusName();
        }

        private void R1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void R2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void R3_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void R4_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void R5_Paint(object sender, PaintEventArgs e)
        {
         
        }

        private void R6_Paint(object sender, PaintEventArgs e)
        {
            
        }
        
        private void R7_Paint(object sender, PaintEventArgs e)
        {
           
        }
        
        private void R8_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void R9_Paint(object sender, PaintEventArgs e)
        {
           
        }
        
        private void R10_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void R11_Paint(object sender, PaintEventArgs e)
        {
            
        }
        
        private void R12_Paint(object sender, PaintEventArgs e)
        {
            
        } 
        
        private void R13_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void R14_Paint(object sender, PaintEventArgs e)
        {
            
        }
       
        private void R15_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void R16_Paint(object sender, PaintEventArgs e)
        {
            
        } 
        
        private void R17_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void R18_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void R19_Paint(object sender, PaintEventArgs e)
        {
          
        }
       
        private void R20_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Reset()
        {
            Rtype = "";
            RCost = 0;
            RoomNum = 0;

        }

        private void UpdateRoom()
        {
            string status = "Booked";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update RoomTable set RStatus=@RS where RId = @RKey", con);

                cmd.Parameters.AddWithValue("@RS", status);
                cmd.Parameters.AddWithValue("@RKey", RoomNum);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Room Updated.");
                con.Close();
                Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void R1_Click(object sender, EventArgs e)
        {
            RoomNum = 1;
        }

        private void R2_Click(object sender, EventArgs e)
        {
            RoomNum = 2;
        }

        private void R3_Click(object sender, EventArgs e)
        {
            RoomNum = 3;
        }

        private void R4_Click(object sender, EventArgs e)
        {
            RoomNum = 4;
        } 
        
        private void R5_Click(object sender, EventArgs e)
        {
            RoomNum = 5;
        }

        private void R6_Click(object sender, EventArgs e)
        {
            RoomNum = 6;
        }

        private void R7_Click(object sender, EventArgs e)
        {
            RoomNum = 7;
        }

        private void R8_Click(object sender, EventArgs e)
        {
            RoomNum = 8;
        }

        private void R9_Click(object sender, EventArgs e)
        {
            RoomNum = 9;
        }

        private void R10_Click(object sender, EventArgs e)
        {
            RoomNum = 10;
        }

        private void R11_Click(object sender, EventArgs e)
        {
            RoomNum = 11;
        }

        private void R12_Click(object sender, EventArgs e)
        {
            RoomNum = 12;
        }

        private void R13_Click(object sender, EventArgs e)
        {
            RoomNum = 13;
        }

        private void R14_Click(object sender, EventArgs e)
        {
            RoomNum = 14;
        }

        private void R15_Click(object sender, EventArgs e)
        {
            RoomNum = 15;
        }

        private void R16_Click(object sender, EventArgs e)
        {
            RoomNum = 16;
        }

        private void R17_Click(object sender, EventArgs e)
        {
            RoomNum = 17;
        }

        private void R18_Click(object sender, EventArgs e)
        {
            RoomNum = 18;
        }

        private void R19_Click(object sender, EventArgs e)
        {
            RoomNum = 19;
        }

        private void R20_Click(object sender, EventArgs e)
        {
            RoomNum = 20;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Booking booking = new Booking();
            booking.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide(); 
        }

        private void BookBtn_Click(object sender, EventArgs e)
        {
            if(CNameTxt.Text == ""|| RoomNum == 0)
            {
                MessageBox.Show("Select room and customer.");
            }
            else
            {
                try
                {
                    GetRoomType();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BookingTable(CusId, CusName, RId, RNum, RType, BCost) values(@CI, @CN, @RI, @RN, @RT, @RC)", con);

                    cmd.Parameters.AddWithValue("@CI", CusIdCmbx.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CN", CNameTxt.Text);
                    cmd.Parameters.AddWithValue("@RI", RoomNum);
                    cmd.Parameters.AddWithValue("@RN", RoomNum);
                    cmd.Parameters.AddWithValue("@RT", Rtype);
                    cmd.Parameters.AddWithValue("@RC", RCost);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Booked.");
                    Reset();
                    con.Close();
                    UpdateRoom();
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void CountBooking()
        {
            string status = "Booked";

            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from BookingTable", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            BookingNumLbl.Text = dt.Rows[0][0].ToString() + " Booking";

            con.Close();
        }
    }
}
