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
    public partial class hotels : Form
    {
        string st = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Data\hotelmanagement.mdf;Integrated Security=True;Connect Timeout=30";
        SqlConnection conn = null;
        SqlDataAdapter sda;
        DataSet ds;
        SqlCommand cmd;
        SqlDataReader sdr;
        
        public hotels()
        {
            InitializeComponent();
            datagridview();
        }
        public void datagridview()
        {
            conn = new SqlConnection(st);
            conn.Open();
            ds = new DataSet();
            sda = new SqlDataAdapter("Select * from hotels", conn);
            sda.Fill(ds);
            this.dataGridView1.DataSource = ds.Tables[0];
        }
        public void datanull()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }
        private void hotels_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(st);
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tablename=textBox2.Text;
            sda = new SqlDataAdapter("insert into hotels values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "');", conn);
            ds = new DataSet();
            sda.Fill(ds);
            MessageBox.Show("Add Hotel Successfully");
            datanull();
            datagridview();
            tablename = tablename.Replace(" ", "_");
            cmd = new SqlCommand("create table "+ tablename+" (roomtype nvarchar(45), price int, room_no int)", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Open();

            cmd = new SqlCommand("create table " + tablename + "_status(roomtype nvarchar(45), price int, rooms int,check_in nvarchar(50),check_out nvarchar(50),client_id nvarchar(45),booking_id nvarchar(45))", conn);
           
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select Hotel_name from Hotels where Hotel_id='" + textBox1.Text + "'", conn);
            sdr = cmd.ExecuteReader();
            String s;
            while (sdr.Read())
            {
                s = sdr["Hotel_name"].ToString();
                s = s.Replace(" ", "_");
                textBox2.Text = s;
            }
            s = textBox2.Text;
            textBox2.Text = "";
            conn.Close();
            conn.Open();
            cmd = new SqlCommand("delete from Hotels where Hotel_id ='" + textBox1.Text + "'", conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Delete Hotel Successfully");
            conn.Close();
            conn.Open();
            SqlCommand cmd1 = new SqlCommand("Drop table "+s+"",conn);
            cmd1.ExecuteNonQuery();
            conn.Close();
            conn.Open();
            SqlCommand cmd3 = new SqlCommand("delete from booking where hotelname='" + s + "'", conn);
            cmd3.ExecuteNonQuery();
            conn.Close();
            conn.Open();
            SqlCommand cmd2= new SqlCommand("Drop table " + s + "_status", conn);
            cmd2.ExecuteNonQuery();
            conn.Close();
            datanull();
            datagridview();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("select * from Hotels where Hotel_id = '" + textBox1.Text + "'", conn);
            try
            {
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    textBox1.Text = sdr[0].ToString();
                    textBox2.Text = sdr[1].ToString();
                    textBox3.Text = sdr[2].ToString();
                    textBox4.Text = sdr[3].ToString();
                }
                else
                {
                    MessageBox.Show(" No Record");
                }
                sdr.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Please Enter Employee Code");
            }
        }
    }
}
