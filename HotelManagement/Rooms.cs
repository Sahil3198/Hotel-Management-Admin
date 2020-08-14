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
    public partial class Rooms : Form
    {
        string st = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Data\hotelmanagement.mdf;Integrated Security=True;Connect Timeout=30";
        SqlConnection conn = null;
        SqlDataAdapter sda;
        DataSet ds;
        SqlCommand cmd;
        SqlDataReader sdr;
        public Rooms()
        {
            InitializeComponent();
            HotelsBindComboBox();
            RoomsTypeBindComboBox();
        }
        public void datanull()
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
        }
        public void HotelsBindComboBox()
        {
            try
            {
                conn = new SqlConnection(st);
                cmd = new SqlCommand("select Hotel_name from Hotels", conn);
                conn.Open();
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    comboBox1.Items.Add(sdr["Hotel_name"].ToString());
                    comboBox3.Items.Add(sdr["Hotel_name"].ToString());
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
        public void RoomsTypeBindComboBox()
        {
            try
            {
                conn = new SqlConnection(st);
                cmd = new SqlCommand("select room_type from Type_of_room", conn);
                conn.Open();
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    comboBox2.Items.Add(sdr["room_type"].ToString());
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
        private void Rooms_Load(object sender, EventArgs e)
        {

        }
        public void hotels()
        {
            try
            {
                string hotelname = comboBox1.SelectedItem.ToString();
                hotelname = hotelname.Replace(" ", "_");
                conn = new SqlConnection(st);
                conn.Open();
                ds = new DataSet();
                sda = new SqlDataAdapter("Select * from " + hotelname + "", conn);
                sda.Fill(ds);
                this.dataGridView2.DataSource = ds.Tables[0];
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            hotels();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int start = Convert.ToInt32(textBox2.Text);
                int end = Convert.ToInt32(textBox3.Text);
                textBox4.Text = (end - start + 1).ToString();
            }
            catch(Exception)
            {
                MessageBox.Show("Please Insert Information");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*string tablename = comboBox1.SelectedItem.ToString();
            tablename = tablename.Replace(" ", "_");
            string roomtype= comboBox2.SelectedItem.ToString();
            conn.Close();
            conn.Open();
            cmd = new SqlCommand("insert into "+tablename+" values('"+roomtype+"','"+textBox1.Text+"','"+ textBox2.Text + "','"+ textBox3.Text + "','"+ textBox4.Text + "')", conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Information Added Successfully");
            datanull();
            conn.Close();*/
            string tablename = comboBox1.SelectedItem.ToString();
            tablename = tablename.Replace(" ", "_");
            string roomtype = comboBox2.SelectedItem.ToString();
            string price = textBox1.Text;
            conn.Close();
            
            int start = Convert.ToInt32(textBox2.Text);
            int total = Convert.ToInt32(textBox4.Text);
            
            for (int i= 0;i< total;i++)
            {
                conn.Open();
                cmd = new SqlCommand("insert into "+tablename+" values('" + roomtype + "','" + price + "','" + start + "')", conn);
                cmd.ExecuteNonQuery();
                start = start + 1;
                conn.Close();
            }
            MessageBox.Show("Information Added Successfully");
            datanull();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void hotelstatus()
        {
            try
            {
                string hotelname = comboBox3.SelectedItem.ToString();
                hotelname = hotelname.Replace(" ", "_");
                conn = new SqlConnection(st);
                conn.Open();
                ds = new DataSet();
                sda = new SqlDataAdapter("Select * from " + hotelname + "_status", conn);
                sda.Fill(ds);
                this.dataGridView1.DataSource = ds.Tables[0];
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            hotelstatus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            datanull();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            datanull();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox5.Text = row.Cells[0].Value.ToString();
                textBox6.Text = row.Cells[1].Value.ToString();
                textBox8.Text = row.Cells[2].Value.ToString();
                textBox7.Text = row.Cells[3].Value.ToString();
                textBox9.Text = row.Cells[4].Value.ToString();
                textBox10.Text = row.Cells[5].Value.ToString();
                textBox11.Text = row.Cells[6].Value.ToString();
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
