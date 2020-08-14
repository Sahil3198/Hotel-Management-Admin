using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement
{
    public partial class booking : Form
    {
        public booking()
        {
            InitializeComponent();
        }

        private void bookAtHotelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            book_at_hotel b1 = new book_at_hotel();
            b1.MdiParent = this;
            b1.Dock = DockStyle.Fill;
            b1.Show();
        }

        private void bookingHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            booking_history b2 = new booking_history();
            b2.MdiParent = this;
            b2.Dock = DockStyle.Fill;
            b2.Show();
        }
    }
}
