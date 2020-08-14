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
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace HotelManagement
{
    public partial class book_at_hotel : Form
    {
        string st = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Data\hotelmanagement.mdf;Integrated Security=True;Connect Timeout=30";
        SqlConnection conn = null;
        SqlDataAdapter sda;
        DataSet ds;
        SqlCommand cmd;
        SqlDataReader sdr;
        DataTable dt;
        DataRow dr;
        public book_at_hotel()
        {
            InitializeComponent();
            HotelsBindComboBox();
            RoomsTypeBindComboBox();
            conn = new SqlConnection(st);
        }
        public void datanull()
        {
            textBox5.Text = "";
            textBox4.Text = "";
            textBox7.Text = "";
            comboBox3.Text = "";
            textBox2.Text = "";
            dateTimePicker2.CustomFormat = null;
            dateTimePicker1.CustomFormat = null;
            textBox1.Text = "";
            comboBox2.Text = "";
            comboBox1.Text = "";
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
                }
                conn.Close();
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
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
        private string Random()
        {
            System.Random rand = new System.Random((int)System.DateTime.Now.Ticks);
            int random = rand.Next(1, 100000);
            string s = random.ToString();
            return s;
        }
        public void emailvalid()
        {
            Regex mRegxExpression;

            if (textBox5.Text.Trim() != string.Empty)

            {

                mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

                if (!mRegxExpression.IsMatch(textBox5.Text.Trim()))

                {

                    MessageBox.Show("E-mail address format is not correct.", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    textBox5.Focus();

                }

            }
        }
        public void ss()
        {
            try
            {
                conn.Open();
                string hotelname = comboBox1.SelectedItem.ToString();
                hotelname = hotelname.Replace(" ", "_");
                string roomtype = comboBox2.SelectedItem.ToString();
                DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
                DateTime dt2 = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
                sda = new SqlDataAdapter("select check_in,check_out from " + hotelname + "_status where roomtype='" + roomtype + "'", conn);
                dt = new DataTable();
                sda.Fill(dt);
                int no = dt.Rows.Count;
                conn.Close();

                conn.Open();
                sda = new SqlDataAdapter("select * from " + hotelname + " where roomtype='" + roomtype + "'", conn);
                dt = new DataTable();
                sda.Fill(dt);
                int totalroom = dt.Rows.Count;
                conn.Close();

                conn.Open();
                DateTime chin, chout;
                String checki, checko;
                int room;
                sda = new SqlDataAdapter("select * from " + hotelname + "_status where roomtype='" + roomtype + "'", conn);
                ds = new DataSet();
                sda.Fill(ds);
                for (int i = 0; i < no; i++)
                {
                    checki = ds.Tables[0].Rows[i]["check_in"].ToString();
                    checko = ds.Tables[0].Rows[i]["check_out"].ToString();
                    room = Convert.ToInt32(ds.Tables[0].Rows[i]["rooms"]);
                    chin = Convert.ToDateTime(checki);
                    chout = Convert.ToDateTime(checko);
                    if (dt1 >= chin && dt1 < chout)
                    {
                        totalroom = totalroom - room;
                        textBox2.Text = totalroom.ToString();
                    }
                    else if (dt2 > chin && dt2 <= chout)
                    {
                        totalroom = totalroom - room;
                        textBox2.Text = totalroom.ToString();
                    }
                }
                textBox2.Text = totalroom.ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void book_at_hotel_Load(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == string.Empty)
            {
                MessageBox.Show("Please Select Hotel First");
                return;
            }
            try
            {
                conn.Open();
                string hotelname = comboBox1.SelectedItem.ToString();
                hotelname = hotelname.Replace(" ", "_");                
                string roomtype = comboBox2.SelectedItem.ToString();
                cmd = new SqlCommand("select price from " + hotelname + " where roomtype='" + roomtype + "'", conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    textBox1.Text = sdr["price"].ToString();

                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please select Proper Hotel Name & Room Type");
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == string.Empty)
            {
                MessageBox.Show("Please Select Hotel First 2");
                return;
            }
            else if (comboBox2.Text == string.Empty)
            {
                MessageBox.Show("Please Select Roomtype First");
                return;
            }
            ss();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == string.Empty)
            {
                MessageBox.Show("Please Select Hotel First 1");
                return;
            }
            else if (comboBox2.Text == string.Empty)
            {
                MessageBox.Show("Please Select Roomtype First");
                return;
            }
            dateTimePicker1.MinDate = DateTime.Now;
            dateTimePicker2.MinDate = dateTimePicker1.Value.AddDays(1);
        }
        public static void SendEmail(string emailid, string booking_id, string clientid, string client, string hotelname, string roomtype, string checkin, string checkout, string rooms, int total)
        {
            MailMessage mailMessage = new MailMessage("continentalhotelsahil@gmail.com", emailid);
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = "Welcome To Our Hotel Continental.<br/>" +
                "Dear " + client + ".<br/>" +
                "Your Client Id is " + clientid + ".<br/>" +
                "<table border=" + 1 + " cellpadding=" + 0 + " cellspacing=" + 0 + ">" +
                "<tr bgcolor='#1a53ff'>" +
               "<th text-align='center' colspan='7'><h1>Booking Information</h1></th>" +
               "</tr>" +
              "<tr bgcolor='#668cff'>" +
               "<th><h2>Hotel Name</h2></th>" +
               "<th><h2>Room Type</h2></td>" +
               "<th><h2>Booking ID</h2></td>" +
               "<th><h2>Check In Date</h2></th>" +
               "<th><h2>Check Out Date</h2></th>" +
               "<th><h2>Total amount</h2></th>" +
               "</tr>" +

               "<tr>" +
               "<th><h3>" + hotelname + "</h3></th>" +
               "<th><h3>" + roomtype + "</h3></th>" +
               "<th><h3>" + booking_id + "</h3></th>" +
               "<th><h3>" + checkin + "</h3></th>" +
               "<th><h3>" + checkout + "</h3></th>" +
               "<th><h3>" + total + "</h3></th>" +
               "</tr>" +

             "</table>";
                                /*"Dear " + client + "." + Environment.NewLine +
                                "Your Client Id is " + clientid + "." + Environment.NewLine +
                                "Your Rooms are Booked in " + hotelname + "." + Environment.NewLine +
                                "You Select " + roomtype + " room." + Environment.NewLine +
                                "Total Rooms:  " + rooms + Environment.NewLine +
                                "Your Booking id is " + booking_id + "." + Environment.NewLine +
                                "Your Checkin Date is " + checkin + " and Checkout Date is " + checkout + "." + Environment.NewLine +
                                "You paid " + total + " rs for room booking. " + Environment.NewLine +
                                "Thank You for Booking.";*/
            mailMessage.Subject = "Hotel Continental Booking";
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "continentalhotelsahil@gmail.com",
                Password = "%TGB6yhn^YHN5tgb"
            };
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String hotelname = comboBox1.SelectedItem.ToString();
                hotelname = hotelname.Replace(" ", "_");
                String roomtype = comboBox2.SelectedItem.ToString();
                int price = Convert.ToInt32(textBox1.Text);
                String checkin = dateTimePicker1.Value.ToShortDateString();
                String checkout = dateTimePicker2.Value.ToShortDateString();
                String rooms = comboBox3.SelectedItem.ToString();
                string clientid = textBox7.Text;
                String client = textBox4.Text;
                String booking_id = "con" + client.Substring(0, 2) + Random();
                string emailid = textBox5.Text;
                int room = Convert.ToInt32(rooms);
                int available = Convert.ToInt32(textBox2.Text);
                int total = room * price;
                
                conn.Open();
                sda = new SqlDataAdapter("select * from " + hotelname + " where roomtype='" + roomtype + "'", conn);
                dt = new DataTable();
                sda.Fill(dt);
                int no = dt.Rows.Count;
                conn.Close();

                conn.Open();
                sda = new SqlDataAdapter("select * from " + hotelname + "_status", conn);
                dt = new DataTable();
                sda.Fill(dt);
                dr = dt.NewRow();
                dt.Dispose();
                conn.Close();
                if (dt.Rows.Count == 0)
                {
                    conn.Open();
                    cmd = new SqlCommand("insert into " + hotelname + "_status values('" + roomtype + "','" + price + "','" + rooms + "','" + checkin + "','" + checkout + "','" + clientid + "','" + booking_id + "');", conn);
                    sdr = cmd.ExecuteReader();
                    conn.Close();
                    conn.Open();
                    cmd = new SqlCommand("insert into booking values('" + booking_id + "','" + clientid + "','" + client + "','" + emailid + "','" + hotelname + "','" + roomtype + "','" + checkin + "','" + checkout + "','" + rooms + "','" + total + "');", conn);
                    sdr = cmd.ExecuteReader();
                    conn.Close();
                    conn.Open();
                    cmd = new SqlCommand("insert into bookingsecret values('" + booking_id + "','" + clientid + "','" + client + "','" + emailid + "','" + hotelname + "','" + roomtype + "','" + checkin + "','" + checkout + "','" + rooms + "','" + total + "');", conn);
                    sdr = cmd.ExecuteReader();
                    conn.Close();
                    SendEmail(emailid, booking_id, clientid, client, hotelname, roomtype, checkin, checkout, rooms, total);
                    MessageBox.Show("Your Rooms is Booked & Your Booking ID is '" + booking_id + "'");
                    datanull();
                }
                else if (room <= available)
                {
                    conn.Open();
                    cmd = new SqlCommand("insert into " + hotelname + "_status values('" + roomtype + "','" + price + "','" + rooms + "','" + checkin + "','" + checkout + "','" + clientid + "','" + booking_id + "');", conn);
                    sdr = cmd.ExecuteReader();
                    conn.Close();
                    conn.Open();
                    cmd = new SqlCommand("insert into booking values('" + booking_id + "','" + clientid + "','" + client + "','" + emailid + "','" + hotelname + "','" + roomtype + "','" + checkin + "','" + checkout + "','" + rooms + "','" + total + "');", conn);
                    sdr = cmd.ExecuteReader();
                    conn.Close();
                    conn.Open();
                    cmd = new SqlCommand("insert into bookingsecret values('" + booking_id + "','" + clientid + "','" + client + "','" + emailid + "','" + hotelname + "','" + roomtype + "','" + checkin + "','" + checkout + "','" + rooms + "','" + total + "');", conn);
                    sdr = cmd.ExecuteReader();
                    conn.Close();
                    SendEmail(emailid, booking_id, clientid, client, hotelname, roomtype, checkin, checkout, rooms, total);
                    MessageBox.Show("Your Rooms is Booked & Your Booking ID is '" + booking_id + "'");
                    datanull();
                }
                else
                {
                    MessageBox.Show("Rooms not available");
                    datanull();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == string.Empty)
            {
                MessageBox.Show("Please Select Hotel First");
                return;
            }
            else if (comboBox2.Text == string.Empty)
            {
                MessageBox.Show("Please Select Roomtype First");
                return;
            }
            else if(dateTimePicker1.Text== string.Empty)
            {
                MessageBox.Show("Please Select Check In Date First");
                return;
            }
            try
            {
                int rooms = Convert.ToInt32(comboBox3.SelectedItem.ToString());
                int available = Convert.ToInt32(textBox2.Text);
                if (rooms > available)
                {
                    MessageBox.Show("Rooms not Available");
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Error");
            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            datanull();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == string.Empty)
            {
                MessageBox.Show("Please Select Hotel First");
                return;
            }
            else if (comboBox2.Text == string.Empty)
            {
                MessageBox.Show("Please Select Roomtype First");
                return;
            }
            else if (dateTimePicker1.Text == string.Empty)
            {
                MessageBox.Show("Please Select Check In Date First");
                return;
            }
            else if(comboBox3.Text == string.Empty)
            {
                MessageBox.Show("Please Select Rooms");
                return;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == string.Empty)
            {
                MessageBox.Show("Please Select Hotel First");
                return;
            }
            else if (comboBox2.Text == string.Empty)
            {
                MessageBox.Show("Please Select Roomtype First");
                return;
            }
            else if (dateTimePicker1.Text == string.Empty)
            {
                MessageBox.Show("Please Select Check In Date First");
                return;
            }
            else if (comboBox3.Text == string.Empty)
            {
                MessageBox.Show("Please Select Rooms");
                return;
            }
            else if (textBox7.Text == string.Empty)
            {
                MessageBox.Show("Please Fill Client ID");
                return;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            emailvalid();
        }
    }
}
