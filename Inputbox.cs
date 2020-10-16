using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackDataManager
{
    public partial class Inputbox : Form
    {
        public static string result;
        private static bool isNum;
        public Inputbox()
        {
            InitializeComponent();
            this.Height = this.CancelButton.Top + this.CancelButton.Height*2 +20;
            this.Width= this.CancelButton.Left + this.CancelButton.Width +20;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowDialog();
        }
        public Inputbox(string Title, string Tip)
        { 
            InitializeComponent();
            this.Width= this.CancelButton.Left + this.CancelButton.Width +20;
            this.Height = this.CancelButton.Top + this.CancelButton.Height*2 + 20;
            this.Text = Title;
            this.Tip.Text = Tip + ":";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowDialog();
        }

        /// <summary>
        /// 对话框获取一个输入字符的返回值
        /// </summary> 
        /// <returns>输入字符的字符串</returns>
        public static string SetAString(bool _isNum)
        {
            result = ""; 
            Inputbox n = new Inputbox();
            isNum = _isNum;
            return result;
        }
        /// <summary>
        /// 对话框获取一个输入字符的返回值
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="Tip">提示文字</param>
        /// <returns>输入字符的字符串</returns>
        public static string SetAString(string Title, string Tip, bool _isNum)
        {
            result = "";
            isNum = _isNum;
            Inputbox n = new Inputbox(Title,Tip);
            return result;
        }
        private void OkButton_Click(object sender, EventArgs e)
        {
            try
            { 
                result = ContentTextBox.Text;
                if (isNum)
                { 
                    MessageBox.Show("设置成功", "车长设置", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Hide();
                Dispose();
            }
            catch (Exception)
            { 
                MessageBox.Show("数据为空或者不为数字！", "车长设置", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        { 
            Hide();
            Dispose();
        }

        
    }
}
