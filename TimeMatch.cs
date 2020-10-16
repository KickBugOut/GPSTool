using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RT2NMEA
{
    public partial class TimeMatch : Form
    {
        public TimeMatch()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.timer1.Interval = 1000 / 20;
            this.timer1.Tick += Timer1_Tick;
            this.timer2.Interval = 1000 / 100;
            this.timer2.Elapsed += Timer2_Tick;
            this.leftDir = false;
            this.timer1.Start();
            this.Text = "时间对齐工具";
            this.file1Pathtxt.Text = "D:\\0\\GISON20191217200641.NMEA";
            this.file2Pathtxt.Text = "D:\\0\\\\GISON20191217170003_BD980.NMEA";
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (wait)
            {
                this.Text = "时间匹配——准备中";
            }
            if (work)
            {
                string file1 = "文件1：" + file1ReadLines.ToString("D2") + "/" + file1TotalLines.ToString("D2");
                string file2 = "文件2：" + file2ReadLines.ToString("D2") + "/" + file2TotalLines.ToString("D2");
                double pro = ((file1ReadLines + file2ReadLines) * 100.0 / (file1TotalLines + file2TotalLines));
                string Progress ="进度："+ pro.ToString("f2") + "%";
                this.Text = "时间匹配:——" + file1 + "&" + file2 + " " + Progress;
                if ((file1ReadLines+ file2ReadLines) == (file1TotalLines+ file2TotalLines))
                {
                    needUpdate = false;
                    outPut.Enabled = true;
                    file1PathSearchBtn.Enabled = true;
                    file2PathSearchBtn.Enabled = true;
                    timer2.Stop(); 
                    MessageBox.Show("处理结束");
                }
            }
        }

        private bool leftDir;

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (!leftDir)
            {

                this.label1.Left += 2;
                if (this.label1.Left + this.label1.Width >= this.Width)
                {
                    leftDir = true;
                }
            }
            else
            {
                 
                this.label1.Left -=2;
                if (this.label1.Left <=0)
                {
                    leftDir = false;
                }
            }

        }

        private void TimeMatch_Load(object sender, EventArgs e)
        {

        }

        private void file1PathSearchBtn_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "(*.NMEA)|*.NMEA";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.file1Pathtxt.Text = ofd.FileName;

            }
        } 
        private void file2PathSearchBtn_Click(object sender, EventArgs e)
        { 
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "(*.NMEA)|*.NMEA";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.file2Pathtxt.Text = ofd.FileName;

            }
        }
        bool MatchFlag = false;
        bool LostMatch = true;
        bool File1Wait = false;
        bool File2Wait = false;
        int file1TotalLines = 0;
        int file2TotalLines = 0;
        int file1ReadLines = 0;
        int file2ReadLines = 0;
        bool wait = false;
        bool work = false;
        bool needUpdate = false;
        private void outPut_Click(object sender, EventArgs e)
        {

            outPut.Enabled = false;
            file1PathSearchBtn.Enabled = false;
            file2PathSearchBtn.Enabled = false;
            Thread t = new Thread(OutputThread);
            t.Start();
        }
        Thread f1Read;
        Thread f2Read;
        DateTime File1NowTime = new DateTime();
        DateTime File2NowTime = new DateTime();
        bool file1TimeIsOk = false;
        bool file2TimeIsOK = false;

        [Obsolete]
        private void OutputThread()
        { 
            needUpdate = true;
            wait = true;
            work = false;
            MatchFlag = false;
            LostMatch = true;
            File1Wait = false;
            File2Wait = false;
            file1TotalLines = 0;
            file2TotalLines = 0;
            file1ReadLines = 0;
            file2ReadLines = 0;
            List<string> file1Source = File.ReadAllLines(file1Pathtxt.Text).ToList() ;
            List<string> file2Source = File.ReadAllLines(file2Pathtxt.Text).ToList() ;
            wait = false;
            work = true;
            file1TotalLines = file1Source.Count;
            file2TotalLines = file2Source.Count;
            ParameterizedThreadStart method1 = o => File1ReadThread(file1Source, file1Pathtxt.Text);
            ParameterizedThreadStart method2 = o => File2ReadThread(file2Source, file2Pathtxt.Text);
            f1Read = new Thread(method1);
            f2Read = new Thread(method2);
            f1Read.Start();
            f2Read.Start();
            timer2.Start();
        }
        int LostCount = 0;
        [Obsolete]
        private void CheckMatch()
        { 
            if (file1TimeIsOk && file2TimeIsOK)
            {
                int res = DateTime.Compare(File1NowTime, File2NowTime);
                if (res == 0)
                {
                    MatchFlag = true;
                    LostCount = 0;
                    File1Wait = false;
                    File2Wait = false;  
                }
                else if (res < 0)
                {
                    LostCount++;
                    if (LostCount > 50)
                    {
                        MatchFlag = false;
                        LostMatch = true; 
                    }  
                    File1Wait = false;
                    File2Wait = true;  
                }
                else
                {
                    LostCount++;
                    if (LostCount > 50) 
                    {
                        MatchFlag = false;
                        LostMatch = true;
                    } 
                    File1Wait = true;
                    File2Wait = false; 
                }
            }
        }
        private List<string> File1SuperBuffer = new List<string>();
        private List<string> File2SuperBuffer = new List<string>();
        [Obsolete]
        private void File1ReadThread(List<string> file1Source,string file)
        {
            string file1DealBuffer = "";
            File1SuperBuffer.Clear();
            //try
            //{
                string Dir = Path.GetDirectoryName(file) + "\\" + Path.GetFileNameWithoutExtension(file);
                string file1CmpString = "";
                bool start = false;
                bool firstRMCTime = false;  
                DateTime startTime = new DateTime(); 
                while (file1ReadLines < file1TotalLines )
                {

                    if (file2ReadLines == file2TotalLines) File1Wait = false;
                    if (File1Wait) continue;
                    if (MatchFlag && LostMatch)//存储匹配前的，开始匹配到的时候
                    {
                        //RMC已经存入在读一行
                        file1CmpString = file1Source[file1ReadLines];
                        while(!file1CmpString.Contains("GGA"))
                        { 
                            file1DealBuffer += file1CmpString+'\n'; 
                            file1ReadLines++;
                            file1CmpString = file1Source[file1ReadLines];
                        }
                        string outPutFileName = Path.GetFileNameWithoutExtension(file);
                        string date = startTime.Year.ToString("D2") + "-" + startTime.Month.ToString("D2") + "-" + startTime.Day.ToString("D2");
                        string outputDir = (Dir + "\\" + date) + "\\" ;
                        if (!System.IO.File.Exists(outputDir))
                        {
                            DirectoryInfo dir = new DirectoryInfo(outputDir);
                            dir.Create();//自行判断一下是否存在。
                        }

                        string startTimeHour =String.Format("{0:D2}",startTime.TimeOfDay.Hours.ToString("D2"));
                        string startTimeMin = String.Format("{0:D2}",startTime.TimeOfDay.Minutes.ToString("D2"));
                        string startTimeSec = String.Format("{0:D2}", startTime.TimeOfDay.Seconds.ToString("D2"));
                        string startTimeMs = String.Format("{0:D2}", startTime.TimeOfDay.Milliseconds.ToString("D2"));
                        string startTimeStr = startTimeHour + "-" + startTimeMin + "-" + startTimeSec + "-" + startTimeMs;
                        string File1NowTimeHour = String.Format("{0:D2}", File1NowTime.TimeOfDay.Hours.ToString("D2"));
                        string File1NowTimeMin = String.Format("{0:D2}", File1NowTime.TimeOfDay.Minutes.ToString("D2"));
                        string File1NowTimeSec = String.Format("{0:D2}", File1NowTime.TimeOfDay.Seconds.ToString("D2"));
                        string File1NowTimeMs = String.Format("{0:D2}", File1NowTime.TimeOfDay.Milliseconds.ToString("D2"));
                        string File1NowTimeStr = File1NowTimeHour + "-" + File1NowTimeMin + "-" + File1NowTimeSec + "-" + File1NowTimeMs;

                        string outPutFileNameWithPath = outputDir + startTimeStr + "  " + File1NowTimeStr + ".NMEA";
                        outPutFileNameWithPath = RT2NMEA.ConfirmFile(outPutFileNameWithPath, FileAttributes.Normal, true); 
                        File.WriteAllText(outPutFileNameWithPath, "");
                        File.AppendAllLines(outPutFileNameWithPath, File1SuperBuffer); 
                        File.AppendAllText(outPutFileNameWithPath, file1DealBuffer);
                        LostMatch = false;
                        firstRMCTime = false;
                        start = false;
                        file1DealBuffer = "";
                        File1SuperBuffer.Clear();
                    }
                    if (!File1Wait)
                    {
                        file1CmpString = file1Source[file1ReadLines];
                        file1ReadLines++;
                        if (file1CmpString.Contains("GGA") && start == false)
                        {
                            start = true; 
                        }
                        if (start)
                        { 
                            file1DealBuffer += file1CmpString + '\n';
                        }
                        if(file1CmpString.Contains("RMC")&& start == true) 
                        {
                            file1TimeIsOk = false;
                            string[] RmcGps = file1CmpString.Split(',');
                            string time = "";
                            string date = "";
                            bool isNewDay = false;
                            if (RmcGps [1] != "")//UTC时间
                            {
                                string msg = RmcGps [1];
                                if (RmcGps [1].Contains("."))
                                {
                                    int index = msg.IndexOf('.');
                                    if (index > 0)
                                        msg = msg.Substring(index + 1, msg.Length - index - 1);
                                }
                                double d = Double.Parse(RmcGps [1]);
                                int t = (int)d;
                                int h = t / 10000;
                                int m = (t - h * 10000) / 100;
                                int s = t % 100;
                                int ms = int.Parse(msg);
                                h = h + 8;
                                if (h >= 24)
                                {
                                    isNewDay = true;
                                    h = h - 24;
                                }

                                time = string.Format("{0:D2}", h) + ":" + string.Format("{0:D2}", m) + ":" + string.Format("{0:D2}", s) + "." + string.Format("{0:D2}", ms);
                            } 
                            if (RmcGps [9] != "")//日期
                            {
                                int t = Int32.Parse(RmcGps [9]);
                                int dd = t / 10000;
                                int mm = (t - dd * 10000) / 100;
                                int yy = t % 100;
                                if (isNewDay) dd += 1;
                                if (mm == 2)
                                {
                                    if (yy / 4 == 0 && yy / 100 != 0 || yy / 400 == 0)
                                    {
                                        if (dd > 29)
                                        {
                                            mm += 1;
                                            dd -= 29;
                                        }
                                    }
                                    else
                                    {
                                        if (dd > 28)
                                        {
                                            mm += 1;
                                            dd -= 28;
                                        }

                                    }
                                }
                                else if (mm == 1 || mm == 3 || mm == 5 || mm == 7 || mm == 8 || mm == 10)
                                {
                                    if (dd > 31)
                                    {
                                        mm += 1;
                                        dd -= 31;
                                    }
                                }
                                else if (mm == 12)
                                {

                                }
                                else
                                {
                                    if (dd > 30)
                                    {
                                        mm += 1;
                                        dd -= 30;
                                    }
                                }
                                date = string.Format("{0:D2}", 2000 + yy) + "-" + string.Format("{0:D2}", mm) + "-" + string.Format("{0:D2}", dd);
                            }
                            if (date != "" && time != "") 
                            {
                                if(DateTime.TryParse(date + " " + time,out File1NowTime))
                                {
                                    file1TimeIsOk = true;
                                    if (!firstRMCTime)
                                    {
                                        firstRMCTime = true;
                                        startTime = File1NowTime;
                                    }
                                    if (file1ReadLines < file1TotalLines) CheckMatch(); 
                                } 
                            } 
                        }
                        if (file1DealBuffer.Length > 10000)
                        {
                            File1SuperBuffer.Add(file1DealBuffer);
                            file1DealBuffer = "";
                        }

                    }  
                }
                Thread.Sleep(50); 
                MessageBox.Show("文件1匹配结束");
                if (file1DealBuffer != "" || File1SuperBuffer.Count == 0)
                {
                    string date = startTime.Year.ToString("D2") + "-" + startTime.Month.ToString("D2") + "-" + startTime.Day.ToString("D2");
                    string outputDir = (Dir + "\\" + date) + "\\";

                    string startTimeHour = String.Format("{0:D2}", startTime.TimeOfDay.Hours.ToString("D2"));
                    string startTimeMin = String.Format("{0:D2}", startTime.TimeOfDay.Minutes.ToString("D2"));
                    string startTimeSec = String.Format("{0:D2}", startTime.TimeOfDay.Seconds.ToString("D2"));
                    string startTimeMs = String.Format("{0:D2}", startTime.TimeOfDay.Milliseconds.ToString("D2"));
                    string startTimeStr = startTimeHour + "-" + startTimeMin + "-" + startTimeSec + "-" + startTimeMs;
                    string File1NowTimeHour = String.Format("{0:D2}", File1NowTime.TimeOfDay.Hours.ToString("D2"));
                    string File1NowTimeMin = String.Format("{0:D2}", File1NowTime.TimeOfDay.Minutes.ToString("D2"));
                    string File1NowTimeSec = String.Format("{0:D2}", File1NowTime.TimeOfDay.Seconds.ToString("D2"));
                    string File1NowTimeMs = String.Format("{0:D2}", File1NowTime.TimeOfDay.Milliseconds.ToString("D2"));
                    string File1NowTimeStr = File1NowTimeHour + "-" + File1NowTimeMin + "  " + File1NowTimeSec + "-" + File1NowTimeMs; 

                    string outPutFileNameWithPath = outputDir + startTimeStr + "-" + File1NowTimeStr + ".NMEA"; 
                    outPutFileNameWithPath = RT2NMEA.ConfirmFile(outPutFileNameWithPath, FileAttributes.Normal, true);
                    File.AppendAllText(outPutFileNameWithPath, "");
                    File.AppendAllLines(outPutFileNameWithPath, File1SuperBuffer);
                    File.AppendAllText(outPutFileNameWithPath, file1DealBuffer);
                }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }

        [Obsolete]
        private void File2ReadThread(List<string> file2Source,string file)
        { 
            string file2DealBuffer = "";
            File2SuperBuffer.Clear();
            try
            {
                string Dir = Path.GetDirectoryName(file) + "\\" + Path.GetFileNameWithoutExtension(file);
                string file2CmpString = ""; 
                bool start = false;
                bool firstRMCTime = false;
                bool end = false;
                DateTime startTime = new DateTime();
                while (file2ReadLines < file2TotalLines )  
                {
                    if (file2ReadLines == file2TotalLines) File2Wait = false;
                    if (File2Wait) continue;
                    if (MatchFlag && LostMatch)//存储匹配前的，开始匹配到的时候
                    {
                        //RMC已经存入在读一行
                        file2CmpString = file2Source[file2ReadLines];
                        while (!file2CmpString.Contains("GGA"))
                        {
                            file2DealBuffer += file2CmpString + '\n';
                            file2ReadLines++;
                            file2CmpString = file2Source[file2ReadLines];
                        }
                        string outPutFileName = Path.GetFileNameWithoutExtension(file);
                        string date = startTime.Year.ToString("D2") + "-" + startTime.Month.ToString("D2") + "-" + startTime.Day.ToString("D2");
                        string outputDir = (Dir + "\\" + date) + "\\";
                        if (!System.IO.File.Exists(outputDir))
                        {
                            DirectoryInfo dir = new DirectoryInfo(outputDir);
                            dir.Create();//自行判断一下是否存在。
                        }

                        string startTimeHour = String.Format("{0:D2}", startTime.TimeOfDay.Hours.ToString("D2"));
                        string startTimeMin = String.Format("{0:D2}", startTime.TimeOfDay.Minutes.ToString("D2"));
                        string startTimeSec = String.Format("{0:D2}", startTime.TimeOfDay.Seconds.ToString("D2"));
                        string startTimeMs = String.Format("{0:D2}", startTime.TimeOfDay.Milliseconds.ToString("D2"));
                        string startTimeStr = startTimeHour + "-" + startTimeMin + "-" + startTimeSec + "-" + startTimeMs;
                        string File2NowTimeHour = String.Format("{0:D2}", File1NowTime.TimeOfDay.Hours.ToString("D2"));
                        string File2NowTimeMin = String.Format("{0:D2}", File1NowTime.TimeOfDay.Minutes.ToString("D2"));
                        string File2NowTimeSec = String.Format("{0:D2}", File1NowTime.TimeOfDay.Seconds.ToString("D2"));
                        string File2NowTimeMs = String.Format("{0:D2}", File1NowTime.TimeOfDay.Milliseconds.ToString("D2"));
                        string File2NowTimeStr = File2NowTimeHour + "-" + File2NowTimeMin + "-" + File2NowTimeSec + "-" + File2NowTimeMs;



                        string outPutFileNameWithPath = outputDir + startTimeStr + "  " + File2NowTimeStr + ".NMEA";
                        outPutFileNameWithPath = RT2NMEA.ConfirmFile(outPutFileNameWithPath, FileAttributes.Normal, true);
                        File.AppendAllText(outPutFileNameWithPath, "");
                        File.AppendAllLines(outPutFileNameWithPath, File2SuperBuffer); 
                        File.AppendAllText(outPutFileNameWithPath, file2DealBuffer);
                        LostMatch = false;
                        firstRMCTime = false;
                        start = false;
                        file2DealBuffer = "";
                        File2SuperBuffer.Clear();
                    }
                    if (!File2Wait)
                    {
                        file2CmpString = file2Source[file2ReadLines];
                        file2ReadLines++;
                        if (file2CmpString.Contains("GGA") && start == false)
                        {
                            start = true;
                            firstRMCTime = false;
                        }
                        if (start)
                        {
                            file2DealBuffer += file2CmpString + '\n';
                        }
                        if (file2CmpString.Contains("RMC") && start == true)
                        {
                            file2TimeIsOK = false;
                            string[] RmcGps = file2CmpString.Split(',');
                            string time = "";
                            string date = "";
                            bool isNewDay = false;
                            if (RmcGps[1] != "")//UTC时间
                            {
                                string msg = RmcGps[1];
                                if (RmcGps[1].Contains("."))
                                {
                                    int index = msg.IndexOf('.');
                                    if (index > 0)
                                        msg = msg.Substring(index + 1, msg.Length - index - 1);
                                }
                                double d = Double.Parse(RmcGps[1]);
                                int t = (int)d;
                                int h = t / 10000;
                                int m = (t - h * 10000) / 100;
                                int s = t % 100;
                                int ms = int.Parse(msg);
                                h = h + 8;
                                if (h >= 24)
                                {
                                    isNewDay = true;
                                    h = h - 24;
                                }

                                time = string.Format("{0:D2}", h) + ":" + string.Format("{0:D2}", m) + ":" + string.Format("{0:D2}", s) + "." + string.Format("{0:D2}", ms);
                            }
                            if (RmcGps[9] != "")//日期
                            {
                                int t = Int32.Parse(RmcGps[9]);
                                int dd = t / 10000;
                                int mm = (t - dd * 10000) / 100;
                                int yy = t % 100;
                                if (isNewDay) dd += 1;
                                if (mm == 2)
                                {
                                    if (yy / 4 == 0 && yy / 100 != 0 || yy / 400 == 0)
                                    {
                                        if (dd > 29)
                                        {
                                            mm += 1;
                                            dd -= 29;
                                        }
                                    }
                                    else
                                    {
                                        if (dd > 28)
                                        {
                                            mm += 1;
                                            dd -= 28;
                                        }

                                    }
                                }
                                else if (mm == 1 || mm == 3 || mm == 5 || mm == 7 || mm == 8 || mm == 10)
                                {
                                    if (dd > 31)
                                    {
                                        mm += 1;
                                        dd -= 31;
                                    }
                                }
                                else if (mm == 12)
                                {

                                }
                                else
                                {
                                    if (dd > 30)
                                    {
                                        mm += 1;
                                        dd -= 30;
                                    }
                                }
                                date = string.Format("{0:D2}", 2000 + yy) + "-" + string.Format("{0:D2}", mm) + "-" + string.Format("{0:D2}", dd);
                            }
                            if (date != "" && time != "")
                            {
                                if (DateTime.TryParse(date + " " + time, out File2NowTime))
                                {
                                    file2TimeIsOK = true;
                                    if (!firstRMCTime) 
                                    {
                                        firstRMCTime = true;
                                        startTime = File2NowTime;
                                    } 
                                    if(file2ReadLines < file2TotalLines)  CheckMatch(); 
                                }
                            }
                        }

                        if (file2DealBuffer.Length > 10000)
                        {
                            File2SuperBuffer.Add(file2DealBuffer);
                            file2DealBuffer = "";
                        }
                    } 
                } 
                Thread.Sleep(50);
                MessageBox.Show("文件2匹配结束");
                if (file2DealBuffer != "" || File2SuperBuffer.Count == 0)
                {
                    string date = startTime.Year.ToString("D2") + "-" + startTime.Month.ToString("D2") + "-" + startTime.Day.ToString("D2");
                    string outputDir = (Dir + "\\" + date) + "\\";

                    string startTimeHour = String.Format("{0:D2}", startTime.TimeOfDay.Hours.ToString("D2"));
                    string startTimeMin = String.Format("{0:D2}", startTime.TimeOfDay.Minutes.ToString("D2"));
                    string startTimeSec = String.Format("{0:D2}", startTime.TimeOfDay.Seconds.ToString("D2"));
                    string startTimeMs = String.Format("{0:D2}", startTime.TimeOfDay.Milliseconds.ToString("D2"));
                    string startTimeStr = startTimeHour + "-" + startTimeMin + "-" + startTimeSec + "-" + startTimeMs;
                    string File2NowTimeHour = String.Format("{0:D2}", File1NowTime.TimeOfDay.Hours.ToString("D2"));
                    string File2NowTimeMin = String.Format("{0:D2}", File1NowTime.TimeOfDay.Minutes.ToString("D2"));
                    string File2NowTimeSec = String.Format("{0:D2}", File1NowTime.TimeOfDay.Seconds.ToString("D2"));
                    string File2NowTimeMs = String.Format("{0:D2}", File1NowTime.TimeOfDay.Milliseconds.ToString("D2"));
                    string File2NowTimeStr = File2NowTimeHour + "-" + File2NowTimeMin + "-" + File2NowTimeSec + "-" + File2NowTimeMs;
                    string outPutFileNameWithPath = outputDir + startTimeStr + "  " + File2NowTimeStr + ".NMEA";
                    outPutFileNameWithPath = RT2NMEA.ConfirmFile(outPutFileNameWithPath, FileAttributes.Normal, true);
                    File.AppendAllText(outPutFileNameWithPath, "");
                    File.AppendAllLines(outPutFileNameWithPath, File2SuperBuffer);
                    File.AppendAllText(outPutFileNameWithPath, file2DealBuffer);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
