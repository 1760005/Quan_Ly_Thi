namespace Quan_Ly_Thi.GUI.Hoc_Sinh.Report_Viewer
{
    partial class De_On
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
            this.MONHOCBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DETHIBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CAUHOIBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.MONHOCBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DETHIBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CAUHOIBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // MONHOCBindingSource
            // 
            this.MONHOCBindingSource.DataSource = typeof(Quan_Ly_Thi.DAO.MONHOC);
            // 
            // DETHIBindingSource
            // 
            this.DETHIBindingSource.DataSource = typeof(Quan_Ly_Thi.DAO.DETHI);
            // 
            // CAUHOIBindingSource
            // 
            this.CAUHOIBindingSource.DataSource = typeof(Quan_Ly_Thi.DAO.CAUHOI);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Mon_Hoc";
            reportDataSource1.Value = this.MONHOCBindingSource;
            reportDataSource2.Name = "De_Thi";
            reportDataSource2.Value = this.DETHIBindingSource;
            reportDataSource3.Name = "Cau_Hoi";
            reportDataSource3.Value = this.CAUHOIBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Quan_Ly_Thi.GUI.Hoc_Sinh.Report_Viewer.DeOn.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(789, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // De_On
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "De_On";
            this.Text = "Đề Ôn";
            this.Load += new System.EventHandler(this.De_On_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MONHOCBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DETHIBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CAUHOIBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource MONHOCBindingSource;
        private System.Windows.Forms.BindingSource DETHIBindingSource;
        private System.Windows.Forms.BindingSource CAUHOIBindingSource;
    }
}