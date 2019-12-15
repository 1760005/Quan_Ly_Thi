namespace Quan_Ly_Thi.GUI.Hoc_Sinh
{
    partial class frmChon_Bai_Thi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChon_Bai_Thi));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbb_Ky_Thi = new System.Windows.Forms.ComboBox();
            this.cbb_Mon = new System.Windows.Forms.ComboBox();
            this.cbb_MaDe = new System.Windows.Forms.ComboBox();
            this.btn_Chon = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kì Thi:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Môn:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(12, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "Mã Đề:";
            // 
            // cbb_Ky_Thi
            // 
            this.cbb_Ky_Thi.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb_Ky_Thi.FormattingEnabled = true;
            this.cbb_Ky_Thi.Location = new System.Drawing.Point(94, 12);
            this.cbb_Ky_Thi.Name = "cbb_Ky_Thi";
            this.cbb_Ky_Thi.Size = new System.Drawing.Size(226, 32);
            this.cbb_Ky_Thi.TabIndex = 2;
            this.cbb_Ky_Thi.TextChanged += new System.EventHandler(this.cbb_Ky_Thi_TextChanged);
            // 
            // cbb_Mon
            // 
            this.cbb_Mon.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb_Mon.FormattingEnabled = true;
            this.cbb_Mon.Location = new System.Drawing.Point(94, 53);
            this.cbb_Mon.Name = "cbb_Mon";
            this.cbb_Mon.Size = new System.Drawing.Size(226, 32);
            this.cbb_Mon.TabIndex = 2;
            this.cbb_Mon.TextChanged += new System.EventHandler(this.cbb_Mon_TextChanged);
            // 
            // cbb_MaDe
            // 
            this.cbb_MaDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb_MaDe.FormattingEnabled = true;
            this.cbb_MaDe.Location = new System.Drawing.Point(94, 97);
            this.cbb_MaDe.Name = "cbb_MaDe";
            this.cbb_MaDe.Size = new System.Drawing.Size(226, 32);
            this.cbb_MaDe.TabIndex = 2;
            // 
            // btn_Chon
            // 
            this.btn_Chon.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Chon.Location = new System.Drawing.Point(94, 135);
            this.btn_Chon.Name = "btn_Chon";
            this.btn_Chon.Size = new System.Drawing.Size(85, 40);
            this.btn_Chon.TabIndex = 3;
            this.btn_Chon.Text = "Chọn";
            this.btn_Chon.UseVisualStyleBackColor = true;
            this.btn_Chon.Click += new System.EventHandler(this.btn_Chon_Click);
            // 
            // frmChon_Bai_Thi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(332, 179);
            this.Controls.Add(this.btn_Chon);
            this.Controls.Add(this.cbb_MaDe);
            this.Controls.Add(this.cbb_Mon);
            this.Controls.Add(this.cbb_Ky_Thi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Red;
            this.Name = "frmChon_Bai_Thi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn Bài Thi";
            this.Load += new System.EventHandler(this.frmChon_Bai_Thi_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbb_Ky_Thi;
        private System.Windows.Forms.ComboBox cbb_Mon;
        private System.Windows.Forms.ComboBox cbb_MaDe;
        private System.Windows.Forms.Button btn_Chon;
    }
}