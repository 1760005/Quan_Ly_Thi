namespace Quan_Ly_Thi.GUI.Hoc_Sinh.Report_Viewer
{
    partial class PhieuDiem
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.KETQUATHIBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.NGUOIDUNGBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.KYTHIBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MONHOCBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.KETQUATHIBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NGUOIDUNGBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KYTHIBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MONHOCBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // KETQUATHIBindingSource
            // 
            this.KETQUATHIBindingSource.DataSource = typeof(Quan_Ly_Thi.DAO.KETQUATHI);
            // 
            // NGUOIDUNGBindingSource
            // 
            this.NGUOIDUNGBindingSource.DataSource = typeof(Quan_Ly_Thi.DAO.NGUOIDUNG);
            // 
            // KYTHIBindingSource
            // 
            this.KYTHIBindingSource.DataSource = typeof(Quan_Ly_Thi.DAO.KYTHI);
            // 
            // MONHOCBindingSource
            // 
            this.MONHOCBindingSource.DataSource = typeof(Quan_Ly_Thi.DAO.MONHOC);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "KetQua";
            reportDataSource1.Value = this.KETQUATHIBindingSource;
            reportDataSource2.Name = "NguoiDung";
            reportDataSource2.Value = this.NGUOIDUNGBindingSource;
            reportDataSource3.Name = "KyThi";
            reportDataSource3.Value = this.KYTHIBindingSource;
            reportDataSource4.Name = "MonHoc";
            reportDataSource4.Value = this.MONHOCBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Quan_Ly_Thi.GUI.Hoc_Sinh.Report_Viewer.PhieuDiem.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // PhieuDiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "PhieuDiem";
            this.Text = "Phiếu Điểm";
            this.Load += new System.EventHandler(this.PhieuDiem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.KETQUATHIBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NGUOIDUNGBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KYTHIBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MONHOCBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource KETQUATHIBindingSource;
        private System.Windows.Forms.BindingSource MONHOCBindingSource;
        private System.Windows.Forms.BindingSource NGUOIDUNGBindingSource;
        private System.Windows.Forms.BindingSource KYTHIBindingSource;
    }
}