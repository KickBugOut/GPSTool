namespace RT2NMEA
{
    partial class RT2NMEA
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RT2NMEA));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.FileBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.ImportFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.About = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ThreadNumCBX = new System.Windows.Forms.ToolStripComboBox();
            this.Mode = new System.Windows.Forms.ToolStripComboBox();
            this.Tool = new System.Windows.Forms.ToolStripDropDownButton();
            this.batchRenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文件后添加统一文字ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文件前添加序号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文件前添加文字ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.PathText = new System.Windows.Forms.TextBox();
            this.PathChooseBtn = new System.Windows.Forms.Button();
            this.OutPutBtn = new System.Windows.Forms.Button();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nMEAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileBtn,
            this.toolStripLabel1,
            this.toolStripSeparator1,
            this.ThreadNumCBX,
            this.Mode,
            this.Tool,
            this.About});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(594, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // FileBtn
            // 
            this.FileBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FileBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportFileToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.FileBtn.Image = ((System.Drawing.Image)(resources.GetObject("FileBtn.Image")));
            this.FileBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FileBtn.Name = "FileBtn";
            this.FileBtn.Size = new System.Drawing.Size(45, 22);
            this.FileBtn.Text = "文件";
            // 
            // ImportFileToolStripMenuItem
            // 
            this.ImportFileToolStripMenuItem.Name = "ImportFileToolStripMenuItem";
            this.ImportFileToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.ImportFileToolStripMenuItem.Text = "导入文件";
            this.ImportFileToolStripMenuItem.Click += new System.EventHandler(this.ImportFileToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.ExitToolStripMenuItem.Text = "退出";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // About
            // 
            this.About.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.About.Image = ((System.Drawing.Image)(resources.GetObject("About.Image")));
            this.About.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.About.Name = "About";
            this.About.Size = new System.Drawing.Size(36, 22);
            this.About.Text = "关于";
            this.About.Click += new System.EventHandler(this.About_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel1.Text = "线程数";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ThreadNumCBX
            // 
            this.ThreadNumCBX.AutoSize = false;
            this.ThreadNumCBX.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ThreadNumCBX.Name = "ThreadNumCBX";
            this.ThreadNumCBX.Size = new System.Drawing.Size(40, 25);
            this.ThreadNumCBX.Text = "线程数";
            this.ThreadNumCBX.Click += new System.EventHandler(this.ThreadNumCBX_Click);
            // 
            // Mode
            // 
            this.Mode.AutoSize = false;
            this.Mode.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Mode.Name = "Mode";
            this.Mode.Size = new System.Drawing.Size(50, 25);
            this.Mode.Text = "模式";
            // 
            // Tool
            // 
            this.Tool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Tool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.batchRenameToolStripMenuItem,
            this.nMEAToolStripMenuItem});
            this.Tool.Image = ((System.Drawing.Image)(resources.GetObject("Tool.Image")));
            this.Tool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Tool.Name = "Tool";
            this.Tool.Size = new System.Drawing.Size(45, 22);
            this.Tool.Text = "工具";
            // 
            // batchRenameToolStripMenuItem
            // 
            this.batchRenameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件前添加序号ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.文件前添加文字ToolStripMenuItem,
            this.文件后添加统一文字ToolStripMenuItem});
            this.batchRenameToolStripMenuItem.Name = "batchRenameToolStripMenuItem";
            this.batchRenameToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.batchRenameToolStripMenuItem.Text = "批量重命名";
            // 
            // 文件后添加统一文字ToolStripMenuItem
            // 
            this.文件后添加统一文字ToolStripMenuItem.Name = "文件后添加统一文字ToolStripMenuItem";
            this.文件后添加统一文字ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.文件后添加统一文字ToolStripMenuItem.Text = "文件后添加文字";
            this.文件后添加统一文字ToolStripMenuItem.Click += new System.EventHandler(this.文件后添加统一文字ToolStripMenuItem_Click);
            // 
            // 文件前添加序号ToolStripMenuItem
            // 
            this.文件前添加序号ToolStripMenuItem.Name = "文件前添加序号ToolStripMenuItem";
            this.文件前添加序号ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.文件前添加序号ToolStripMenuItem.Text = "文件前添加序号";
            this.文件前添加序号ToolStripMenuItem.Click += new System.EventHandler(this.文件前添加序号ToolStripMenuItem_Click);
            // 
            // 文件前添加文字ToolStripMenuItem
            // 
            this.文件前添加文字ToolStripMenuItem.Name = "文件前添加文字ToolStripMenuItem";
            this.文件前添加文字ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.文件前添加文字ToolStripMenuItem.Text = "文件前添加文字";
            this.文件前添加文字ToolStripMenuItem.Click += new System.EventHandler(this.文件前添加文字ToolStripMenuItem_Click);
            // 
            // splitter3
            // 
            this.splitter3.Location = new System.Drawing.Point(0, 25);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(3, 41);
            this.splitter3.TabIndex = 9;
            this.splitter3.TabStop = false;
            // 
            // PathText
            // 
            this.PathText.Location = new System.Drawing.Point(5, 35);
            this.PathText.Name = "PathText";
            this.PathText.ReadOnly = true;
            this.PathText.Size = new System.Drawing.Size(402, 21);
            this.PathText.TabIndex = 10;
            // 
            // PathChooseBtn
            // 
            this.PathChooseBtn.Location = new System.Drawing.Point(410, 34);
            this.PathChooseBtn.Name = "PathChooseBtn";
            this.PathChooseBtn.Size = new System.Drawing.Size(83, 24);
            this.PathChooseBtn.TabIndex = 11;
            this.PathChooseBtn.Text = "源选择路径";
            this.PathChooseBtn.UseVisualStyleBackColor = true;
            this.PathChooseBtn.Click += new System.EventHandler(this.PathChooseBtn_Click);
            // 
            // OutPutBtn
            // 
            this.OutPutBtn.Location = new System.Drawing.Point(502, 34);
            this.OutPutBtn.Name = "OutPutBtn";
            this.OutPutBtn.Size = new System.Drawing.Size(83, 24);
            this.OutPutBtn.TabIndex = 12;
            this.OutPutBtn.Text = "导出";
            this.OutPutBtn.UseVisualStyleBackColor = true;
            this.OutPutBtn.Click += new System.EventHandler(this.OutPutBtn_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem1.Text = "文件后添加序号";
            // 
            // nMEAToolStripMenuItem
            // 
            this.nMEAToolStripMenuItem.Name = "nMEAToolStripMenuItem";
            this.nMEAToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.nMEAToolStripMenuItem.Text = "NMEA时间对齐";
            this.nMEAToolStripMenuItem.Click += new System.EventHandler(this.nMEAToolStripMenuItem_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 66);
            this.Controls.Add(this.OutPutBtn);
            this.Controls.Add(this.PathChooseBtn);
            this.Controls.Add(this.PathText);
            this.Controls.Add(this.splitter3);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "RT2NMEA";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton FileBtn;
        private System.Windows.Forms.ToolStripMenuItem ImportFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.TextBox PathText;
        private System.Windows.Forms.Button PathChooseBtn;
        private System.Windows.Forms.Button OutPutBtn;
        private System.Windows.Forms.ToolStripButton About;
        private System.Windows.Forms.ToolStripComboBox ThreadNumCBX;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox Mode;
        private System.Windows.Forms.ToolStripDropDownButton Tool;
        private System.Windows.Forms.ToolStripMenuItem batchRenameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文件后添加统一文字ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文件前添加序号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文件前添加文字ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem nMEAToolStripMenuItem;
    }
}

