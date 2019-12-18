namespace Quan_Ly_Thi.GUI.Adminn
{
    partial class frmUserReport
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
            this.reportUsers = new Microsoft.Reporting.WinForms.ReportViewer();
            this.cbDecentralize = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.UserRevenueBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.UserRevenueBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportUsers
            // 
            this.reportUsers.Dock = System.Windows.Forms.DockStyle.Top;
            reportDataSource1.Name = "Users";
            reportDataSource1.Value = this.UserRevenueBindingSource;
            this.reportUsers.LocalReport.DataSources.Add(reportDataSource1);
            this.reportUsers.LocalReport.ReportEmbeddedResource = "Quan_Ly_Thi.GUI.Adminn.Reports.ReportUserRevenue.rdlc";
            this.reportUsers.Location = new System.Drawing.Point(0, 0);
            this.reportUsers.Name = "reportUsers";
            this.reportUsers.ServerReport.BearerToken = null;
            this.reportUsers.Size = new System.Drawing.Size(800, 331);
            this.reportUsers.TabIndex = 0;
            // 
            // cbDecentralize
            // 
            this.cbDecentralize.FormattingEnabled = true;
            this.cbDecentralize.Location = new System.Drawing.Point(229, 379);
            this.cbDecentralize.Name = "cbDecentralize";
            this.cbDecentralize.Size = new System.Drawing.Size(121, 21);
            this.cbDecentralize.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(479, 377);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // UserRevenueBindingSource
            // 
            this.UserRevenueBindingSource.DataSource = typeof(Quan_Ly_Thi.DTO.UserRevenue);
            // 
            // frmUserReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.cbDecentralize);
            this.Controls.Add(this.reportUsers);
            this.Name = "frmUserReport";
            this.Text = "frmUserReport";
            this.Load += new System.EventHandler(this.frmUserReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UserRevenueBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportUsers;
        private System.Windows.Forms.ComboBox cbDecentralize;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.BindingSource UserRevenueBindingSource;
    }
}