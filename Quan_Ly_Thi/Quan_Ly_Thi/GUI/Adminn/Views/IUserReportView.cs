using Quan_Ly_Thi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Thi.GUI.Adminn.Views
{
    public interface IUserReportView
    {
        IList<UserRevenue> DataSource { get; set; }
        string Decentralize { get; }
        event EventHandler RefreshReport;
    }
}
