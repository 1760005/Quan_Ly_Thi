using Quan_Ly_Thi.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Thi.DAO
{
    public class DAO_Admin
    {
        
        public static void BackupDataBase(string Path, DbProviderFactory factory, DbDataAdapter data, DbConnection connection)
        {
            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();
            data.SelectCommand.CommandText = @"backup database QuanLyThiTracNghiemDB to disk = '" + Path + "QLTTN.bak' with init";
            data.SelectCommand.Connection = connection;
            data.SelectCommand.ExecuteNonQuery();
        }

        public static void RestoreDataBase(string Path, DbProviderFactory factory, DbDataAdapter data, DbConnection connection)
        {
            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();
            data.SelectCommand.CommandText = @"restore database QuanLyThiTracNghiemDB from disk = '" + Path + "QLTTN.bak' with recovery";
            data.SelectCommand.Connection = connection;
            data.SelectCommand.ExecuteNonQuery();
        }

        public static List<Giao_Vienn> layDanhSachGiaoVien()
        {
            using (var qlttn = new QLTTNDataContext())
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
            using (var qlttn = new QLTTNDataContext())
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

        public static void InsertStudentWithExcel(Microsoft.Office.Interop.Excel.Range xlRange, int row)
        {
            using (QLTTNDataContext dataContext = new QLTTNDataContext())
            {
                int column = 1;
                NGUOIDUNG newUser = new NGUOIDUNG()
                {
                    TaiKhoan = xlRange.Cells[row, column].Text,
                    MatKhau = xlRange.Cells[row, column].Text,
                    MaKhoi = xlRange.Cells[row, column + 1].Text,
                    MaLop = xlRange.Cells[row, column + 2].Text,
                    HoTen = xlRange.Cells[row, column + 3].Text,
                    CMND_TCC = xlRange.Cells[row, column + 4].Text,
                    NgaySinh = DateTime.Parse(xlRange.Cells[row, column + 5].Text),
                    SoDienThoai = xlRange.Cells[row, column + 6].Text,
                    Email = xlRange.Cells[row, column + 7].Text,
                    MaPhanQuyen = "HS"
                };

                dataContext.NGUOIDUNGs.InsertOnSubmit(newUser);
                dataContext.SubmitChanges();
            }
        }

        public static void InsertTeacherWithExcel(Microsoft.Office.Interop.Excel.Range xlRange, int row)
        {
            using (QLTTNDataContext dataContext = new QLTTNDataContext())
            {
                int column = 1;
                NGUOIDUNG newUser = new NGUOIDUNG()
                {
                    TaiKhoan = xlRange.Cells[row, column].Text,
                    MatKhau = xlRange.Cells[row, column].Text,
                    MaKhoi = xlRange.Cells[row, column + 1].Text,
                    MaLop = xlRange.Cells[row, column + 2].Text,
                    HoTen = xlRange.Cells[row, column + 3].Text,
                    CMND_TCC = xlRange.Cells[row, column + 4].Text,
                    NgaySinh = DateTime.Parse(xlRange.Cells[row, column + 5].Text),
                    SoDienThoai = xlRange.Cells[row, column + 6].Text,
                    Email = xlRange.Cells[row, column + 7].Text,
                    MaPhanQuyen = "GV"
                };

                dataContext.NGUOIDUNGs.InsertOnSubmit(newUser);
                dataContext.SubmitChanges();
            }
        }

        public static void InsertStudentWithLinq(Hoc_Sinhh Student)
        {
            using (QLTTNDataContext dataContext = new QLTTNDataContext())
            {
                NGUOIDUNG newUser = new NGUOIDUNG()
                {
                    TaiKhoan = Student.Tai_Khoan,
                    MatKhau = Student.Mat_Khau,
                    MaKhoi = Student.Khoi,
                    MaLop = Student.Lop,
                    HoTen = Student.Ho_Ten,
                    CMND_TCC = Student.CMND_TCC,
                    NgaySinh = Student.Ngay_Sinh,
                    SoDienThoai = Student.SDT,
                    Email = Student.Email,
                    MaPhanQuyen = "HS"
                };

                dataContext.NGUOIDUNGs.InsertOnSubmit(newUser);
                dataContext.SubmitChanges();
            }
        }

        public static void InsertTeacherWithLinq(Giao_Vienn Teacher)
        {
            using (QLTTNDataContext dataContext = new QLTTNDataContext())
            {
                NGUOIDUNG newUser = new NGUOIDUNG()
                {
                    TaiKhoan = Teacher.Tai_Khoan,
                    MatKhau = Teacher.Mat_Khau,
                    MaKhoi = Teacher.Khoi,
                    MaLop = Teacher.Lop,
                    HoTen = Teacher.Ho_Ten,
                    CMND_TCC = Teacher.CMND_TCC,
                    NgaySinh = Teacher.Ngay_Sinh,
                    SoDienThoai = Teacher.SDT,
                    Email = Teacher.Email,
                    MaPhanQuyen = "GV"
                };

                dataContext.NGUOIDUNGs.InsertOnSubmit(newUser);
                dataContext.SubmitChanges();
            }
        }

        public static List<Ket_Qua_Thi> GetExaminationResult()
        {
            using (QLTTNDataContext dataContext = new QLTTNDataContext())
            {
                var KetQuaThi = dataContext.KETQUATHIs.Join(dataContext.NGUOIDUNGs, kqt => kqt.TaiKhoan, nd => nd.TaiKhoan, (kqt, nd) => new { kqt, nd, nd.HoTen, kqt.Diem }).
                                                        Join(dataContext.LOPHOCs, kqt => kqt.nd.MaLop, k => k.MaLop, (kqt, k) => new { kqt, k.TenLop }).
                                                        Join(dataContext.DETHIs, kqt => kqt.kqt.kqt.MaDeThi, dt => dt.MaDeThi, (kqt, dt) => new { kqt, dt.MaMonHoc }).
                                                        Join(dataContext.MONHOCs, kqt => kqt.MaMonHoc, mh => mh.MaMonHoc, (kqt, mh) => new { kqt, mh.TenMonHoc }).
                                                        Join(dataContext.KYTHIs, kqt => kqt.kqt.kqt.kqt.kqt.MaKyThi, kt => kt.MaKyThi, (kqt, kt) => new { kqt, kt.TenKyThi, kt.MaLoaiKyThi }).
                                                        Join(dataContext.LOAIKYTHIs, kqt => kqt.MaLoaiKyThi, lkt => lkt.MaLoaiKyThi, (kqt, lkt) => new { kqt, lkt.TenLoaiKyThi }).
                                                        Where(kqt => kqt.kqt.kqt.kqt.kqt.kqt.nd.MaPhanQuyen.Equals("HS")).Select(kq => new { kq }).ToList();
                List<Ket_Qua_Thi> listKQT = new List<Ket_Qua_Thi>();
                foreach (var KQT in KetQuaThi)
                {
                    Ket_Qua_Thi kqt = new Ket_Qua_Thi()
                    {
                        TenHocSinh = KQT.kq.kqt.kqt.kqt.kqt.kqt.nd.HoTen,
                        Lop = KQT.kq.kqt.kqt.kqt.kqt.TenLop,
                        TenMonHoc = KQT.kq.kqt.kqt.TenMonHoc,
                        TenKiThi = KQT.kq.kqt.TenKyThi,
                        TenLoaiKiThi = KQT.kq.TenLoaiKyThi,
                        Diem = Convert.ToDouble(KQT.kq.kqt.kqt.kqt.kqt.kqt.kqt.Diem)
                    };
                    listKQT.Add(kqt);
                }

                return listKQT;
            }
        }

        public static List<Ket_Qua_Thi> GetExaminationResult(string TaiKhoan)
        {
            using (QLTTNDataContext dataContext = new QLTTNDataContext())
            {
                var KetQuaThi = dataContext.KETQUATHIs.Join(dataContext.NGUOIDUNGs, kqt => kqt.TaiKhoan, nd => nd.TaiKhoan, (kqt, nd) => new { kqt, nd, nd.HoTen, kqt.Diem }).
                                                        Join(dataContext.LOPHOCs, kqt => kqt.nd.MaLop, k => k.MaLop, (kqt, k) => new { kqt, k.TenLop }).
                                                        Join(dataContext.DETHIs, kqt => kqt.kqt.kqt.MaDeThi, dt => dt.MaDeThi, (kqt, dt) => new { kqt, dt.MaMonHoc }).
                                                        Join(dataContext.MONHOCs, kqt => kqt.MaMonHoc, mh => mh.MaMonHoc, (kqt, mh) => new { kqt, mh.TenMonHoc }).
                                                        Join(dataContext.KYTHIs, kqt => kqt.kqt.kqt.kqt.kqt.MaKyThi, kt => kt.MaKyThi, (kqt, kt) => new { kqt, kt.TenKyThi, kt.MaLoaiKyThi }).
                                                        Join(dataContext.LOAIKYTHIs, kqt => kqt.MaLoaiKyThi, lkt => lkt.MaLoaiKyThi, (kqt, lkt) => new { kqt, lkt.TenLoaiKyThi }).
                                                        Where(kqt => kqt.kqt.kqt.kqt.kqt.kqt.nd.MaPhanQuyen.Equals("HS")).Where(kqt => kqt.kqt.kqt.kqt.kqt.kqt.nd.TaiKhoan.Equals(TaiKhoan)).Select(kq => new { kq }).ToList();
                List<Ket_Qua_Thi> listKQT = new List<Ket_Qua_Thi>();
                foreach (var KQT in KetQuaThi)
                {
                    Ket_Qua_Thi kqt = new Ket_Qua_Thi()
                    {
                        TenHocSinh = KQT.kq.kqt.kqt.kqt.kqt.kqt.nd.HoTen,
                        Lop = KQT.kq.kqt.kqt.kqt.kqt.TenLop,
                        TenMonHoc = KQT.kq.kqt.kqt.TenMonHoc,
                        TenKiThi = KQT.kq.kqt.TenKyThi,
                        TenLoaiKiThi = KQT.kq.TenLoaiKyThi,
                        Diem = Convert.ToDouble(KQT.kq.kqt.kqt.kqt.kqt.kqt.kqt.Diem)
                    };
                    listKQT.Add(kqt);
                }

                return listKQT;
            }
        }

        public static List<Ket_Qua_Thi> GetExaminationResultForTeacher(string MaKhoi)
        {
            using (QLTTNDataContext dataContext = new QLTTNDataContext())
            {
                var KetQuaThi = dataContext.KETQUATHIs.Join(dataContext.NGUOIDUNGs, kqt => kqt.TaiKhoan, nd => nd.TaiKhoan, (kqt, nd) => new { kqt, nd, nd.HoTen, kqt.Diem }).
                                                        Join(dataContext.LOPHOCs, kqt => kqt.nd.MaLop, k => k.MaLop, (kqt, k) => new { kqt, k.TenLop }).
                                                        Join(dataContext.DETHIs, kqt => kqt.kqt.kqt.MaDeThi, dt => dt.MaDeThi, (kqt, dt) => new { kqt, dt.MaMonHoc }).
                                                        Join(dataContext.MONHOCs, kqt => kqt.MaMonHoc, mh => mh.MaMonHoc, (kqt, mh) => new { kqt, mh.TenMonHoc }).
                                                        Join(dataContext.KYTHIs, kqt => kqt.kqt.kqt.kqt.kqt.MaKyThi, kt => kt.MaKyThi, (kqt, kt) => new { kqt, kt.TenKyThi, kt.MaLoaiKyThi }).
                                                        Join(dataContext.LOAIKYTHIs, kqt => kqt.MaLoaiKyThi, lkt => lkt.MaLoaiKyThi, (kqt, lkt) => new { kqt, lkt.TenLoaiKyThi }).
                                                        Where(kqt => kqt.kqt.kqt.kqt.kqt.kqt.nd.MaPhanQuyen.Equals("HS")).Where(kqt => kqt.kqt.kqt.kqt.kqt.kqt.nd.MaKhoi.Equals(MaKhoi)).Select(kq => new { kq }).ToList();
                List<Ket_Qua_Thi> listKQT = new List<Ket_Qua_Thi>();
                foreach (var KQT in KetQuaThi)
                {
                    Ket_Qua_Thi kqt = new Ket_Qua_Thi()
                    {
                        TenHocSinh = KQT.kq.kqt.kqt.kqt.kqt.kqt.nd.HoTen,
                        Lop = KQT.kq.kqt.kqt.kqt.kqt.TenLop,
                        TenMonHoc = KQT.kq.kqt.kqt.TenMonHoc,
                        TenKiThi = KQT.kq.kqt.TenKyThi,
                        TenLoaiKiThi = KQT.kq.TenLoaiKyThi,
                        Diem = Convert.ToDouble(KQT.kq.kqt.kqt.kqt.kqt.kqt.kqt.Diem)
                    };
                    listKQT.Add(kqt);
                }

                return listKQT;
            }
        }

        public static void DeleteUserWithLinq(string taiKhoan)
        {
            using (var dataContext = new QLTTNDataContext())
            {
                NGUOIDUNG Student = dataContext.NGUOIDUNGs.Where(nd => nd.TaiKhoan.Equals(taiKhoan)).Select(nd => nd).Single();

                dataContext.NGUOIDUNGs.DeleteOnSubmit(Student);
                dataContext.SubmitChanges();
            }
        }
    }
}
