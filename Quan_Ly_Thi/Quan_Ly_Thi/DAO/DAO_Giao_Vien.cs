using Quan_Ly_Thi.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Thi.DAO
{
    public class DAO_Giao_Vien
    {

        static public List<MONHOC> layTatCaMonHoc()
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                return qlttn.MONHOCs.ToList<MONHOC>();
            }
        }

        static public List<KHOI> layTatCaKhoi()
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                return qlttn.KHOIs.ToList<KHOI>();
            }
        }

        static public List<DETHI> layTatCaDeThi()
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                return qlttn.DETHIs.ToList<DETHI>();
            }
        }

        static public List<KYTHI> layTatCaKiThi()
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                return qlttn.KYTHIs.ToList<KYTHI>();
            }
        }

        static public List<LOAICAUHOI> layTatCaLoaiCauHoi()
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                return qlttn.LOAICAUHOIs.ToList<LOAICAUHOI>();
            }
        }

        static public List<CHITIETKYTHI> layTatCaChiTietKiThi()
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                return qlttn.CHITIETKYTHIs.ToList<CHITIETKYTHI>();
            }
        }

        static public List<dynamic> layTatCaKetQua(string flag, string value)
        {
            List<dynamic> listKetQua = new List<dynamic>();
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                if (flag.Equals("HOCSINH"))
                {
                    listKetQua = qlttn.KETQUATHIs.Join(qlttn.KYTHIs, kqt => kqt.MaKyThi, kt => kt.MaKyThi,
                                              (kqt, kt) => new { kqt, kt })
                                              .Join(qlttn.DETHIs, kq => kq.kqt.MaDeThi, dt => dt.MaDeThi,
                                              (kq, dt) => new { kq, dt })
                                              .Join(qlttn.MONHOCs, kq => kq.dt.MaMonHoc, mh => mh.MaMonHoc,
                                              (kq, mh) => new { kq, mh })
                                              .Join(qlttn.KHOIs, kq => kq.kq.dt.MaKhoi, k => k.MaKhoi,
                                              (kq, k) => new { kq, k })
                                              .Join(qlttn.LOAIKYTHIs, kq => kq.kq.kq.kq.kt.MaLoaiKyThi, lkt => lkt.MaLoaiKyThi,
                                              (kq, lkt) => new { kq, lkt })
                                              .Join(qlttn.NGUOIDUNGs, kq => kq.kq.kq.kq.kq.kqt.TaiKhoan, nd => nd.TaiKhoan,
                                              (kq, nd) => new { kq, nd })
                                              .Join(qlttn.LOPHOCs, kq => kq.nd.MaLop, lh => lh.MaLop,
                                              (kq, lh) => new { kq, lh })
                                              .Where(kq => kq.kq.nd.TaiKhoan.Equals(value) || kq.kq.nd.HoTen.Contains(value))
                                              .Select(kq => new
                                              {
                                                  kq.kq.kq.lkt.TenLoaiKyThi,
                                                  kq.kq.kq.kq.kq.kq.kq.kt.TenKyThi,
                                                  kq.kq.kq.kq.kq.kq.dt.MaDeThi,
                                                  kq.kq.kq.kq.kq.mh.TenMonHoc,
                                                  kq.kq.kq.kq.k.TenKhoi,
                                                  kq.kq.nd.HoTen,
                                                  kq.lh.TenLop,
                                                  kq.kq.kq.kq.kq.kq.kq.kqt.Diem
                                              }).Distinct().ToList<dynamic>();
                }
                else if (flag.Equals("LOAICAUHOI"))
                {
                    listKetQua = qlttn.KETQUATHIs.Join(qlttn.KYTHIs, kqt => kqt.MaKyThi, kt => kt.MaKyThi,
                                            (kqt, kt) => new { kqt, kt })
                                            .Join(qlttn.DETHIs, kq => kq.kqt.MaDeThi, dt => dt.MaDeThi,
                                            (kq, dt) => new { kq, dt })
                                            .Join(qlttn.CHITIETDETHIs, kq => kq.dt.MaDeThi, ctdt => ctdt.MaDeThi,
                                            (kq, ctdt) => new { kq, ctdt })
                                            .Join(qlttn.CAUHOIs, kq => kq.ctdt.MaCauHoi, ch => ch.MaCauHoi,
                                            (kq, ch) =>new { kq, ch})
                                            .Join(qlttn.LOAICAUHOIs, kq => kq.ch.MaLoaiCauHoi, lch => lch.MaLoaiCauHoi,
                                            (kq, lch) => new { kq, lch})
                                            .Where(kq => kq.lch.MaLoaiCauHoi.Equals(value))
                                            .Join(qlttn.MONHOCs, kq => kq.kq.kq.kq.dt.MaMonHoc, mh => mh.MaMonHoc,
                                            (kq, mh) => new { kq, mh })
                                            .Join(qlttn.KHOIs, kq => kq.kq.kq.kq.kq.dt.MaKhoi, k => k.MaKhoi,
                                            (kq, k) => new { kq, k })
                                            .Join(qlttn.LOAIKYTHIs, kq => kq.kq.kq.kq.kq.kq.kq.kt.MaLoaiKyThi, lkt => lkt.MaLoaiKyThi,
                                            (kq, lkt) => new { kq, lkt })
                                            .Join(qlttn.NGUOIDUNGs, kq => kq.kq.kq.kq.kq.kq.kq.kq.kqt.TaiKhoan, nd => nd.TaiKhoan,
                                            (kq, nd) => new { kq, nd })
                                            .Join(qlttn.LOPHOCs, kq => kq.nd.MaLop, lh => lh.MaLop,
                                            (kq, lh) => new { kq, lh })
                                            .Select(kq => new
                                            {
                                                
                                                kq.kq.kq.lkt.TenLoaiKyThi,
                                                kq.kq.kq.kq.kq.kq.kq.kq.kq.kq.kt.TenKyThi,
                                                kq.kq.kq.kq.kq.kq.kq.kq.kq.dt.MaDeThi,
                                                kq.kq.kq.kq.kq.mh.TenMonHoc,
                                                kq.kq.kq.kq.k.TenKhoi,
                                                kq.kq.nd.HoTen,
                                                kq.lh.TenLop,
                                                kq.kq.kq.kq.kq.kq.kq.kq.kq.kq.kqt.Diem
                                            }).Distinct().ToList<dynamic>();
                    
                }
                else
                {
                    listKetQua = qlttn.KETQUATHIs.Join(qlttn.KYTHIs, kqt => kqt.MaKyThi, kt => kt.MaKyThi,
                                            (kqt, kt) => new { kqt, kt })
                                            .Where(kq => kq.kqt.MaKyThi.Equals(value))
                                            .Join(qlttn.DETHIs, kq => kq.kqt.MaDeThi, dt => dt.MaDeThi,
                                            (kq, dt) => new { kq, dt })
                                            .Join(qlttn.MONHOCs, kq => kq.dt.MaMonHoc, mh => mh.MaMonHoc,
                                            (kq, mh) => new { kq, mh })
                                            .Join(qlttn.KHOIs, kq => kq.kq.dt.MaKhoi, k => k.MaKhoi,
                                            (kq, k) => new { kq, k })
                                            .Join(qlttn.LOAIKYTHIs, kq => kq.kq.kq.kq.kt.MaLoaiKyThi, lkt => lkt.MaLoaiKyThi,
                                            (kq, lkt) => new { kq, lkt })
                                            .Join(qlttn.NGUOIDUNGs, kq => kq.kq.kq.kq.kq.kqt.TaiKhoan, nd => nd.TaiKhoan,
                                            (kq, nd) => new { kq, nd })
                                            .Join(qlttn.LOPHOCs, kq => kq.nd.MaLop, lh => lh.MaLop,
                                            (kq, lh) => new { kq, lh })
                                            .Select(kq => new
                                            {
                                                kq.kq.kq.lkt.TenLoaiKyThi,
                                                kq.kq.kq.kq.kq.kq.kq.kt.TenKyThi,
                                                kq.kq.kq.kq.kq.kq.dt.MaDeThi,
                                                kq.kq.kq.kq.kq.mh.TenMonHoc,
                                                kq.kq.kq.kq.k.TenKhoi,
                                                kq.kq.nd.HoTen,
                                                kq.lh.TenLop,
                                                kq.kq.kq.kq.kq.kq.kq.kqt.Diem
                                            }).Distinct().ToList<dynamic>();
                }
               
            }
            return listKetQua;
        }

        static public bool themDeThiVaoDB(DETHI dt)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                try
                {
                    qlttn.DETHIs.InsertOnSubmit(dt);
                    qlttn.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
               
            }
        }

        static public bool capNhatDeThiVaoDB(DETHI dt)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                try
                {
                   var deThi = qlttn.DETHIs.Where(dtt => dtt.MaDeThi.Equals(dt.MaDeThi));
                   foreach(var dtt in deThi)
                   {
                        dtt.ThoiGianLamBai = dt.ThoiGianLamBai;
                        dtt.MaKhoi = dt.MaKhoi;
                        dtt.MaMonHoc = dt.MaMonHoc;
                   }
                    qlttn.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }

        static public bool xoaDeThiVaoDB(DETHI dt)
        {
            //xóa ở chi tiết đề thi trước
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                try
                {
                    //Xóa ở chi tiết đề thi
                    var chiTietDeThi = qlttn.CHITIETDETHIs.Where(ctdt => ctdt.MaDeThi.Equals(dt.MaDeThi));
                    foreach (var ctdt in chiTietDeThi)
                    {
                        qlttn.CHITIETDETHIs.DeleteOnSubmit(ctdt);
                    }


                    //Xóa ở đề thi
                    var deThi = qlttn.DETHIs.Where(dtt => dtt.MaDeThi.Equals(dt.MaDeThi));
                    foreach (var dtt in deThi)
                    {
                        qlttn.DETHIs.DeleteOnSubmit(dtt);
                        qlttn.SubmitChanges();
                    }
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }

        static public bool themChiTietDeThiVaoDB(CHITIETDETHI ctdt)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                try
                {
                    qlttn.CHITIETDETHIs.InsertOnSubmit(ctdt);
                    qlttn.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }

        static public bool xoaChiTietDeThiVaoDB(CHITIETDETHI ctdt)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                try
                {
                    var chiTietDeThi = qlttn.CHITIETDETHIs.Where(ctdtt => ctdtt.MaDeThi.Equals(ctdt.MaDeThi) && ctdtt.MaCauHoi.Equals(ctdt.MaCauHoi));
                    foreach (var ctdtt in chiTietDeThi)
                    {
                        qlttn.CHITIETDETHIs.DeleteOnSubmit(ctdtt);
                    }
                    qlttn.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }

        static public bool themKyThiVaoDB(KYTHI kt)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                try
                {
                    qlttn.KYTHIs.InsertOnSubmit(kt);
                    qlttn.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }

        static public bool capNhatKyThiVaoDB(KYTHI kt)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                try
                {
                    var kyThi = qlttn.KYTHIs.Where(ktt => ktt.MaKyThi.Equals(kt.MaKyThi));
                    foreach (var ktt in kyThi)
                    {
                        ktt.MaLoaiKyThi = kt.MaLoaiKyThi;
                        ktt.TenKyThi = kt.TenKyThi;
                    }
                    qlttn.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }

        static public bool xoaKyThiVaoDB(KYTHI kt)
        {
            //xóa ở chi tiết đề thi trước
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                try
                {
                    //Xóa ở chi tiết đề thi
                    var chiTietKyThi = qlttn.CHITIETKYTHIs.Where(ctkt => ctkt.MaKyThi.Equals(kt.MaKyThi));
                    foreach (var ctkt in chiTietKyThi)
                    {
                        qlttn.CHITIETKYTHIs.DeleteOnSubmit(ctkt);
                    }


                    //Xóa ở đề thi
                    var kyThi = qlttn.KYTHIs.Where(ktt => ktt.MaKyThi.Equals(kt.MaKyThi));
                    foreach (var ktt in kyThi)
                    {
                        qlttn.KYTHIs.DeleteOnSubmit(ktt);
                        qlttn.SubmitChanges();
                    }
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }

        static public bool themChiTietKyThiVaoDB(CHITIETKYTHI ctkt)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                try
                {
                    qlttn.CHITIETKYTHIs.InsertOnSubmit(ctkt);
                    qlttn.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }

        static public bool xoaChiTietKyThiVaoDB(CHITIETKYTHI ctkt)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                try
                {
                    var chiTietKyThi = qlttn.CHITIETKYTHIs.Where(ctktt => ctktt.MaDeThi.Equals(ctkt.MaDeThi) && ctktt.MaKyThi.Equals(ctkt.MaKyThi));
                    foreach (var ctktt in chiTietKyThi)
                    {
                        qlttn.CHITIETKYTHIs.DeleteOnSubmit(ctktt);
                    }
                    qlttn.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        static public bool capNhatChiTietKyThiVaoDB(CHITIETKYTHI ctkt)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                try
                {
                    var chiTietKyThi = qlttn.CHITIETKYTHIs.Where(ctktt => ctktt.MaDeThi.Equals(ctkt.MaDeThi) && ctktt.MaKyThi.Equals(ctkt.MaKyThi));
                    foreach (var ctktt in chiTietKyThi)
                    {
                        ctktt.ThoiGianBatDau = ctkt.ThoiGianBatDau;
                        ctktt.ThoiGianKetThuc = ctkt.ThoiGianKetThuc;
                    }
                    qlttn.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }

        static public bool capNhatNguoiDungVaoDB(NGUOIDUNG nd)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                try
                {
                    var nguoiDung = qlttn.NGUOIDUNGs.Where(ndd => ndd.TaiKhoan.Equals(nd.TaiKhoan));
                    foreach (var ndd in nguoiDung)
                    {
                        ndd.HoTen = nd.HoTen;
                        ndd.CMND_TCC = nd.CMND_TCC;
                        ndd.NgaySinh = nd.NgaySinh;
                        ndd.SoDienThoai = nd.SoDienThoai;
                        ndd.Email = nd.Email;
                    }
                    qlttn.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }

            }

        }



    }
}
