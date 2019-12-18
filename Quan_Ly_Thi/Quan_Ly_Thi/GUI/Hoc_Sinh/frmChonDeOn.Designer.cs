namespace Quan_Ly_Thi.GUI.Hoc_Sinh
{
    partial class frmChon_De_On
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChon_De_On));
            this.btn_Chon = new System.Windows.Forms.Button();
            this.cbb_DoKho = new System.Windows.Forms.ComboBox();
            this.cbb_MonHoc = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbb_Khoi = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_Chon
            // 
            this.btn_Chon.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Chon.Location = new System.Drawing.Point(100, 140);
            this.btn_Chon.Name = "btn_Chon";
            this.btn_Chon.Size = new System.Drawing.Size(85, 40);
            this.btn_Chon.TabIndex = 10;
            this.btn_Chon.Text = "Chọn";
            this.btn_Chon.UseVisualStyleBackColor = true;
            this.btn_Chon.Click += new System.EventHandler(this.btn_Chon_Click);
            // 
            // cbb_DoKho
            // 
            this.cbb_DoKho.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb_DoKho.FormattingEnabled = true;
            this.cbb_DoKho.Location = new System.Drawing.Point(100, 49);
            this.cbb_DoKho.Name = "cbb_DoKho";
            this.cbb_DoKho.Size = new System.Drawing.Size(216, 32);
            this.cbb_DoKho.TabIndex = 8;
            // 
            // cbb_MonHoc
            // 
            this.cbb_MonHoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb_MonHoc.FormattingEnabled = true;
            this.cbb_MonHoc.Location = new System.Drawing.Point(100, 8);
            this.cbb_MonHoc.Name = "cbb_MonHoc";
            this.cbb_MonHoc.Size = new System.Drawing.Size(216, 32);
            this.cbb_MonHoc.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(15, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 32);
            this.label2.TabIndex = 6;
            this.label2.Text = "Độ Khó:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(15, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 32);
            this.label1.TabIndex = 4;
            this.label1.Text = "Môn:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(15, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 32);
            this.label3.TabIndex = 6;
            this.label3.Text = "Khối";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbb_Khoi
            // 
            this.cbb_Khoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb_Khoi.FormattingEnabled = true;
            this.cbb_Khoi.Location = new System.Drawing.Point(100, 96);
            this.cbb_Khoi.Name = "cbb_Khoi";
            this.cbb_Khoi.Size = new System.Drawing.Size(216, 32);
            this.cbb_Khoi.TabIndex = 8;
            // 
            // frmChon_De_On
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(332, 194);
            this.Controls.Add(this.btn_Chon);
            this.Controls.Add(this.cbb_Khoi);
            this.Controls.Add(this.cbb_DoKho);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbb_MonHoc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmChon_De_On";
            this.Text = "Chọn Đề Ôn";
            this.Load += new System.EventHandler(this.frmChon_De_On_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Chon;
        private System.Windows.Forms.ComboBox cbb_DoKho;
        private System.Windows.Forms.ComboBox cbb_MonHoc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbb_Khoi;
    }
}