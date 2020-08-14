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
    public partial class booking_history : Form
    {
        string st = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Data\hotelmanagement.mdf;Integrated Security=True;Connect Timeout=30";
        SqlConnection conn = null;
        SqlDataAdapter sda;
        DataSet ds;
        SqlCommand cmd;
        SqlDataReader sdr;
        public booking_history()
        {
            InitializeComponent();
        }
        public void datagridview()
        {
            conn = new SqlConnection(st);
            conn.Open();
            ds = new DataSet();
            sda = new SqlDataAdapter("Select * from booking", conn);
            sda.Fill(ds);
            this.dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void booking_history_Load(object sender, EventArgs e)
        {
            datagridview();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox5.Text = row.Cells[4].Value.ToString();
                textBox6.Text = row.Cells[5].Value.ToString();
                textBox7.Text = row.Cells[6].Value.ToString();
                textBox8.Text = row.Cells[7].Value.ToString();
                textBox9.Text = row.Cells[8].Value.ToString();
                textBox10.Text = row.Cells[9].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new SqlCommand("select * from booking where booking_Id = '" + textBox11.Text + "'", conn);
            try
            {
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    textBox1.Text = sdr[0].ToString();
                    textBox2.Text = sdr[1].ToString();
                    textBox3.Text = sdr[2].ToString();
                    textBox4.Text = sdr[3].ToString();
                    textBox5.Text = sdr[4].ToString();
                    textBox6.Text = sdr[5].ToString();
                    textBox7.Text = sdr[6].ToString();
                    textBox8.Text = sdr[7].ToString();
                    textBox9.Text = sdr[8].ToString();
                    textBox10.Text = sdr[9].ToString();
                }
                else
                {
                    MessageBox.Show("No Record");
                }
                sdr.Close();
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Please Enter Booking ID");
            }
            textBox11.Text = "";
        }
    }
}
