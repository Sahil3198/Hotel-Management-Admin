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

namespace HotelManagement
{
    public partial class cancellation : Form
    {
        string st = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Data\hotelmanagement.mdf;Integrated Security=True;Connect Timeout=30";
        SqlConnection conn = null;
        SqlDataAdapter sda;
        DataSet ds;
        SqlCommand cmd;
        SqlDataReader sdr;
        public cancellation()
        {
            InitializeComponent();
        }
        public void datanull()
        {
            textBox1.Text = "";
        }

        private void cancellation_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(st);
                string s = textBox1.Text;
                string hotelname = "";
                conn.Open();
                cmd = new SqlCommand("select hotelname from booking where booking_Id='" + s + "'", conn);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    hotelname = sdr["hotelname"].ToString();
                }
                conn.Close();
                conn.Open();
                cmd = new SqlCommand("delete from booking where booking_Id='" + s + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Open();
                cmd = new SqlCommand("delete from " + hotelname + "_status where booking_id='" + s + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                datanull();
                MessageBox.Show("Cancellation of Booking is Successfully");

            }
            catch(Exception)
            {
                MessageBox.Show("No Data Found");
            }
        }
    }
}
