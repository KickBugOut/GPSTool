namespace TrackDataManager
{
    partial class Inputbox
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
            this.Tip = new System.Windows.Forms.Label();
            this.ContentTextBox = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Tip
            // 
            this.Tip.AutoSize = true;
            this.Tip.Location = new System.Drawing.Point(16, 15);
            this.Tip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Tip.Name = "Tip";
            this.Tip.Size = new System.Drawing.Size(52, 15);
            this.Tip.TabIndex = 0;
            this.Tip.Text = "提示：";
            // 
            // ContentTextBox
            // 
            this.ContentTextBox.Location = new System.Drawing.Point(83, 15);
            this.ContentTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.ContentTextBox.Name = "ContentTextBox";
            this.ContentTextBox.Size = new System.Drawing.Size(408, 25);
            this.ContentTextBox.TabIndex = 1;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(501, 15);
            this.OkButton.Margin = new System.Windows.Forms.Padding(4);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 29);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "确定";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(501, 52);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(100, 29);
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = "退出";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // Inputbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 87);
            this.ControlBox = false;
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.ContentTextBox);
            this.Controls.Add(this.Tip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Inputbox";
            this.Text = "标题";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private  System.Windows.Forms.Label Tip;
        private  System.Windows.Forms.TextBox ContentTextBox;
        internal System.Windows.Forms.Button OkButton;
        private new System.Windows.Forms.Button CancelButton;
    }
}