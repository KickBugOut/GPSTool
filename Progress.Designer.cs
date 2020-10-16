namespace RT2NMEA
{
    partial class Progress
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.FormerLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FormerLabel
            // 
            this.FormerLabel.BackColor = System.Drawing.Color.Lime;
            this.FormerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FormerLabel.Location = new System.Drawing.Point(0, 0);
            this.FormerLabel.Margin = new System.Windows.Forms.Padding(0);
            this.FormerLabel.Name = "FormerLabel";
            this.FormerLabel.Size = new System.Drawing.Size(198, 18);
            this.FormerLabel.TabIndex = 0;
            this.FormerLabel.Text = " ";
            // 
            // Progress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.FormerLabel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Progress";
            this.Size = new System.Drawing.Size(198, 18);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label FormerLabel;
    }
}
