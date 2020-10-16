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
    public partial class GPSWeekCalForm : Form
    {
        public GPSWeekCalForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.chooseTime.ValueChanged += ChooseTime_ValueChanged;
        }

        private void ChooseTime_ValueChanged(object sender, EventArgs e)
        {
            long time = this.GetDay(this.chooseTime.Value);
            this.GPSDayTxt.Text = time.ToString();
            this.GPSWeekTxt.Text = Math.Floor((double)(time / 7.0)).ToString();
        }

        private void GPSWeekCalForm_Load(object sender, EventArgs e)
        {
            this.chooseTime.Value = DateTime.Now;
        }
        private long leap(long ye)
        {
            long k = 0;
            while (ye > 1980) 
            {
                if (Judge(ye)) k++;
                ye = ye - 1;
            }
            return k;
        }
        private bool Judge(long ye)
        {
            if ((ye % 4 == 0 && ye % 100 != 0) || ye % 400 == 0) return true;
            else return false;
        }
        private long GetDay(DateTime time)
        {
            return this.GetDay(time.Year, time.Month, time.Day);
        }
        private long GetDay(long y, long m, long d)
        {
            long total = 0;
            total = (y - 1980) * 365;
            total = total + d - 6;
            if (m >= 3) {
                total = total + leap(y);
            }
            else if (Judge(y)) {
                total = total + leap(y) - 1;
            }
            else {
                total = total + leap(y);
            }
            m = m - 1;
            while (m >= 1)
            {
                if (m == 1|| m == 3 || m ==5 || m == 7 || m == 8 ||m == 10||m==12)
                {
                    total = total + 31;
                }
                else if(m ==4 || m == 6|| m == 9 || m ==11)
                {
                    total = total + 30;

                }
                else
                {
                    total = total + 28;
                }
                m = m - 1;
            }
            return total;
        }
    }
}
