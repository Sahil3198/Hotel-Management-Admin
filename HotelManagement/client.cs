using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement
{
    public partial class client : Form
    {
        string st = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Data\hotelmanagement.mdf;Integrated Security=True;Connect Timeout=30";
        SqlConnection conn = null;
        SqlDataAdapter sda;
        DataSet ds;
        SqlCommand cmd;
        SqlDataReader sdr;
        public client()
        {
            InitializeComponent();
        }
        public void datagridview()
        {
            conn = new SqlConnection(st);
            ds = new DataSet();
            sda = new SqlDataAdapter("Select client_id,First_name,Last_name,email_id,mobile from client", conn);
            sda.Fill(ds);
            this.dataGridView1.DataSource = ds.Tables[0];
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox5.Text = row.Cells[4].Value.ToString();
            }
        }

        private void client_Load(object sender, EventArgs e)
        {
            datagridview();
        }
    }
}
