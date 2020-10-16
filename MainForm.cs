using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RT2NMEA
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btn_RT2NMEA_Click(object sender, EventArgs e)
        {
            RT2NMEA rT2NMEA = new RT2NMEA();
            this.Hide();
            rT2NMEA.ShowDialog();
            this.Show();
        }

        private void btn_GPSWeekCal_Click(object sender, EventArgs e)
        {
            GPSWeekCalForm gps = new GPSWeekCalForm();
            this.Hide();    
            gps.ShowDialog();
            this.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
