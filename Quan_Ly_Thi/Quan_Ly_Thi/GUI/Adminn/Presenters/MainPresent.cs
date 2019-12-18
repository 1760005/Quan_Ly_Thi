using Quan_Ly_Thi.DAO;
using Quan_Ly_Thi.DTO;
using Quan_Ly_Thi.GUI.Adminn.Views;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Thi.GUI.Adminn.Presenters
{
    public class MainPresent
    {
        IUserReportView View;

        public MainPresent(IUserReportView TempView)
        {
            View = TempView;
            Init();
        }

        void Init()
        {
            View.RefreshReport += View_RefreshReport;
        }

        private void View_RefreshReport(object sender, EventArgs e)
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                var lo = new DataLoadOptions();
                lo.LoadWith<NGUOIDUNG>(nd => nd.TaiKhoan);
                QLTTN.LoadOptions = lo;
                View.DataSource = QLTTN.NGUOIDUNGs.Where(nd => nd.MaPhanQuyen.Equals(View.Decentralize)).Select(nd => new UserRevenue { UserAccount = nd.TaiKhoan, userPassword = nd.MatKhau, ClassID = nd.MaLop, GradeID = nd.MaKhoi, Decen = nd.MaPhanQuyen, DOB = Convert.ToDateTime(nd.NgaySinh), Email = nd.Email, PhoneNum = nd.SoDienThoai, userID = nd.CMND_TCC, UserName = nd.HoTen }).ToList();
            }

        }
    }
}
