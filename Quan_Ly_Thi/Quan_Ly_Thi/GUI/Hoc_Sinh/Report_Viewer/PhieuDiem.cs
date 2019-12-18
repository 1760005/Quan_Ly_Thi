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

namespace Quan_Ly_Thi.GUI.Hoc_Sinh.Report_Viewer
{
    public partial class PhieuDiem : Form
    {
        public static string TaiKhoan_;
        public static string _ma_ky_thi_;
        public PhieuDiem()
        {
        //    TaiKhoan_ = "TK000009";
        //    _ma_ky_thi_ = "KT000001  ";
            InitializeComponent();
        }

        private void PhieuDiem_Load(object sender, EventArgs e)
        {
            using (var QLTTN = new  QLTTNDataContext() )
            {
                QLTTN.DeferredLoadingEnabled = false;

                //Thông Tin
                var Querry = from ng_dung in QLTTN.NGUOIDUNGs
                             where ng_dung.TaiKhoan == TaiKhoan_
                             select ng_dung;
                
                NGUOIDUNGBindingSource.DataSource = Querry;

               
               var  Querry1 = from kythi in QLTTN.KYTHIs
                         where kythi.MaKyThi == _ma_ky_thi_
                         select kythi;

                KYTHIBindingSource.DataSource = Querry1;

                var Querry2 = from ket_qua in QLTTN.KETQUATHIs
                              join de in QLTTN.DETHIs on ket_qua.CHITIETKYTHI.MaDeThi equals de.MaDeThi
                              where ket_qua.TaiKhoan == TaiKhoan_ && ket_qua.CHITIETKYTHI.MaKyThi == _ma_ky_thi_
                              select new { ket_qua.Diem,de.MONHOC.MaMonHoc, de.MONHOC.TenMonHoc};

               KETQUATHIBindingSource.DataSource = Querry2.ToList();

                MONHOCBindingSource.DataSource = Querry2.ToList();
            }

            this.reportViewer1.RefreshReport();
        }
    }
}
