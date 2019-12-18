using Microsoft.Reporting.WinForms;
using Quan_Ly_Thi.DAO;
using Quan_Ly_Thi.DTO;
using Quan_Ly_Thi.GUI.Adminn.Models;
using Quan_Ly_Thi.GUI.Adminn.Presenters;
using Quan_Ly_Thi.GUI.Adminn.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Thi.GUI.Adminn
{
    public partial class frmUserReport : Form, IUserReportView
    {
        List<Decentralization> listDecen = null;
        BindingSource UserRevenueBS;
        ReportDataSource UserReveueRPDS;
        const string RPDSName = "Users";
        MainPresent Presenter;

        public event EventHandler RefreshReport;

        public frmUserReport()
        {
            InitializeComponent();
            btnRefresh.Click += BtnRefresh_Click;
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshReport?.Invoke(sender, e);
        }

        public IList<UserRevenue> DataSource 
        {
            get => UserRevenueBS.DataSource as IList<UserRevenue>; 
            set
            {
                UserRevenueBS.DataSource = value;
                reportUsers.RefreshReport();
            }
        }

        public string Decentralize => listDecen[cbDecentralize.SelectedIndex].Decen;

        private void frmUserReport_Load(object sender, EventArgs e)
        {
            listDecen = new List<Decentralization>()
            {
                new Decentralization()
                {
                    Decen = "HS"
                },
                new Decentralization()
                {
                    Decen = "GV"
                },
                new Decentralization()
                {
                    Decen = "AD"
                }
            };
            cbDecentralize.DataSource = listDecen;

            Presenter = new MainPresent(this);
            reportUsers.LocalReport.DataSources.Clear();
            UserRevenueBS = new BindingSource();
            UserReveueRPDS = new ReportDataSource("Users");
            UserReveueRPDS.Value = UserRevenueBS;
            reportUsers.LocalReport.DataSources.Add(UserReveueRPDS);
            
            this.reportUsers.RefreshReport();
        }
    }
}
