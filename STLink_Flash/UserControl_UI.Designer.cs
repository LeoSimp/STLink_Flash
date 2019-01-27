namespace STLink_Flash
{
    /// <summary>
    /// 
    /// </summary>
    partial class UserControl_UI
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_FileSet = new System.Windows.Forms.Button();
            this.tb_FlashFile = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.rtb_STLink = new System.Windows.Forms.RichTextBox();
            this.tb_FileNameFullString = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btn_RunBat = new System.Windows.Forms.Button();
            this.cb_Debug = new System.Windows.Forms.CheckBox();
            this.UC_Tittle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::STLink_Flash.Properties.Resources.ICO;
            this.pictureBox1.Location = new System.Drawing.Point(3, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(417, 86);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(3, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 15);
            this.label3.TabIndex = 36;
            this.label3.Text = "File Name:";
            // 
            // btn_FileSet
            // 
            this.btn_FileSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_FileSet.Location = new System.Drawing.Point(296, 114);
            this.btn_FileSet.Name = "btn_FileSet";
            this.btn_FileSet.Size = new System.Drawing.Size(124, 27);
            this.btn_FileSet.TabIndex = 35;
            this.btn_FileSet.Text = "烧录文件设置";
            this.btn_FileSet.UseVisualStyleBackColor = true;
            this.btn_FileSet.Click += new System.EventHandler(this.btn_FileSet_Click);
            // 
            // tb_FlashFile
            // 
            this.tb_FlashFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_FlashFile.Location = new System.Drawing.Point(6, 116);
            this.tb_FlashFile.Margin = new System.Windows.Forms.Padding(4);
            this.tb_FlashFile.Name = "tb_FlashFile";
            this.tb_FlashFile.ReadOnly = true;
            this.tb_FlashFile.Size = new System.Drawing.Size(280, 25);
            this.tb_FlashFile.TabIndex = 34;
            this.tb_FlashFile.TextChanged += new System.EventHandler(this.tb_FlashFile_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(3, 189);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(160, 15);
            this.label14.TabIndex = 33;
            this.label14.Text = "STLink_Flash.bat:";
            // 
            // rtb_STLink
            // 
            this.rtb_STLink.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_STLink.Location = new System.Drawing.Point(3, 215);
            this.rtb_STLink.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtb_STLink.Name = "rtb_STLink";
            this.rtb_STLink.ReadOnly = true;
            this.rtb_STLink.Size = new System.Drawing.Size(417, 283);
            this.rtb_STLink.TabIndex = 32;
            this.rtb_STLink.Text = "";
            // 
            // tb_FileNameFullString
            // 
            this.tb_FileNameFullString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_FileNameFullString.BackColor = System.Drawing.Color.White;
            this.tb_FileNameFullString.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_FileNameFullString.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tb_FileNameFullString.Location = new System.Drawing.Point(6, 149);
            this.tb_FileNameFullString.Margin = new System.Windows.Forms.Padding(4);
            this.tb_FileNameFullString.Multiline = true;
            this.tb_FileNameFullString.Name = "tb_FileNameFullString";
            this.tb_FileNameFullString.ReadOnly = true;
            this.tb_FileNameFullString.Size = new System.Drawing.Size(414, 36);
            this.tb_FileNameFullString.TabIndex = 37;
            this.tb_FileNameFullString.Text = "File name full string";
            // 
            // btn_RunBat
            // 
            this.btn_RunBat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_RunBat.Enabled = false;
            this.btn_RunBat.Location = new System.Drawing.Point(294, 184);
            this.btn_RunBat.Name = "btn_RunBat";
            this.btn_RunBat.Size = new System.Drawing.Size(124, 27);
            this.btn_RunBat.TabIndex = 38;
            this.btn_RunBat.Text = "Run BAT";
            this.btn_RunBat.UseVisualStyleBackColor = true;
            this.btn_RunBat.Click += new System.EventHandler(this.btn_RunBat_Click);
            // 
            // cb_Debug
            // 
            this.cb_Debug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Debug.AutoSize = true;
            this.cb_Debug.Location = new System.Drawing.Point(254, 189);
            this.cb_Debug.Name = "cb_Debug";
            this.cb_Debug.Size = new System.Drawing.Size(37, 19);
            this.cb_Debug.TabIndex = 39;
            this.cb_Debug.Text = "D";
            this.cb_Debug.UseVisualStyleBackColor = true;
            this.cb_Debug.CheckedChanged += new System.EventHandler(this.cb_Debug_CheckedChanged);
            // 
            // UC_Tittle
            // 
            this.UC_Tittle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UC_Tittle.BackColor = System.Drawing.Color.Transparent;
            this.UC_Tittle.Font = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UC_Tittle.Location = new System.Drawing.Point(190, 0);
            this.UC_Tittle.Name = "UC_Tittle";
            this.UC_Tittle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.UC_Tittle.Size = new System.Drawing.Size(227, 22);
            this.UC_Tittle.TabIndex = 40;
            this.UC_Tittle.Text = "Tittle";
            this.UC_Tittle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UserControl_UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.UC_Tittle);
            this.Controls.Add(this.cb_Debug);
            this.Controls.Add(this.btn_RunBat);
            this.Controls.Add(this.tb_FileNameFullString);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_FileSet);
            this.Controls.Add(this.tb_FlashFile);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.rtb_STLink);
            this.Controls.Add(this.pictureBox1);
            this.Name = "UserControl_UI";
            this.Size = new System.Drawing.Size(420, 500);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_FileSet;
        private System.Windows.Forms.TextBox tb_FlashFile;
        private System.Windows.Forms.Label label14;
        internal System.Windows.Forms.RichTextBox rtb_STLink;
        internal System.Windows.Forms.TextBox tb_FileNameFullString;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btn_RunBat;
        private System.Windows.Forms.CheckBox cb_Debug;
        private System.Windows.Forms.Label UC_Tittle;
    }
}
