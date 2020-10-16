namespace RT2NMEA
{
    partial class MainForm
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_GPSWeekCal = new System.Windows.Forms.Button();
            this.btn_RT2NMEA = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btn_RT2NMEA, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_GPSWeekCal, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(328, 41);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btn_GPSWeekCal
            // 
            this.btn_GPSWeekCal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_GPSWeekCal.Location = new System.Drawing.Point(3, 3);
            this.btn_GPSWeekCal.Name = "btn_GPSWeekCal";
            this.btn_GPSWeekCal.Size = new System.Drawing.Size(158, 35);
            this.btn_GPSWeekCal.TabIndex = 0;
            this.btn_GPSWeekCal.Text = "GPS周计算";
            this.btn_GPSWeekCal.UseVisualStyleBackColor = true;
            this.btn_GPSWeekCal.Click += new System.EventHandler(this.btn_GPSWeekCal_Click);
            // 
            // btn_RT2NMEA
            // 
            this.btn_RT2NMEA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_RT2NMEA.Location = new System.Drawing.Point(167, 3);
            this.btn_RT2NMEA.Name = "btn_RT2NMEA";
            this.btn_RT2NMEA.Size = new System.Drawing.Size(158, 35);
            this.btn_RT2NMEA.TabIndex = 1;
            this.btn_RT2NMEA.Text = "RT27提取NMEA报文";
            this.btn_RT2NMEA.UseVisualStyleBackColor = true;
            this.btn_RT2NMEA.Click += new System.EventHandler(this.btn_RT2NMEA_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(328, 41);
            this.Controls.Add(this.tableLayoutPanel1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_RT2NMEA;
        private System.Windows.Forms.Button btn_GPSWeekCal;
    }
}