using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quan_Ly_Thi.DAO;
using Quan_Ly_Thi.BUS;
namespace Quan_Ly_Thi.GUI.Hoc_Sinh.Report_Viewer
{
    public partial class De_On : Form
    {
        public static string _ma_khoi_;
        public static string _do_kho_;
        public static string _ma_mon_hoc_;

        public De_On()
        {
           
            InitializeComponent();
        }

        private void De_On_Load(object sender, EventArgs e)
        {
            using (var QLTTN  = new QLTTNDataContext())
            {
                QLTTN.DeferredLoadingEnabled = false;

                var Querry = from ch in QLTTN.CAUHOIs
                             where ch.MaKhoi == _ma_khoi_
                             && ch.MaCapDo == _do_kho_
                             && ch.MaMonHoc == _ma_mon_hoc_
                             select ch;

                CAUHOIBindingSource.DataSource = Querry;
            }
            this.reportViewer1.RefreshReport();
        }
    }
}
