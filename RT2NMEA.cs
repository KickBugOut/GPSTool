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
using TrackDataManager;

namespace RT2NMEA
{
    public partial class RT2NMEA : Form
    {
        Progress p;
        private
        System.Timers.Timer t;
        List<string> temp;
        private List<int> tempCounts = new List<int>();
        private List<ParameterizedThreadStart> Methods = new List<ParameterizedThreadStart>();
        private List<Thread> Threads = new List<Thread>();
        private List<List<string>> tempStrings = new List<List<string>>();
        private List<string> Paths = new List<string>();
        public RT2NMEA()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            p = new Progress();
            t = new System.Timers.Timer();
            t.Interval = 60;
            t.Elapsed += T_Elapsed;
            p.Location = new Point(0, 0);
            p.FormerLabel.AutoSize = false;
            p.FormerLabel.Width = 0;
            p.FormerLabel.Dock = DockStyle.None;
            p.FormerLabel.Width = 0;
            p.FormerLabel.Left = 0;
            this.ThreadNumCBX.Items.Add(4);
            this.ThreadNumCBX.Items.Add(8);
            this.ThreadNumCBX.Items.Add(16);
            this.ThreadNumCBX.Items.Add(24);
            this.ThreadNumCBX.Items.Add(32);

            this.Mode.Items.Add("拆分");
            this.Mode.Items.Add("合并"); 

            this.Controls.Add(p);
        }

        private void T_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            int totalCount = 0;
            int threadNums = (int)(ThreadNumCBX.SelectedItem);
            for (int i = 0; i < threadNums; i++)
            {
                totalCount += tempCounts[i];
            }
            double percent = ((int)((double)totalCount / totalLines * 100));
            this.Text = "RT2NMEA-" + (totalCount).ToString() + "/" + totalLines.ToString() + "\t" + "进度:" + percent.ToString() + "%";
            p.FormerLabel.Width = (int)(p.Width * percent / 100.0);
            if (totalCount == totalLines)
            {
                temp.Clear();
                t.Stop();
                this.Text = "RT2NMEA-" + "后台处理中，请稍后";
                p.FormerLabel.Text = "后台处理中，请稍后......"; 
                string Dir = Path.GetDirectoryName(file) + "\\" + Path.GetFileNameWithoutExtension(file);
                string outPutFileName = Path.GetFileNameWithoutExtension(file);
                string outPutFileNameWithPath = Dir + "\\" + outPutFileName + ".NMEA";
                outPutFileNameWithPath = ConfirmFile(outPutFileNameWithPath, FileAttributes.Normal, true);

                //合并
                string tempPath = Application.StartupPath; 
                Thread.Sleep(500);
                string toLeft = "";
                for (int i = 0; i < Paths.Count; i++)
                {
                    toLeft = Save(toLeft, Paths[i], outPutFileNameWithPath);
                    Thread.Sleep(500);
                }
                this.Text = "RT2NMEA";
                p.Hide();
                MessageBox.Show("提取成功，导出文件位于源文件同目录下！\r\n" + outPutFileName, "RT2NMEA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                p.FormerLabel.Text = "";
            }
        }
        /// <summary>
        /// 首次事件
        /// </summary>
        /// <param name="source">NMEA报文</param>
        /// <returns></returns>
        private string FirstTimeOfNMEA(string source)
        {
            string nameMsg = FirstGGA(source);
            string[] GGAStrgps = nameMsg.Split(',');
            return GGAStrgps[1];
        }
        /// <summary>
        /// 最后时间
        /// </summary>
        /// <param name="source">最后GGA所在行字符串</param>
        /// <returns></returns>
        private string LastTimeOfNMEA(string source)
        {
            int index = source.LastIndexOf("GGA");
            if (index == -1)
            {
                Console.WriteLine(source);
            }
            string toleft= source.Substring(index - 3, source.Length - index + 3);
            string[] GGAStrgps = toleft.Split(',');
            return GGAStrgps[1]; ; 
        }
        private string FirstGGA(string source)
        {
            int index = SecondIndexOfString(source, "$");
            return source.Substring(0, index);
        }
        public static bool IsFileInUse(string fileName)
        {
            bool inUse = true;

            FileStream fs = null;
            try
            {

                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read,

                FileShare.None);

                inUse = false;
            }
            catch
            {
            }
            finally
            {
                if (fs != null)

                    fs.Close();
            }
            return inUse;//true表示正在使用,false没有使用  
        }
        private int SecondIndexOfString(string source, string value)
        {
            int j = 0;
            int length = value.Length;
            int findCount = 0;
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == value[0])
                {
                    int innerCount = 1;
                    for (j = 1; j < value.Length; j++)
                    {
                        i++;
                        if (source[i] == value[j])
                        {
                            innerCount++;
                        }
                        if (innerCount != j + 1)
                        {
                            break;
                        }
                    }
                    if (innerCount == length)
                    {
                        findCount++;
                    }

                }
                if (findCount == 2)
                {
                    return i - length + 1;
                }
            }
            return -1;
        }
        private string Save(string toLeft, string tempPath, string outPutFileNameWithPath)
        {
            try
            {
                string tempString = File.ReadAllText(tempPath);
                if (Mode.Text == "合并")
                {
                    File.AppendAllText(outPutFileNameWithPath, tempString);
                    File.Delete(tempPath);
                }
                else
                {
                    int index = tempString.LastIndexOf("GGA");
                    string toAdd = toLeft + tempString.Substring(0, index - 3);
                    toLeft = tempString.Substring(index - 3, tempString.Length - index + 3);
                    new FileInfo(tempPath).Attributes = FileAttributes.Normal;
                    string FirstName = FirstTimeOfNMEA(toAdd);
                    string LastName = LastTimeOfNMEA(toAdd);
                    string outPutName = Path.GetDirectoryName(tempPath) + "\\" + FirstName + "-" + LastName + ".NMEA";
                    File.WriteAllText(tempPath, toAdd);
                    Thread.Sleep(100);
                    if (File.Exists(outPutName)) File.Delete(outPutName);
                    Thread.Sleep(100);
                    FileInfo fi = new FileInfo(tempPath); //xx/xx/aa.rar 
                    fi.MoveTo(outPutName); //xx/xx/xx.rar 
                    new FileInfo(outPutName).Attributes = FileAttributes.Hidden;
                    if (File.Exists(outPutFileNameWithPath)) File.Delete(outPutFileNameWithPath);
                }

            }
            catch (Exception)
            {

                throw;
            }
            return toLeft;

        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ImportFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "所有文件(*.RT27)|*.RT27|(*.txt)|*.txt|(*.NMEA)|*.NMEA";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file = dialog.FileName;
                this.PathText.Text = file;
            }
        }
        static private System.Text.Encoding _encoding = Encoding.GetEncoding("gb2312");//Encoding.UTF8; //Encoding.ASCII;  
        private void T_Tick(object sender, EventArgs e)
        {
        }
        int totalLines = 0;
        /// <summary>
        /// 多线程处理
        /// </summary>
        /// <param name="file"></param>
        private void ReadTxt(string file)
        {
            p.Show();
            p.BringToFront();
            temp = File.ReadLines(file).ToList();
            totalLines = temp.Count;
            int threadNums = (int)(ThreadNumCBX.SelectedItem);
            int perLine = (int)(temp.Count / threadNums);
            {
                string Dir = Path.GetDirectoryName(file) + "\\" + Path.GetFileNameWithoutExtension(file); 
                if (!System.IO.File.Exists(Dir))
                {
                    DirectoryInfo dir = new DirectoryInfo(Dir);
                    dir.Create();//自行判断一下是否存在。
                }
                string path = Dir;
                this.tempCounts.Clear();
                this.Methods.Clear();
                this.Threads.Clear();
                this.tempStrings.Clear();
                int i = 0;
                for ( i = 0; i < threadNums-1; i++)
                {
                    List<string> tempS = temp.GetRange(i*perLine, perLine);
                    tempStrings.Add(tempS);
                    Paths.Add(path + "\\" + i.ToString() + ".txt");
                    tempCounts.Add(0); 
                } 
                List<string> tempLast= temp.GetRange(perLine * (threadNums - 1), totalLines - perLine * (threadNums - 1));
                tempStrings.Add(tempLast);
                Paths.Add(path + "\\" + (threadNums - 1).ToString() + ".txt");
                tempCounts.Add(0);

                for (i = 0; i < threadNums; i++)
                {
                    List<string> strings = tempStrings[i]; 
                    ParameterizedThreadStart method = o => tempMakeDeal(ref strings,Paths[i]);
                    Thread t = new Thread(method);
                    Threads.Add(t);
                    t.Start();
                    t.Join(100);
                }
                  

                t.Start();
            }
        }
        private string file;

        private void tempMakeDeal(ref List<string> temp, string path)
        {
            string temps = "";
            path = ConfirmFile(path, FileAttributes.Hidden, true);
            int index =int.Parse( Path.GetFileNameWithoutExtension(path));
            int num = tempCounts[index];
            foreach (string s in temp)
            { 
                num++;
                tempCounts[index] = num;
                if (s.Contains("$") && !s.Contains("%"))
                {
                    if (s.IndexOf("$G") == 0 || s.IndexOf("$B") == 0 || s.IndexOf("$Q") == 0)
                        temps += s + "\n";
                    else
                    {
                        string[] tempArray = s.Split('$');
                        if (tempArray.Length > 1)
                        {
                            for (int i = 0; i < tempArray.Length; i++)
                            {
                                if (tempArray[i].IndexOf("G") == 0 || tempArray[i].IndexOf("B") == 0 || tempArray[i].IndexOf("Q") == 0)
                                {
                                    temps += '$' + tempArray[i] + "\n";
                                }
                            }
                        }
                    }
                }
                if (temps.Length > 8000)
                {
                    File.AppendAllText(path, temps);
                    temps = "";
                }
            }
            //int totalCount = temp1Count + temp2Count + temp3Count + temp4Count;
            //p.FormerLabel.Text = (totalCount).ToString() + "/" + totalLines.ToString() + "\t" + "进度:" + ((int)((double)totalCount / totalLines * 100)).ToString() + "%";
            if (temps != "")
            {
                File.AppendAllText(path, temps);
            }
        }

        /// <summary>
        /// 读取大文件用,读取文件前面指定长度字节数
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="readByteLength">读取长度,单位字节</param>
        /// <returns></returns>
        public byte[] ReadBigFile(string filePath, int readByteLength)
        {
            FileStream stream = new FileStream(filePath, FileMode.Open);
            byte[] buffer = new byte[readByteLength];
            stream.Read(buffer, 0, readByteLength);
            stream.Close();
            stream.Dispose();
            return buffer;
            //string str = Encoding.Default.GetString(buffer) //如果需要转换成编码字符串的话
        }

        /// <summary>
        /// 读取大文件用,读取文件前面指定长度字节数
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="readByteLength">读取长度,单位字节</param>
        /// <returns></returns>
        public string ReadBigFile_Str(string filePath, int readByteLength)
        {
            FileStream stream = new FileStream(filePath, FileMode.Open);
            byte[] buffer = new byte[readByteLength];
            stream.Read(buffer, 0, readByteLength);
            stream.Close();
            stream.Dispose();
            string str = (Encoding.Default.GetString(buffer));
            return str;  //如果需要转换成编码字符串的话
        }

        public static string ConfirmFile(string path, FileAttributes attributes, bool IsDeleted)
        {
            if (!System.IO.File.Exists(path))
            {
                //没有则创建这个文件
                FileStream fs1 = new FileStream(path, FileMode.Create, FileAccess.Write);//创建写入文件                //设置文件属性为隐藏
                System.IO.File.SetAttributes(@path, attributes);
                StreamWriter sw = new StreamWriter(fs1);
                sw.Close();
                fs1.Close();
                return path;
            }
            if (IsDeleted)
            {
                File.Delete(path);
                //新建则创建这个文件
                FileStream fs1 = new FileStream(path, FileMode.Create, FileAccess.Write);//创建写入文件                //设置文件属性为隐藏
                System.IO.File.SetAttributes(@path, attributes);
                StreamWriter sw = new StreamWriter(fs1);
                sw.Close();
                fs1.Close();
                return path;

            }
            else
            {
                string outDir = Path.GetDirectoryName(path);
                string outName = Path.GetFileNameWithoutExtension(path) + "CreateByRT2NMEA" + Path.GetExtension(path);
                string outPutFileName = outDir + "\\" + outName;
                FileStream fs1 = new FileStream(outPutFileName, FileMode.Create, FileAccess.Write);//创建写入文件                //设置文件属性为隐藏
                System.IO.File.SetAttributes(@outPutFileName, attributes);
                StreamWriter sw = new StreamWriter(fs1);
                sw.Close();
                fs1.Close();
                return outPutFileName;
            }
        }

        private void OutPutBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (file == null)
                {
                    MessageBox.Show("路径不能为空", "RT2NMEA", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                }
                if (!System.IO.File.Exists(file))
                {
                    MessageBox.Show("该文件不存在", "RT2NMEA", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                }

                ParameterizedThreadStart method = ob => ReadTxt(ob.ToString());
                Thread t = new Thread(method);
                this.Text = "RT2NMEA- 准备中……";
                t.Start(file);
                t.Join(10);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.ToString());
            }
        }

        private void PathChooseBtn_Click(object sender, EventArgs e)
        {
            ImportFileToolStripMenuItem_Click(null, null);
        }

        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("说明：多线程RT27转NMEA\r\n" +
                            "版本:2.0Beta\r\n作者:Aniss\r\n@BJTU GPSLAB",
                            "RT2NMEA", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {

            Console.WriteLine(FirstTimeOfNMEA("$GNGGA,120646.80,3214.97719871,N,11207.79237638,E,1,07,2.7,138.978,M,-23.678,M,,*5D" +
                        "$GNGST, 120646.80, 5.023, 13.236, 1.924, 162.2, 12.613, 4.449, 27.330 * 71" +
                        "$GNGGA,150626.80,3214.97719871,N,11207.79237638,E,1,07,2.7,138.978,M,-23.678,M,,*5D"));
            Console.WriteLine(LastTimeOfNMEA("$GNGGA,120646.80,3214.97719871,N,11207.79237638,E,1,07,2.7,138.978,M,-23.678,M,,*5D" +
                        "$GNGST, 120646.80, 5.023, 13.236, 1.924, 162.2, 12.613, 4.449, 27.330 * 71" +
                        "$GNGGA,150626.80,3214.97719871,N,11207.79237638,E,1,07,2.7,138.978,M,-23.678,M,,*5D"));
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ThreadNumCBX.SelectedIndex = 0;
        }

        private void ThreadNumCBX_Click(object sender, EventArgs e)
        {

        }

        private void 文件后添加统一文字ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string content = Inputbox.SetAString("输入添加文字", "文字：", false);
                while (content == "")
                {
                    content = Inputbox.SetAString("输入添加文字", "文字：", false);
                }
                foreach (string file in ofd.FileNames)
                {
                    string name = Path.GetFileNameWithoutExtension(file);
                    string extension = Path.GetExtension(file);
                    string outputName = name + content+extension;
                    string Dir = Path.GetDirectoryName(file);
                    string outputPath = Dir + "\\" + outputName; 
                    FileInfo fi = new FileInfo(file); //xx/xx/aa.rar 
                    fi.Attributes = FileAttributes.Normal;
                    fi.MoveTo(outputPath); //xx/xx/xx.rar    
                }
                MessageBox.Show("改名结束");
            }
        }

        private void 文件前添加序号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                int readCount = 1;
                foreach (string file in ofd.FileNames)
                {
                    string name = Path.GetFileName(file);
                    string outputName = readCount.ToString() + "-"+name;
                    string Dir = Path.GetDirectoryName(file);
                    string outputPath = Dir + "\\" + outputName;
                    FileInfo fi = new FileInfo(file); //xx/xx/aa.rar 
                    fi.Attributes = FileAttributes.Normal;
                    fi.MoveTo(outputPath); //xx/xx/xx.rar   
                    readCount++;
                }
                MessageBox.Show("改名结束");
            }
        }
         
        private void 文件前添加文字ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string content = Inputbox.SetAString("输入添加文字", "文字：", false);
                while (content == "")
                {
                    content = Inputbox.SetAString("输入添加文字", "文字：", false);
                }
                foreach (string file in ofd.FileNames)
                {
                    string name = Path.GetFileNameWithoutExtension(file);
                    string extension = Path.GetExtension(file);
                    string outputName = content +name +  extension;
                    string Dir = Path.GetDirectoryName(file);
                    string outputPath = Dir + "\\" + outputName;
                    FileInfo fi = new FileInfo(file); //xx/xx/aa.rar 
                    fi.Attributes = FileAttributes.Normal;
                    fi.MoveTo(outputPath); //xx/xx/xx.rar    
                }
                MessageBox.Show("改名结束");
            }
        }
         
        private void nMEAToolStripMenuItem_Click_1(object sender, EventArgs e)
        { 
            TimeMatch t = new TimeMatch();
            t.ShowDialog(); 
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(ModifierKeys== Keys.Control && e.KeyCode == Keys.M)
            {
                nMEAToolStripMenuItem_Click_1(null, null);
            }
        }
    }
}