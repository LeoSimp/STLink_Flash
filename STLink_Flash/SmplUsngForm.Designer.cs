﻿namespace STLink_Flash
{
    partial class SmplUsngForm
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
            this.pModuel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pModuel
            // 
            this.pModuel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pModuel.Location = new System.Drawing.Point(0, 0);
            this.pModuel.Name = "pModuel";
            this.pModuel.Size = new System.Drawing.Size(641, 604);
            this.pModuel.TabIndex = 0;
            // 
            // SmplUsngForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 604);
            this.Controls.Add(this.pModuel);
            this.Name = "SmplUsngForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pModuel;
    }
}

