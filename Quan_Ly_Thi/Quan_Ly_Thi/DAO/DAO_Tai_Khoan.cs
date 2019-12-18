using Quan_Ly_Thi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Thi.DAO
{
    public class DAO_Tai_Khoan
    {

        public static Tai_khoan layThongTinTaiKhoan(string taiKhoan)
        {
            using (var qlttn = new QLTTNDataContext())
            {
                var thongTin = qlttn.NGUOIDUNGs.Where(nd => nd.TaiKhoan.Equals(taiKhoan))
                                                .Select(nd => new { nd }).Single();


                if (thongTin.nd.MaPhanQuyen.Equals("AD        "))
                {
                    Adminn ad = new Adminn();
                    ad.Ho_Ten = thongTin.nd.HoTen;
                    ad.Ngay_Sinh = thongTin.nd.NgaySinh.Value;
                    ad.Tai_Khoan = thongTin.nd.TaiKhoan;
                    ad.CMND_TCC = thongTin.nd.CMND_TCC;
                    return ad;
                }
                else if (thongTin.nd.MaPhanQuyen.Equals("GV        "))
                {
                    var thongTinn = qlttn.NGUOIDUNGs
                                                    .Join(qlttn.KHOIs, nd => nd.MaKhoi, k => k.MaKhoi,
                                                        (nd, k) => new { nd, k.TenKhoi })
                                                    .Where(nd => nd.nd.TaiKhoan.Equals(taiKhoan))
                                                    .Select(nd => new { nd }).Single();

                    Giao_Vienn gv = new Giao_Vienn();
                    gv.Tai_Khoan = thongTinn.nd.nd.TaiKhoan;
                    gv.Ho_Ten = thongTinn.nd.nd.HoTen;
                    gv.CMND_TCC = thongTinn.nd.nd.CMND_TCC;
                    gv.Ngay_Sinh = thongTinn.nd.nd.NgaySinh.Value;
                    gv.SDT = thongTinn.nd.nd.SoDienThoai;
                    gv.Email = thongTinn.nd.nd.Email;
                    gv.Khoi = thongTinn.nd.TenKhoi;
                    return gv;
                }
                else
                {
                    var thongTinn = qlttn.NGUOIDUNGs
                                                   .Join(qlttn.LOPHOCs, nd => nd.MaLop, lh => lh.MaLop,
                                                       (nd, lh) => new { nd, lh })
                                                   .Join(qlttn.KHOIs, nd => nd.lh.MaKhoi, k => k.MaKhoi,
                                                    (nd, k) => new { nd, k.TenKhoi })
                                                    .Where(nd => nd.nd.nd.TaiKhoan.Equals(taiKhoan))
                                                   .Select(nd => new { nd }).Single();

                    Hoc_Sinhh hs = new Hoc_Sinhh();
                    hs.Tai_Khoan = thongTinn.nd.nd.nd.TaiKhoan;
                    hs.Ho_Ten = thongTinn.nd.nd.nd.HoTen;
                    hs.CMND_TCC = thongTinn.nd.nd.nd.CMND_TCC;
                    hs.Ngay_Sinh = thongTinn.nd.nd.nd.NgaySinh.Value;
                    hs.SDT = thongTinn.nd.nd.nd.SoDienThoai;
                    hs.Email = thongTinn.nd.nd.nd.Email;
                    hs.Khoi = thongTinn.nd.TenKhoi;
                    hs.Lop = thongTinn.nd.nd.lh.TenLop;
                    return hs;
                }
            }
        }

        public static string Quyen(string TaiKhoan)
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from q in QLTTN.NGUOIDUNGs
                             where q.TaiKhoan == TaiKhoan
                             select new { q.MaPhanQuyen };

                return Querry.First().MaPhanQuyen;
            }
        }

        public static void Them_Nguoi_Dung(NGUOIDUNG nguoi_dung)
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                QLTTN.NGUOIDUNGs.InsertOnSubmit(nguoi_dung);
                QLTTN.SubmitChanges();
            }
        }

        public static string ID_Lop(string Ten_lop)
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from id in QLTTN.LOPHOCs
                             where id.TenLop == Ten_lop
                             select new { id.MaLop };
                return Querry.First().MaLop;
            }
        }

        public static string ID_Khoi(string Ten_lop)
       {
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from id in QLTTN.LOPHOCs
                             where id.TenLop == Ten_lop
                             select new { id.MaKhoi };
                return Querry.First().MaKhoi;
            }
        }

        public static string ID_Quyen(string Ten_Quyen)
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from id in QLTTN.PHANQUYENs
                             where id.TenPhanQuyen == Ten_Quyen
                             select new { id.MaPhanQuyen };
                return Querry.First().MaPhanQuyen;
            }
        }

        public static void Doi_Mat_Khau(string Tai_khoan, string MatKhau_moi)
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from p in QLTTN.NGUOIDUNGs
                             where p.TaiKhoan == Tai_khoan
                             select p;
                Querry.First().MatKhau = MatKhau_moi;
                QLTTN.SubmitChanges();
            }

        }
    }
}
