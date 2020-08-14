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

namespace HotelManagement
{
    public partial class feedback : Form
    {
        string st = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Data\hotelmanagement.mdf;Integrated Security=True;Connect Timeout=30";
        SqlConnection conn = null;
        SqlDataAdapter sda;
        DataSet ds;
        SqlCommand cmd;
        SqlDataReader sdr;
        public feedback()
        {
            InitializeComponent();
        }
        public void datagridview()
        {
            conn = new SqlConnection(st);
            ds = new DataSet();
            sda = new SqlDataAdapter("Select * from feedback", conn);
            sda.Fill(ds);
            this.dataGridView1.DataSource = ds.Tables[0];
        }

        private void feedback_Load(object sender, EventArgs e)
        {
            datagridview();
        }

        private void cellclickdatagrid(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                richTextBox1.Text= row.Cells[3].Value.ToString();
            }
        }
    }
}
