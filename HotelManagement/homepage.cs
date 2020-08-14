using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
namespace HotelManagement
{
    public partial class homepage : Form
    {
        string st = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Data\hotelmanagement.mdf;Integrated Security=True;Connect Timeout=30";
        SqlConnection conn = null;
        SqlDataAdapter sda;
        DataSet ds;
        SqlCommand cmd;
        SqlDataReader sdr;
        DataTable dt;
        public homepage()
        {
            InitializeComponent();
            InitializeMyControl();
        }
        private void InitializeMyControl()
        { 
            textBox2.Text = "";          
            textBox2.PasswordChar = '*';
            textBox4.PasswordChar = '*';
            textBox5.PasswordChar = '*';
            textBox6.PasswordChar = '*';
            textBox1.MaxLength = 14;
        }
        public void datanull()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
        public void datachecker()
        {
            conn = new SqlConnection(st);
            conn.Open();
            sda = new SqlDataAdapter("select * from Hotels", conn);
            dt = new DataTable();
            sda.Fill(dt);
            int no = dt.Rows.Count;
            conn.Close();

            string current = DateTime.Today.ToString("dd-MM-yyyy");
            DateTime cur = Convert.ToDateTime(current);

            conn.Open();
            cmd = new SqlCommand("select Hotel_name from Hotels", conn);
            sdr = cmd.ExecuteReader();
            string[] hotel = new string[no];
            int i= 0;
            string hotelname = "";
            while (sdr.Read())
            {
                hotelname = sdr["Hotel_name"].ToString();
                hotelname = hotelname.Replace(" ", "_");
                hotel[i] = hotelname;
                i++;
            }
            conn.Close();
            
            for (int j=0;j<no;j++)
            {
                conn.Open();
                SqlDataReader sdr1;
                string[] date1 = new string[10000];
                int k = 0;
                cmd = new SqlCommand("select check_out from " + hotel[j] + "_status", conn);
                sdr1 = cmd.ExecuteReader();
                while (sdr1.Read())
                {
                    date1[k] = sdr1["check_out"].ToString();
                    k++; 
                }
                conn.Close();

                for(int s=0;s<k;s++)
                {
                    DateTime cout = Convert.ToDateTime(date1[s]);
                    if (cout < cur)
                    {
                        conn.Open();
                        cmd = new SqlCommand("delete from " + hotel[j] + "_status where check_out='" + cout.ToShortDateString() + "'", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        cmd = new SqlCommand("delete from booking where Check_out_date='" + cout.ToShortDateString() + "'", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            datachecker();
            SqlConnection conn = null;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            try
            {
                conn = new SqlConnection(st);
            }
            catch (SqlException)
            {
                MessageBox.Show("Error");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            button1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(st);
                conn.Open();
                cmd = new SqlCommand("select * from admin where admin_id= '" + textBox1.Text + "'and password = '" + textBox2.Text + "'", conn);
                sdr = cmd.ExecuteReader();
                int count = 0;
                while (sdr.Read())
                {
                    count = count + 1;
                }
                if (count == 1)
                {
                    login l1 = new login();
                    l1.Show(this);
                    datanull();
                }
                else if(textBox1.Text=="" && textBox2.Text == "")
                {
                    MessageBox.Show("Please Enter Admin_id & Password");
                }
                else
                {
                    MessageBox.Show("Incorrect Username & Password");
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Not Run");
            }
            

            /*
            conn = new SqlConnection(st);
            conn.Open();
            cmd = new SqlCommand("select * from admin where admin_id=@username and password =@password", conn);
            cmd.Parameters.AddWithValue("@username", textBox1.Text);  
            cmd.Parameters.AddWithValue("@password", textBox2.Text);  
            sda = new SqlDataAdapter(cmd);  
            DataTable dt = new DataTable();  
            sda.Fill(dt);  
            int i = cmd.ExecuteNonQuery();  
            if (dt.Rows.Count > 0)  
            {
                login l1 = new login();
                l1.Show(this);
            }  
            else  
            {  
                MessageBox.Show("Please enter Correct Username and Password");  
            } */
            conn.Close(); 
        }
        protected override void OnClick(EventArgs e)
        {
            groupBox1.Visible = false;
            button1.Visible = true;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            groupBox1.Visible = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(st);
            conn.Open();
            cmd = new SqlCommand("select * from admin where admin_id= '" + textBox3.Text + "'and password = '" + textBox4.Text + "'", conn);
            sdr = cmd.ExecuteReader();
            int count = 0;
            while (sdr.Read())
            {
                count = count + 1;
            }
            if (count == 1)
            {
                conn.Close();
                if(string.IsNullOrWhiteSpace(textBox5.Text) || string.IsNullOrWhiteSpace(textBox6.Text) )
                {
                    label9.Text = "*Enter New Password";
                    label9.Visible = true;
                }
                else
                {
                    if (textBox5.Text == textBox6.Text)
                    {
                        conn.Open();
                        
                        cmd = new SqlCommand("UPDATE admin SET password='" + textBox5.Text + "' WHERE admin_id='" + textBox3.Text + "'", conn);
                        sdr= cmd.ExecuteReader();
                        conn.Close();
                        MessageBox.Show("Password Set Successfully");
                        groupBox2.Visible = false;
                        groupBox1.Visible = true;
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                    else
                    {
                        label9.Text = "*Password Doesn't Match";
                        label9.Visible = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("Incorrect Username & Password");
            }
        }
    }
}
