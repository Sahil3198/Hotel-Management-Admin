namespace HotelManagement
{
    partial class booking
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(booking));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.bookAtHotelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bookingHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bookAtHotelToolStripMenuItem,
            this.bookingHistoryToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // bookAtHotelToolStripMenuItem
            // 
            this.bookAtHotelToolStripMenuItem.Name = "bookAtHotelToolStripMenuItem";
            this.bookAtHotelToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.bookAtHotelToolStripMenuItem.Text = "Book at Hotel";
            this.bookAtHotelToolStripMenuItem.Click += new System.EventHandler(this.bookAtHotelToolStripMenuItem_Click);
            // 
            // bookingHistoryToolStripMenuItem
            // 
            this.bookingHistoryToolStripMenuItem.Name = "bookingHistoryToolStripMenuItem";
            this.bookingHistoryToolStripMenuItem.Size = new System.Drawing.Size(104, 20);
            this.bookingHistoryToolStripMenuItem.Text = "Booking History";
            this.bookingHistoryToolStripMenuItem.Click += new System.EventHandler(this.bookingHistoryToolStripMenuItem_Click);
            // 
            // booking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "booking";
            this.Text = "Booking ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem bookAtHotelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bookingHistoryToolStripMenuItem;
    }
}