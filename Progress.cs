using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RT2NMEA
{
    public partial class Progress : UserControl
    {
        public Progress()
        {
            InitializeComponent();
            SetStyle(
              ControlStyles.AllPaintingInWmPaint |
              ControlStyles.UserPaint |
              ControlStyles.OptimizedDoubleBuffer,true);
            t = new System.Timers.Timer();
            t.Interval = 100;
            step=this.Width / 10;
            t.Elapsed += T_Elapsed;
            this.FormerLabel.AutoSize = false;
            this.FormerLabel.Dock = DockStyle.None;
            this.FormerLabel.Width = this.Width /6;
        }
      private int step;
       public void SetSpeed(int Interval) 
       {
	t.Interval = Interval;
       }
 
       public void SetStep(int Step)
        {
            this.step = Step;
        }
        private void T_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if(this.FormerLabel.Left < this.Width)
            { 
                this.FormerLabel.Left += this.step;
            }
            else
            {
                this.FormerLabel.Left = 0;
            }
        }
        /// <summary>
        /// 开始等待
        /// </summary>
        public void wait()
        {
            t.Start();
        }
        /// <summary>
        /// 结束等待
        /// </summary>
        public void stop()
        {
            t.Stop();
        }
        private System.Timers.Timer t;


    }
}
