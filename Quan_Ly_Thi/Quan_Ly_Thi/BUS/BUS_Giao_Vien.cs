using Quan_Ly_Thi.DAO;
using Quan_Ly_Thi.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Thi.BUS
{
    class BUS_Giao_Vien
    {
        static public List<MONHOC> layTatCaMonHoc()
        {
            return DAO_Giao_Vien.layTatCaMonHoc();
        }

        static public List<KHOI> layTatCaKhoi()
        {
            return DAO_Giao_Vien.layTatCaKhoi();
        }

        static public List<LOPHOC> layTatCaLopHoc()
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                return qlttn.LOPHOCs.ToList<LOPHOC>();
            }
        }

        static public List<DETHI> layTatCaDeThi()
        {
            return DAO_Giao_Vien.layTatCaDeThi();
        }

        static public List<LOAIKYTHI> layTatCaLoaiKyThi()
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                return qlttn.LOAIKYTHIs.ToList<LOAIKYTHI>();
            }
        }

        static public List<KYTHI> layTatCaKiThi()
        {
            return DAO_Giao_Vien.layTatCaKiThi();
        }

        static public IEnumerable<KYTHI> layTatCaKiThiDaHoanThanh()
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                //Lấy những kỳ thi mà có chi tiết kỳ thi chưa diễn ra
                List<KYTHI> listNgoaiTru1 = qlttn.CHITIETKYTHIs.Where(ctkt => ctkt.ThoiGianKetThuc >= DateTime.Now)
                                        .Join(qlttn.KYTHIs, kq => kq.MaKyThi, kt => kt.MaKyThi,
                                        (kq, kt) => new { kq, kt })
                                        .Select(kq => kq.kt).Distinct().ToList<KYTHI>();

                //Lấy ra những kỳ thi có chi tiết kỳ thi
                List<KYTHI> listKyThi = qlttn.CHITIETKYTHIs.Join(qlttn.KYTHIs, ctkt => ctkt.MaKyThi, kt => kt.MaKyThi,
                                                                (ctkt, kt) => new { ctkt, kt })
                                                                .Select(kq => kq.kt).Distinct().ToList<KYTHI>();


                return listKyThi.Except(listNgoaiTru1).ToList<KYTHI>();
            }
        }

        static public List<LOAICAUHOI> layTatCaLoaiCauHoi()
        {
            return DAO_Giao_Vien.layTatCaLoaiCauHoi();
        }

        static public List<dynamic> layTatCaKetQua(string flag, string value)
        {
            return DAO_Giao_Vien.layTatCaKetQua(flag, value);
        }

        static public List<CHITIETKYTHI> layTatCaChiTietKiThi()
        {
            return DAO_Giao_Vien.layTatCaChiTietKiThi();
        }

        static public string tuDongTangKhoaDeThi()
        {
            int ma = 1;
            if (DAO_Giao_Vien.layTatCaDeThi().Count > 0)
                ma = int.Parse(DAO_Giao_Vien.layTatCaDeThi().Last().MaDeThi.Substring(2)) + 1;
            string maDeThi = "DT" + ma.ToString("D6") + "  ";
            return maDeThi;
        }
       
        static public string tuDongTangKhoaKyThi()
        {         
                int ma = 1;
                if (DAO_Giao_Vien.layTatCaKiThi().Count > 0)               
                    ma = int.Parse(DAO_Giao_Vien.layTatCaKiThi().Last().MaKyThi.Substring(2)) + 1;                
                string maDeThi = "KT" + ma.ToString("D6") + "  ";
                return maDeThi;        
        }

        static public bool themDeThiVaoDB(DETHI dt)
        {
            return DAO_Giao_Vien.themDeThiVaoDB(dt);
        }

        static public bool capNhatDeThiVaoDB(DETHI dt)
        {
            return DAO_Giao_Vien.capNhatDeThiVaoDB(dt);
        }

        static public bool xoaDeThiVaoDB(DETHI dt)
        {
            return DAO_Giao_Vien.xoaDeThiVaoDB(dt);
        }

        static public bool themKyThiVaoDB(KYTHI kt)
        {
            return DAO_Giao_Vien.themKyThiVaoDB(kt);
        }

        static public bool capNhatKyThiVaoDB(KYTHI kt)
        {
            return DAO_Giao_Vien.capNhatKyThiVaoDB(kt);
        }

        static public bool xoaKyThiVaoDB(KYTHI kt)
        {
            return DAO_Giao_Vien.xoaKyThiVaoDB(kt);
        }

        static public bool themChiTietKyThiVaoDB(CHITIETKYTHI ctkt)
        {
            return DAO_Giao_Vien.themChiTietKyThiVaoDB(ctkt);
        }

        static public bool xoaChiTietKyThiVaoDB(CHITIETKYTHI ctkt)
        {
            return DAO_Giao_Vien.xoaChiTietKyThiVaoDB(ctkt);
        }

        static public bool capNhatChiTietKyThiVaoDB(CHITIETKYTHI ctkt)
        {
            return DAO_Giao_Vien.capNhatChiTietKyThiVaoDB(ctkt);
        }

        static public bool kyThiCoTrongKetQuaThi(string maKyThi)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                var list = qlttn.KETQUATHIs.Where(kqt => kqt.MaKyThi.Equals(maKyThi)).Select(kq => new { kq }).ToList();
                if (list.Count > 0) return true; //Có
                return false; //Không có

            }
        }

        static public bool chiTietKiThiCoTrongKetQuaThi(string maKyThi, string maDeThi)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                var list = qlttn.KETQUATHIs.Where(kqt => kqt.MaKyThi.Equals(maKyThi) && kqt.MaDeThi.Equals(maDeThi))
                                            .Select(kq => new { kq }).ToList();
                if (list.Count > 0) return true; //Có
                return false; //Không có

            }
        }

        static public bool deThiCoTrongKetQuaThi(string maDeThi)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                var list = qlttn.KETQUATHIs.Where(kqt => kqt.MaDeThi.Equals(maDeThi)).Select(kq => new { kq }).ToList();
                if (list.Count > 0) return true; //Có
                return false; //Không có

            }
        }

        static public bool deThiCoTrongChiTietDeThi (string maDeThi)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                var list = qlttn.CHITIETDETHIs.Where(ctdt => ctdt.MaDeThi.Equals(maDeThi)).Select(kq => new { kq }).ToList();
                if (list.Count > 0) return true; //Có
                return false; //Không

            }
        }

        static public bool themChiTietDeThiVaoDB(CHITIETDETHI ctdt)
        {
            return DAO_Giao_Vien.themChiTietDeThiVaoDB(ctdt);
        }

        static public bool xoaChiTietDeThiVaoDB(CHITIETDETHI ctdt)
        {
            return DAO_Giao_Vien.xoaChiTietDeThiVaoDB(ctdt);
        }

        static public List<dynamic> layTatCaCauHoiTheoLoaiCauHoi(string maLoaiCauHoi, string maMonHoc, string maKhoi)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                return qlttn.CAUHOIs.Where(ch => ch.MaLoaiCauHoi.Equals(maLoaiCauHoi) && ch.MaKhoi.Equals(maKhoi) && ch.MaMonHoc.Equals(maMonHoc))
                                    .Select(kq => new {kq.MaCauHoi, kq.CauHoi1, kq.CauA, kq.CauB, kq.CauC, kq.CauD}).ToList<dynamic>();
            }
        }

        static public List<dynamic> layTatCaCauHoiCuaDe(string maDeThi)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                return qlttn.CAUHOIs.Join(qlttn.CHITIETDETHIs, ch => ch.MaCauHoi, ctdt => ctdt.MaCauHoi,
                                        (ch, ctdt) => new { ch, ctdt })
                                     .Where(kq => kq.ctdt.MaDeThi.Equals(maDeThi))
                                     .Select(kq => new {kq.ch.MaCauHoi, kq.ch.CauHoi1, kq.ch.CauA, kq.ch.CauB, kq.ch.CauC, kq.ch.CauD }).ToList<dynamic>();
            }           
        }

        static public List<DETHI> layTatCaDeThiTheoMaKyThiVaMonHocVaKhoi(KYTHI kyThi ,string maKhoi, string maMonhoc)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                List<DETHI> listDeTheoLoaiKyThi = layTatCaDeThiTheoLoaiKyThi(kyThi.MaLoaiKyThi);
                return listDeTheoLoaiKyThi.Where(ldt => ldt.MaMonHoc.Equals(maMonhoc) && ldt.MaKhoi.Equals(maKhoi)).ToList<DETHI>();                                     
            }
        }

        static public List<DanhSachKyThi> danhSachKyThi(string maKyThi)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                //return qlttn.CHITIETKYTHIs.Where(ctkt => ctkt.MaKyThi.Equals(maKyThi))
                //                            .Join(qlttn.KYTHIs, kq => kq.MaKyThi, kt => kt.MaKyThi,
                //                            (kq, kt) => new { kq, kt })
                //                            .Join(qlttn.DETHIs, kq => kq.kq.MaDeThi, dt => dt.MaDeThi,
                //                            (kq, dt) => new { kq, dt })
                //                            .Join(qlttn.MONHOCs, kq => kq.dt.MaMonHoc, mh => mh.MaMonHoc,
                //                            (kq, mh) => new { kq, mh })
                //                            .Join(qlttn.KHOIs, kq => kq.kq.dt.MaKhoi, k => k.MaKhoi,
                //                            (kq, k) => new { kq, k })
                //                            .Select(kq => new
                //                            {
                //                                kq.kq.kq.kq.kt.MaKyThi,
                //                                kq.kq.kq.kq.kt.TenKyThi,
                //                                kq.k.MaKhoi,
                //                                kq.kq.mh.MaMonHoc,
                //                                kq.k.TenKhoi,
                //                                kq.kq.mh.TenMonHoc,
                //                                kq.kq.kq.kq.kq.ThoiGianBatDau,
                //                                kq.kq.kq.dt.MaDeThi,
                //                                kq.kq.kq.dt.ThoiGianLamBai
                //                            }).ToList<dynamic>();


                var list = qlttn.CHITIETKYTHIs.Where(ctkt => ctkt.MaKyThi.Equals(maKyThi))
                                            .Join(qlttn.KYTHIs, kq => kq.MaKyThi, kt => kt.MaKyThi,
                                            (kq, kt) => new { kq, kt })
                                            .Join(qlttn.DETHIs, kq => kq.kq.MaDeThi, dt => dt.MaDeThi,
                                            (kq, dt) => new { kq, dt })
                                            .Join(qlttn.MONHOCs, kq => kq.dt.MaMonHoc, mh => mh.MaMonHoc,
                                            (kq, mh) => new { kq, mh })
                                            .Join(qlttn.KHOIs, kq => kq.kq.dt.MaKhoi, k => k.MaKhoi,
                                            (kq, k) => new { kq, k })
                                            .Select(kq => new
                                            {
                                                kq.kq.kq.kq.kt.MaKyThi,
                                                kq.kq.kq.kq.kt.TenKyThi,
                                                kq.k.MaKhoi,
                                                kq.kq.mh.MaMonHoc,
                                                kq.k.TenKhoi,
                                                kq.kq.mh.TenMonHoc,
                                                kq.kq.kq.kq.kq.ThoiGianBatDau,
                                                kq.kq.kq.dt.MaDeThi,
                                                kq.kq.kq.dt.ThoiGianLamBai
                                            }).ToList();

                List<DanhSachKyThi> danhSachKyThi = new List<DanhSachKyThi>();
                foreach (var l in list)
                {
                    DanhSachKyThi dskt = new DanhSachKyThi();
                    dskt.MaKyThi = l.MaKyThi;
                    dskt.TenKyThi = l.TenKyThi;
                    dskt.MaDeThi = l.MaDeThi;
                    dskt.MaMonHoc = l.MaMonHoc;
                    dskt.MaKhoi = l.MaKhoi;
                    dskt.TenKhoi = l.TenKhoi;
                    dskt.TenMonHoc = l.TenMonHoc;
                    dskt.ThoiGianBatDau = l.ThoiGianBatDau.Value;
                    dskt.ThoiGianLamBai = l.ThoiGianLamBai.Value;
                    danhSachKyThi.Add(dskt);
                }
                return danhSachKyThi;

            }
        }

        static public KYTHI layThongTinKyThi(string maKyThi)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                return qlttn.KYTHIs.Where(kt => kt.MaKyThi.Equals(maKyThi)).Single();
            }
        }

        static public List<DETHI> layTatCaDeThiTheoLoaiKyThi(string maLoaiKyThi)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                if (maLoaiKyThi.Equals("LKT000001 ")) //nếu là kỳ thi chính thức
                    return qlttn.DETHIs.Join(qlttn.CHITIETDETHIs, dt => dt.MaDeThi, ctdt => ctdt.MaDeThi,
                                        (dt, ctdt) => new { dt, ctdt })
                                        .Join(qlttn.CAUHOIs, kq => kq.ctdt.MaCauHoi, ch => ch.MaCauHoi,
                                        (kq, ch) => new { kq, ch })
                                        .Where(kq => kq.ch.MaLoaiCauHoi.Equals("LCH000001 ")) //Loại câu hỏi chính thức
                                        .Select(kq => kq.kq.dt).ToList<DETHI>();
                else if (maLoaiKyThi.Equals("LKT000002 ")) //nếu là kỳ thi thử
                    return qlttn.DETHIs.Join(qlttn.CHITIETDETHIs, dt => dt.MaDeThi, ctdt => ctdt.MaDeThi,
                                       (dt, ctdt) => new { dt, ctdt })
                                       .Join(qlttn.CAUHOIs, kq => kq.ctdt.MaCauHoi, ch => ch.MaCauHoi,
                                       (kq, ch) => new { kq, ch })
                                       .Where(kq => kq.ch.MaLoaiCauHoi.Equals("LCH000001 ") || kq.ch.MaLoaiCauHoi.Equals("LCH000002 ")) //Loại câu hỏi chính thức hoặc ôn luyện
                                       .Select(kq => kq.kq.dt).ToList<DETHI>();
                else
                    return qlttn.DETHIs.Join(qlttn.CHITIETDETHIs, dt => dt.MaDeThi, ctdt => ctdt.MaDeThi,
                                      (dt, ctdt) => new { dt, ctdt })
                                      .Join(qlttn.CAUHOIs, kq => kq.ctdt.MaCauHoi, ch => ch.MaCauHoi,
                                      (kq, ch) => new { kq, ch })
                                      .Where(kq => kq.ch.MaLoaiCauHoi.Equals("LCH000002 ")) //Loại câu hỏi ôn luyện
                                      .Select(kq => kq.kq.dt).ToList<DETHI>();
            }
        }

        static public DETHI layThongTinDeThi(string maDeThi)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                return qlttn.DETHIs.Where(dt => dt.MaDeThi.Equals(maDeThi)).Single();
            }
        }

        static public List<DETHI> layThongTinDeThiTheoKyThi(string maKyThi)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                return qlttn.CHITIETKYTHIs.Where(ctkt => ctkt.MaKyThi.Equals(maKyThi))
                            .Join(qlttn.DETHIs, kq => kq.MaDeThi, dt => dt.MaDeThi,
                            (kq, dt) => new { kq, dt })
                            .Select(kq => kq.dt).ToList<DETHI>();
            }
        }

        static public CHITIETKYTHI layThongTinChiTietKyThi(string maKyThi ,string maDeThi)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                return qlttn.CHITIETKYTHIs.Where(ctkt => ctkt.MaKyThi.Equals(maKyThi) && ctkt.MaDeThi.Equals(maDeThi)).Single();
            }
        }

        static public List<dynamic> danhSachThiSinh(string maKyThi, string maKhoi, string maMonHoc)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                return qlttn.CHITIETKYTHIs.Where(ctkt => ctkt.MaKyThi.Equals(maKyThi))
                                            .Join(qlttn.DETHIs, kq => kq.MaDeThi, dt => dt.MaDeThi,
                                            (kq, dt) => new { kq, dt })
                                            .Where(kq => kq.dt.MaKhoi.Equals(maKhoi) && kq.dt.MaMonHoc.Equals(maMonHoc))
                                            .Join(qlttn.LOPHOCs, kq => kq.dt.MaKhoi, lh => lh.MaKhoi,
                                            (kq, lh) => new { kq, lh })
                                            .Join(qlttn.NGUOIDUNGs, kq => kq.lh.MaLop, nd => nd.MaLop,
                                            (kq, nd) => new { kq, nd })
                                            .Select(kq => new {kq.nd.TaiKhoan, kq.nd.HoTen, kq.nd.NgaySinh, kq.kq.lh.TenLop}).ToList<dynamic>();
            }
        }

        static public List<ReportDanhSachThiSinh> reportDanhSachThiSinh(string maKyThi)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                var list = qlttn.CHITIETKYTHIs.Where(ctkt => ctkt.MaKyThi.Equals(maKyThi))
                                            .Join(qlttn.DETHIs, kq => kq.MaDeThi, dt => dt.MaDeThi,
                                            (kq, dt) => new { kq, dt })
                                            .Join(qlttn.LOPHOCs, kq => kq.dt.MaKhoi, lh => lh.MaKhoi,
                                            (kq, lh) => new { kq, lh })
                                            .Join(qlttn.NGUOIDUNGs, kq => kq.lh.MaLop, nd => nd.MaLop,
                                            (kq, nd) => new { kq, nd })                                            
                                            .Select(kq => new {kq.nd.TaiKhoan, kq.nd.HoTen, kq.nd.NgaySinh, kq.kq.lh.TenLop })
                                            .Distinct()
                                            .ToList();
                List<ReportDanhSachThiSinh> ds = new List<ReportDanhSachThiSinh>();
                foreach(var l in list)
                {
                    ReportDanhSachThiSinh dsts = new ReportDanhSachThiSinh();
                    dsts.TaiKhoan = l.TaiKhoan;
                    dsts.HoTen = l.HoTen;
                    dsts.NgaySinh = l.NgaySinh.Value.Day.ToString("D2") + "/" + l.NgaySinh.Value.Month.ToString("D2") + "/"  + l.NgaySinh.Value.Year.ToString();
                    dsts.TenLop = l.TenLop;
                    ds.Add(dsts);
                }
                return ds;
            }
        }

        static public List<ReportTongKetKyThi> reportTongKetKyThi(string maKyThi)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                var list = qlttn.KETQUATHIs.Where(kqt => kqt.MaKyThi.Equals(maKyThi))
                                            .Join(qlttn.DETHIs, kqt => kqt.MaDeThi, dt => dt.MaDeThi,
                                            (kqt, dt) => new { kqt, dt })
                                            .Join(qlttn.MONHOCs, kq => kq.dt.MaMonHoc, mh => mh.MaMonHoc,
                                            (kq, mh) => new { kq, mh })
                                            .Join(qlttn.NGUOIDUNGs, kq => kq.kq.kqt.TaiKhoan, nd => nd.TaiKhoan,
                                            (kq, nd) => new { kq, nd })
                                            .Join(qlttn.LOPHOCs, kq => kq.nd.MaLop, lh => lh.MaLop,
                                            (kq, lh) => new { kq, lh })
                                            .Select(kq => new { kq.lh.TenLop, kq.kq.nd.TaiKhoan, kq.kq.nd.HoTen, kq.kq.nd.NgaySinh, kq.kq.kq.mh.TenMonHoc, kq.kq.kq.kq.kqt.Diem })
                                            .Distinct()
                                            .ToList();
                List<ReportTongKetKyThi> ds = new List<ReportTongKetKyThi>();
                foreach (var l in list)
                {
                    ReportTongKetKyThi dsts = new ReportTongKetKyThi();
                    dsts.TaiKhoan = l.TaiKhoan;
                    dsts.HoTen = l.HoTen;
                    dsts.NgaySinh = l.NgaySinh.Value.Day.ToString("D2") + "/" + l.NgaySinh.Value.Month.ToString("D2") + "/" + l.NgaySinh.Value.Year.ToString();
                    dsts.TenLop = l.TenLop;
                    dsts.Diem = float.Parse(l.Diem.Value.ToString());
                    dsts.MonHoc = l.TenMonHoc;
                    ds.Add(dsts);
                }
                return ds;
            }
        }

        static public string layTenKhoiTuongUng(string maKhoi)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                return qlttn.KHOIs.Where(kh => kh.MaKhoi.Equals(maKhoi))
                                    .Select(kh => new { kh.TenKhoi }).ToString();
            }
        }

        static public string layTenPhanQuyenTuongUng(string maPhanQuyen)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                return qlttn.PHANQUYENs.Where(kh => kh.MaPhanQuyen.Equals(maPhanQuyen))
                                    .Select(kh => new { kh.TenPhanQuyen }).ToString();
            }
        }

        static public bool capNhatNguoiDungVaoDB(NGUOIDUNG nd)
        {
            return DAO_Giao_Vien.capNhatNguoiDungVaoDB(nd);
        }

        static public List<dynamic> danhSachHocSinh(string maLop)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                return qlttn.NGUOIDUNGs.Where(nd => nd.MaPhanQuyen.Contains("HS") && nd.MaLop.Equals(maLop))  
                                            .Select(kq => new { kq.TaiKhoan, kq.HoTen, kq.NgaySinh, kq.CMND_TCC, kq.Email}).ToList<dynamic>();
            }
        }

        static public List<dynamic> danhSachHocSinhTheoThongTinHocSinh(string maLop, string value)
        {
            using (QLTTNDataContext qlttn = new QLTTNDataContext())
            {
                return qlttn.NGUOIDUNGs.Where(nd => nd.MaPhanQuyen.Contains("HS") && nd.MaLop.Equals(maLop)
                                                        && (nd.TaiKhoan.Equals(value) || nd.HoTen.Contains(value)))
                                            .Select(kq => new { kq.TaiKhoan, kq.HoTen, kq.NgaySinh, kq.CMND_TCC, kq.Email }).ToList<dynamic>();
            }
        }
    }
}
