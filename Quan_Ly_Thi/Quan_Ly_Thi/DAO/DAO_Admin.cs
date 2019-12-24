using Quan_Ly_Thi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Thi.DAO
{
    public class DAO_Admin
    {
        public static List<Giao_Vienn> layDanhSachGiaoVien()
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                var thongTin = qlttn.NGUOIDUNGs.Join(qlttn.KHOIs, nd => nd.MaKhoi, k => k.MaKhoi,
                                                        (nd, k) => new { nd, k.TenKhoi })
                                                .Where(nd => nd.nd.MaPhanQuyen.Equals("GV"))
                                                .OrderBy(nd => nd.TenKhoi)
                                                .Select(nd => new { nd }).ToList();


                List<Giao_Vienn> giaoVienList = new List<Giao_Vienn>();
                foreach (var tt in thongTin)
                {
                    Giao_Vienn gv = new Giao_Vienn();
                    gv.Tai_Khoan = tt.nd.nd.TaiKhoan;
                    gv.Ho_Ten = tt.nd.nd.HoTen;
                    gv.CMND_TCC = tt.nd.nd.CMND_TCC;
                    gv.Ngay_Sinh = tt.nd.nd.NgaySinh.Value;
                    gv.SDT = tt.nd.nd.SoDienThoai;
                    gv.Email = tt.nd.nd.Email;
                    gv.Khoi = tt.nd.TenKhoi;
                    giaoVienList.Add(gv);
                }

                return giaoVienList;
            }
        }

        public static List<Hoc_Sinhh> layDanhSachHocSinh()
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                var thongTin = qlttn.NGUOIDUNGs.Join(qlttn.LOPHOCs, nd => nd.MaLop, lh => lh.MaLop,
                                                (nd, lh) => new { nd, lh })
                                                .Join(qlttn.KHOIs, nd => nd.lh.MaKhoi, k => k.MaKhoi,
                                                (nd, k) => new { nd, k.TenKhoi })
                                                .Where(nd => nd.nd.nd.MaPhanQuyen.Equals("HS"))
                                                .OrderBy(nd => nd.nd.lh.TenLop)
                                                .Select(nd => new { nd }).ToList();


                List<Hoc_Sinhh> hocSinhList = new List<Hoc_Sinhh>();
                foreach (var tt in thongTin)
                {
                    Hoc_Sinhh hs = new Hoc_Sinhh();
                    hs.Tai_Khoan = tt.nd.nd.nd.TaiKhoan;
                    hs.Ho_Ten = tt.nd.nd.nd.HoTen;
                    hs.CMND_TCC = tt.nd.nd.nd.CMND_TCC;
                    hs.Ngay_Sinh = tt.nd.nd.nd.NgaySinh.Value;
                    hs.SDT = tt.nd.nd.nd.SoDienThoai;
                    hs.Email = tt.nd.nd.nd.Email;
                    hs.Khoi = tt.nd.TenKhoi;
                    hs.Lop = tt.nd.nd.lh.TenLop;
                    hocSinhList.Add(hs);
                }

                return hocSinhList;
            }

        }
    }
}
