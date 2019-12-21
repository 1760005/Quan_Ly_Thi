using Quan_Ly_Thi.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Thi.DAO
{
    public class DAO_Admin
    {
        
        public static void BackupDataBase(string Path, ConnectionStringSettings conStrSettings)
        {
            if (Path != null)
            {
                DbConnection Connection;
                DbProviderFactory factory;
                DbDataAdapter data;
                

                factory = DbProviderFactories.GetFactory(conStrSettings.ProviderName);
                Connection = factory.CreateConnection();
                Connection.ConnectionString = conStrSettings.ConnectionString;
                Connection.Open();

                data = factory.CreateDataAdapter();
                data.SelectCommand = factory.CreateCommand();
                data.SelectCommand.CommandText = @"backup database QuanLyThiTracNghiemDB to disk = '" + Path + "QLTTN.bak' with init";
                data.SelectCommand.Connection = Connection;
                data.SelectCommand.ExecuteNonQuery();
            }
        }

        public static void RestoreDataBase(string Path, ConnectionStringSettings conStrSettings)
        {
            if (Path != null)
            {
                DbConnection Connection;
                DbProviderFactory factory;
                DbDataAdapter data;
                

                factory = DbProviderFactories.GetFactory(conStrSettings.ProviderName);
                Connection = factory.CreateConnection();
                Connection.ConnectionString = conStrSettings.ConnectionString;
                Connection.Open();

                data = factory.CreateDataAdapter();
                data.SelectCommand = factory.CreateCommand();
                data.SelectCommand.CommandText = @"use [master]";
                data.SelectCommand.Connection = Connection;
                data.SelectCommand.ExecuteNonQuery();

                data = factory.CreateDataAdapter();
                data.SelectCommand = factory.CreateCommand();
                data.SelectCommand.CommandText = @"restore database QuanLyThiTracNghiemDB from disk = '" + Path + "' with replace, stats = 5";
                data.SelectCommand.Connection = Connection;
                data.SelectCommand.ExecuteNonQuery();
            }
        }

        public static void GetListOfTeacher(DataGridView gridView, ConnectionStringSettings conStrSettings, Label lbPage_student, ref int n, ref int Page, ref int Count, ref int NOP)
        {
            Page = 0;
            Count = 0;

            DbConnection Connection;
            DbProviderFactory factory;
            DbDataAdapter data;
            DataTable TchTable = new DataTable();
            DataTable TchTableLast = new DataTable();

            factory = DbProviderFactories.GetFactory(conStrSettings.ProviderName);
            Connection = factory.CreateConnection();
            Connection.ConnectionString = conStrSettings.ConnectionString;

            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();
            data.SelectCommand.CommandText = @"select nd.*, k.TenKhoi from NGUOIDUNG nd JOIN KHOI k on k.MaKhoi = nd.MaKhoi where nd.MaPhanQuyen = 'GV'";
            data.SelectCommand.Connection = Connection;
            var crbuilder = factory.CreateCommandBuilder();
            crbuilder.DataAdapter = data;

            data.Fill(TchTable);
            n = TchTable.Rows.Count;

            NOP = n / 10 + (102 % 10 > 0 ? 1 : 0);
            lbPage_student.Text = "1/" + NOP.ToString();

            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();
            data.SelectCommand.CommandText = @"SELECT * FROM (SELECT nd.*, k.TenKhoi, ROW_NUMBER() OVER (ORDER BY nd.MaKhoi) AS RowNum FROM NGUOIDUNG nd JOIN KHOI k on k.MaKhoi = nd.MaKhoi WHERE nd.MaPhanQuyen = 'GV') AS MyDerivedTable WHERE MyDerivedTable.RowNum BETWEEN 1 AND 10";
            data.SelectCommand.Connection = Connection;
            crbuilder = factory.CreateCommandBuilder();
            crbuilder.DataAdapter = data;

            data.Fill(TchTableLast);

            Count += 10;
            Page += 1;

            gridView.DataSource = TchTableLast;
        }

        public static void GetListOfStudent(DataGridView gridView, ConnectionStringSettings conStrSettings, Label lbPage_student,  ref int n, ref int Page, ref int Count, ref int NOP)
        {
            Page = 0;
            Count = 0;

            DbConnection Connection;
            DbProviderFactory factory;
            DbDataAdapter data;
            DataTable StdTable = new DataTable();
            DataTable StdTableLast = new DataTable();

            factory = DbProviderFactories.GetFactory(conStrSettings.ProviderName);
            Connection = factory.CreateConnection();
            Connection.ConnectionString = conStrSettings.ConnectionString;

            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();
            data.SelectCommand.CommandText = @"select nd.*, lh.TenLop from NGUOIDUNG nd JOIN LOPHOC lh on lh.MaLop = nd.MaLop where nd.MaPhanQuyen = 'HS'";
            data.SelectCommand.Connection = Connection;
            var crbuilder = factory.CreateCommandBuilder();
            crbuilder.DataAdapter = data;

            data.Fill(StdTable);
            n = StdTable.Rows.Count;

            NOP = n / 10 + (102 % 10 > 0 ? 1 : 0);
            lbPage_student.Text = "1/" + NOP.ToString();

            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();
            data.SelectCommand.CommandText = @"SELECT * FROM (SELECT nd.*, lh.TenLop, ROW_NUMBER() OVER (ORDER BY nd.MaLop) AS RowNum FROM NGUOIDUNG nd JOIN LOPHOC lh on lh.MaLop = nd.MaLop WHERE nd.MaPhanQuyen = 'HS') AS MyDerivedTable WHERE MyDerivedTable.RowNum BETWEEN 1 AND 10";
            data.SelectCommand.Connection = Connection;
            crbuilder = factory.CreateCommandBuilder();
            crbuilder.DataAdapter = data;

            data.Fill(StdTableLast);

            Count += 10;
            Page += 1;

            gridView.DataSource = StdTableLast;
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
                var ResultTemp = dataContext.KETQUATHIs.Where(kq => kq.TaiKhoan.Equals(taiKhoan)).Select(kq => kq).ToList();
                var listStudentTemp = dataContext.DANHSACHTHISINHs.Where(ds => ds.TaiKhoan.Equals(taiKhoan)).Select(ds => ds).ToList();
                NGUOIDUNG Student = dataContext.NGUOIDUNGs.Where(nd => nd.TaiKhoan.Equals(taiKhoan)).Select(nd => nd).Single();
                KETQUATHI Result = null;
                DANHSACHTHISINH listStudent = null;
                if (ResultTemp.Count > 0)
                {
                    Result = dataContext.KETQUATHIs.Where(kq => kq.TaiKhoan.Equals(taiKhoan)).Select(kq => kq).Single();
                    dataContext.KETQUATHIs.DeleteOnSubmit(Result);
                }
                if (listStudentTemp.Count > 0)
                {
                    listStudent = dataContext.DANHSACHTHISINHs.Where(ds => ds.TaiKhoan.Equals(taiKhoan)).Select(ds => ds).Single();
                    dataContext.DANHSACHTHISINHs.DeleteOnSubmit(listStudent);
                }
                
                dataContext.NGUOIDUNGs.DeleteOnSubmit(Student);
                dataContext.SubmitChanges();
            }
        }

        public static void UpdateStudent(Hoc_Sinhh Student)
        {
            using (var dataContext = new QLTTNDataContext())
            {
                var data = dataContext.NGUOIDUNGs.Where(nd => nd.TaiKhoan.Equals(Student.Tai_Khoan)).Select(nd => nd).Single();

                data.TaiKhoan = Student.Tai_Khoan;
                data.MatKhau = Student.Mat_Khau;
                data.MaKhoi = Student.Khoi;
                data.MaLop = Student.Lop;
                data.HoTen = Student.Ho_Ten;
                data.CMND_TCC = Student.CMND_TCC;
                data.NgaySinh = Student.Ngay_Sinh;
                data.SoDienThoai = Student.SDT;
                data.Email = Student.Email;

                
                dataContext.SubmitChanges();
            }
        }

        public static void UpdateTeacher(Giao_Vienn Teacher)
        {
            using (var dataContext = new QLTTNDataContext())
            {
                var data = dataContext.NGUOIDUNGs.Where(nd => nd.TaiKhoan.Equals(Teacher.Tai_Khoan)).Select(nd => nd).Single();


                data.TaiKhoan = Teacher.Tai_Khoan;
                data.MatKhau = Teacher.Mat_Khau;
                data.MaKhoi = Teacher.Khoi;
                data.MaLop = Teacher.Lop;
                data.HoTen = Teacher.Ho_Ten;
                data.CMND_TCC = Teacher.CMND_TCC;
                data.NgaySinh = Teacher.Ngay_Sinh;
                data.SoDienThoai = Teacher.SDT;
                data.Email = Teacher.Email;

                dataContext.SubmitChanges();
            }
        }

        public static List<Classes> LoadClasses()
        {
            List<Classes> listClasses = new List<Classes>();
            using (var data = new QLTTNDataContext())
            {
                var Data = data.LOPHOCs.Select(lh => new { lh.TenLop, lh.MaLop }).ToList();

                foreach (var i in Data)
                {
                    Classes Class = new Classes()
                    {
                        ClassName = i.TenLop,
                        ClassID = i.MaLop
                    };

                    listClasses.Add(Class);
                }
            }
            return listClasses;
        }

        public static List<Grades> LoadGrades()
        {
            List<Grades> listGrades = new List<Grades>();
            using (var data = new QLTTNDataContext())
            {
                var Data = data.KHOIs.Select(k => new { k.TenKhoi, k.MaKhoi }).ToList();

                foreach (var i in Data)
                {
                    Grades Grade = new Grades()
                    {
                        GradeName = i.TenKhoi,
                        GradeID = i.MaKhoi
                    };

                    listGrades.Add(Grade);
                }
            }
            return listGrades;
        }

        public static DataTable SearchingForStudentWithName(string Information, ConnectionStringSettings conStrSettings, Label lbPage_student, ref int n, ref int Page, ref int Count, ref int NOP)
        {
            Page = 0;
            Count = 0;
            DbConnection Connection;
            DbProviderFactory factory;
            DbDataAdapter data;
            DataTable StdTable = new DataTable();
            DataTable StdTableLast = new DataTable();

            factory = DbProviderFactories.GetFactory(conStrSettings.ProviderName);
            Connection = factory.CreateConnection();
            Connection.ConnectionString = conStrSettings.ConnectionString;

            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();
            data.SelectCommand.CommandText = @"select nd.*, lh.TenLop from NGUOIDUNG nd join LOPHOC lh on nd.MaLop = lh.MaLop join KHOI k on k.MaKhoi = lh.MaKhoi where nd.HoTen like N'%" + Information + "%'";
            data.SelectCommand.Connection = Connection;
            var crbuilder = factory.CreateCommandBuilder();
            crbuilder.DataAdapter = data;

            
            data.Fill(StdTable);
            n = StdTable.Rows.Count;

            NOP = n / 10 + (102 % 10 > 0 ? 1 : 0);
            lbPage_student.Text = "1/" + NOP.ToString();

            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();
            data.SelectCommand.CommandText = @"SELECT * FROM (SELECT nd.*, lh.TenLop, ROW_NUMBER() OVER (ORDER BY nd.MaLop) AS RowNum FROM NGUOIDUNG nd JOIN LOPHOC lh on lh.MaLop = nd.MaLop WHERE nd.HoTen like N'%" + Information + "%') AS MyDerivedTable WHERE MyDerivedTable.RowNum BETWEEN 1 AND 10";
            data.SelectCommand.Connection = Connection;
            crbuilder = factory.CreateCommandBuilder();
            crbuilder.DataAdapter = data;

            
            data.Fill(StdTableLast);

            Count += 10;
            Page += 1;

            return StdTableLast;
        }

        public static DataTable SearchingForTeacherWithName(string Information, ConnectionStringSettings conStrSettings, Label lbPage_teacher, ref int n, ref int Page, ref int Count, ref int NOP)
        {
            Page = 0;
            Count = 0;
            DbConnection Connection;
            DbProviderFactory factory;
            DbDataAdapter data;
            DataTable TchTable = new DataTable();
            DataTable TchTableLast = new DataTable();

            factory = DbProviderFactories.GetFactory(conStrSettings.ProviderName);
            Connection = factory.CreateConnection();
            Connection.ConnectionString = conStrSettings.ConnectionString;

            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();
            data.SelectCommand.CommandText = @"select nd.*, k.TenKhoi from NGUOIDUNG nd join KHOI k on k.MaKhoi = nd.MaKhoi where nd.HoTen like N'%" + Information + "%'";
            data.SelectCommand.Connection = Connection;
            var crbuilder = factory.CreateCommandBuilder();
            crbuilder.DataAdapter = data;


            data.Fill(TchTable);
            n = TchTable.Rows.Count;

            NOP = n / 10 + (102 % 10 > 0 ? 1 : 0);
            lbPage_teacher.Text = "1/" + NOP.ToString();

            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();
            data.SelectCommand.CommandText = @"SELECT * FROM (SELECT nd.*, k.TenKhoi, ROW_NUMBER() OVER (ORDER BY nd.MaKhoi) AS RowNum FROM NGUOIDUNG nd JOIN KHOI k on k.MaKhoi = nd.MaKhoi WHERE nd.HoTen like N'%" + Information + "%') AS MyDerivedTable WHERE MyDerivedTable.RowNum BETWEEN 1 AND 10";
            data.SelectCommand.Connection = Connection;
            crbuilder = factory.CreateCommandBuilder();
            crbuilder.DataAdapter = data;


            data.Fill(TchTableLast);

            Count += 10;
            Page += 1;

            return TchTableLast;
        }

        public static DataTable SearchingForStudentWithClass(string Information, ConnectionStringSettings conStrSettings, Label lbPage_student, ref int n, ref int Page, ref int Count, ref int NOP)
        {
            Page = 0;
            Count = 0;
            DbConnection Connection;
            DbProviderFactory factory;
            DbDataAdapter data;
            DataTable StdTable = new DataTable();
            DataTable StdTableLast = new DataTable();

            factory = DbProviderFactories.GetFactory(conStrSettings.ProviderName);
            Connection = factory.CreateConnection();
            Connection.ConnectionString = conStrSettings.ConnectionString;

            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();
            data.SelectCommand.CommandText = @"select nd.*, lh.TenLop from NGUOIDUNG nd join LOPHOC lh on nd.MaLop = lh.MaLop where lh.TenLop like N'%" + Information + "%'";
            data.SelectCommand.Connection = Connection;
            var crbuilder = factory.CreateCommandBuilder();
            crbuilder.DataAdapter = data;


            data.Fill(StdTable);
            n = StdTable.Rows.Count;

            NOP = n / 10 + (102 % 10 > 0 ? 1 : 0);
            lbPage_student.Text = "1/" + NOP.ToString();

            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();
            data.SelectCommand.CommandText = @"SELECT * FROM (SELECT nd.*, lh.TenLop, ROW_NUMBER() OVER (ORDER BY nd.MaLop) AS RowNum FROM NGUOIDUNG nd JOIN LOPHOC lh on lh.MaLop = nd.MaLop WHERE lh.TenLop like N'%" + Information + "%') AS MyDerivedTable WHERE MyDerivedTable.RowNum BETWEEN 1 AND 10";
            data.SelectCommand.Connection = Connection;
            crbuilder = factory.CreateCommandBuilder();
            crbuilder.DataAdapter = data;


            data.Fill(StdTableLast);

            Count += 10;
            Page += 1;

            return StdTableLast;
        }

        public static DataTable SearchingForTeacherWithGrade(string Information, ConnectionStringSettings conStrSettings, Label lbPage_teacher, ref int n, ref int Page, ref int Count, ref int NOP)
        {
            Page = 0;
            Count = 0;
            DbConnection Connection;
            DbProviderFactory factory;
            DbDataAdapter data;
            DataTable TchTable = new DataTable();
            DataTable TchTableLast = new DataTable();

            factory = DbProviderFactories.GetFactory(conStrSettings.ProviderName);
            Connection = factory.CreateConnection();
            Connection.ConnectionString = conStrSettings.ConnectionString;

            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();
            data.SelectCommand.CommandText = @"select nd.*, k.TenKhoi from NGUOIDUNG nd join KHOI k on k.MaKhoi = nd.MaKhoi where nd.TenKhoi like N'%" + Information + "%'";
            data.SelectCommand.Connection = Connection;
            var crbuilder = factory.CreateCommandBuilder();
            crbuilder.DataAdapter = data;


            data.Fill(TchTable);
            n = TchTable.Rows.Count;

            NOP = n / 10 + (102 % 10 > 0 ? 1 : 0);
            lbPage_teacher.Text = "1/" + NOP.ToString();

            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();
            data.SelectCommand.CommandText = @"SELECT * FROM (SELECT nd.*, k.TenKhoi, ROW_NUMBER() OVER (ORDER BY nd.MaKhoi) AS RowNum FROM NGUOIDUNG nd JOIN KHOI k on k.MaKhoi = nd.MaKhoi WHERE k.TenKhoi like N'%" + Information + "%') AS MyDerivedTable WHERE MyDerivedTable.RowNum BETWEEN 1 AND 10";
            data.SelectCommand.Connection = Connection;
            crbuilder = factory.CreateCommandBuilder();
            crbuilder.DataAdapter = data;


            data.Fill(TchTableLast);

            Count += 10;
            Page += 1;

            return TchTableLast;
        }

        public static void PrevPage_student(DataGridView gridView, ConnectionStringSettings conStrSettings, Label lbPage_student, ref int n, ref int Page, ref int Count, ref int NOP, string Information, RadioButton radioButton_StdName, RadioButton radioButton_StdClass)
        {
            DbConnection Connection;
            DbProviderFactory factory;
            DbDataAdapter data;
            DataTable StdTable = new DataTable();

            factory = DbProviderFactories.GetFactory(conStrSettings.ProviderName);
            Connection = factory.CreateConnection();
            Connection.ConnectionString = conStrSettings.ConnectionString;

            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();

            if (radioButton_StdName.Checked == true)
            {
                data.SelectCommand.CommandText = @"SELECT * FROM (SELECT nd.*, lh.TenLop, ROW_NUMBER() OVER (ORDER BY nd.MaLop) AS RowNum FROM NGUOIDUNG nd JOIN LOPHOC lh on lh.MaLop = nd.MaLop WHERE nd.HoTen like N'%" + Information + "%') AS MyDerivedTable WHERE MyDerivedTable.RowNum BETWEEN " + (Count + 1).ToString() + " AND " + (Count + 10).ToString();
            }
            if (radioButton_StdClass.Checked == true)
            {
                data.SelectCommand.CommandText = @"SELECT * FROM (SELECT nd.*, lh.TenLop, ROW_NUMBER() OVER (ORDER BY nd.MaLop) AS RowNum FROM NGUOIDUNG nd JOIN LOPHOC lh on lh.MaLop = nd.MaLop WHERE lh.TenLop like N'%" + Information + "%') AS MyDerivedTable WHERE MyDerivedTable.RowNum BETWEEN " + (Count + 1).ToString() + " AND " + (Count + 10).ToString();
            }
            if (radioButton_StdName.Checked == false && radioButton_StdClass.Checked == false)
            {
                return;
            }
            
            data.SelectCommand.Connection = Connection;
            var crbuilder = factory.CreateCommandBuilder();
            crbuilder.DataAdapter = data;

            StdTable = new DataTable();
            data.Fill(StdTable);

            gridView.DataSource = StdTable;

            Page--;
            lbPage_student.Text = Page.ToString() + '/' + NOP.ToString();
            Count -= 10;
        }

        public static void NextPage_student_last(DataGridView gridView, ConnectionStringSettings conStrSettings, Label lbPage_student, ref int n, ref int Page, ref int Count, ref int NOP, string Information, RadioButton radioButton_StdName, RadioButton radioButton_StdClass)
        {
            DbConnection Connection;
            DbProviderFactory factory;
            DbDataAdapter data;
            DataTable StdTable = new DataTable();

            factory = DbProviderFactories.GetFactory(conStrSettings.ProviderName);
            Connection = factory.CreateConnection();
            Connection.ConnectionString = conStrSettings.ConnectionString;

            int Temp = (Count + 10) - ((Count + 10) - n);
            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();

            if (radioButton_StdName.Checked == true)
            {
                data.SelectCommand.CommandText = @"SELECT * FROM (SELECT nd.*, lh.TenLop, ROW_NUMBER() OVER (ORDER BY nd.MaLop) AS RowNum FROM NGUOIDUNG nd JOIN LOPHOC lh on lh.MaLop = nd.MaLop WHERE nd.HoTen like N'%" + Information + "%') AS MyDerivedTable WHERE MyDerivedTable.RowNum BETWEEN " + (Count + 1).ToString() + " AND " + Temp.ToString();
            }
            if (radioButton_StdClass.Checked == true)
            {
                data.SelectCommand.CommandText = @"SELECT * FROM (SELECT nd.*, lh.TenLop, ROW_NUMBER() OVER (ORDER BY nd.MaLop) AS RowNum FROM NGUOIDUNG nd JOIN LOPHOC lh on lh.MaLop = nd.MaLop WHERE lh.TenLop like N'%" + Information + "%') AS MyDerivedTable WHERE MyDerivedTable.RowNum BETWEEN " + (Count + 1).ToString() + " AND " + Temp.ToString();
            }
            if (radioButton_StdName.Checked == false && radioButton_StdClass.Checked == false)
            {
                return;
            }

            data.SelectCommand.Connection = Connection;
            var crbuilder_1 = factory.CreateCommandBuilder();
            crbuilder_1.DataAdapter = data;

            data.Fill(StdTable);

            gridView.DataSource = StdTable;

            Page++;
            lbPage_student.Text = Page.ToString() + '/' + NOP.ToString();
            Count += 10;
        }

        public static void NextPage_student(DataGridView gridView, ConnectionStringSettings conStrSettings, Label lbPage_student, ref int n, ref int Page, ref int Count, ref int NOP, string Information, RadioButton radioButton_StdName, RadioButton radioButton_StdClass)
        {
            DbConnection Connection;
            DbProviderFactory factory;
            DbDataAdapter data;
            DataTable StdTable = new DataTable();

            factory = DbProviderFactories.GetFactory(conStrSettings.ProviderName);
            Connection = factory.CreateConnection();
            Connection.ConnectionString = conStrSettings.ConnectionString;

            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();

            if (radioButton_StdName.Checked == true)
            {
                data.SelectCommand.CommandText = @"SELECT * FROM (SELECT nd.*, lh.TenLop, ROW_NUMBER() OVER (ORDER BY nd.MaLop) AS RowNum FROM NGUOIDUNG nd JOIN LOPHOC lh on lh.MaLop = nd.MaLop WHERE nd.HoTen like N'%" + Information + "%') AS MyDerivedTable WHERE MyDerivedTable.RowNum BETWEEN " + (Count + 1).ToString() + " AND " + (Count + 10).ToString();
            }
            if (radioButton_StdClass.Checked == true)
            {
                data.SelectCommand.CommandText = @"SELECT * FROM (SELECT nd.*, lh.TenLop, ROW_NUMBER() OVER (ORDER BY nd.MaLop) AS RowNum FROM NGUOIDUNG nd JOIN LOPHOC lh on lh.MaLop = nd.MaLop WHERE lh.TenLop like N'%" + Information + "%') AS MyDerivedTable WHERE MyDerivedTable.RowNum BETWEEN " + (Count + 1).ToString() + " AND " + (Count + 10).ToString();
            }
            if (radioButton_StdName.Checked == false && radioButton_StdClass.Checked == false)
            {
                return;
            }
            data.SelectCommand.Connection = Connection;
            var crbuilder = factory.CreateCommandBuilder();
            crbuilder.DataAdapter = data;

            StdTable = new System.Data.DataTable();
            data.Fill(StdTable);

            gridView.DataSource = StdTable;

            Page++;
            lbPage_student.Text = Page.ToString() + '/' + NOP.ToString();
            Count += 10;
        }

        public static void PrevPage_teacher(DataGridView gridView, ConnectionStringSettings conStrSettings, Label lbPage_teacher, ref int n, ref int Page, ref int Count, ref int NOP, string Information, RadioButton radioButton_TchName, RadioButton radioButton_TchGrade)
        {
            DbConnection Connection;
            DbProviderFactory factory;
            DbDataAdapter data;
            DataTable TchTable = new DataTable();

            factory = DbProviderFactories.GetFactory(conStrSettings.ProviderName);
            Connection = factory.CreateConnection();
            Connection.ConnectionString = conStrSettings.ConnectionString;

            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();

            if (radioButton_TchName.Checked == true)
            {
                data.SelectCommand.CommandText = @"SELECT * FROM (SELECT nd.*, k.TenKhoi, ROW_NUMBER() OVER (ORDER BY nd.MaKhoi) AS RowNum FROM NGUOIDUNG nd JOIN KHOI k on k.MaKhoi = nd.MaKhoi WHERE nd.HoTen like N'%" + Information + "%') AS MyDerivedTable WHERE MyDerivedTable.RowNum BETWEEN " + (Count + 1).ToString() + " AND " + (Count + 10).ToString();
            }
            if (radioButton_TchGrade.Checked == true)
            {
                data.SelectCommand.CommandText = @"SELECT * FROM (SELECT nd.*, k.TenKhoi, ROW_NUMBER() OVER (ORDER BY nd.MaKhoi) AS RowNum FROM NGUOIDUNG nd JOIN KHOI k on k.MaKhoi = nd.MaKhoi WHERE k.TenKhoi like N'%" + Information + "%') AS MyDerivedTable WHERE MyDerivedTable.RowNum BETWEEN " + (Count + 1).ToString() + " AND " + (Count + 10).ToString();
            }
            if (radioButton_TchName.Checked == false && radioButton_TchGrade.Checked == false)
            {
                return;
            }
   
            data.SelectCommand.Connection = Connection;
            var crbuilder = factory.CreateCommandBuilder();
            crbuilder.DataAdapter = data;

            TchTable = new DataTable();
            data.Fill(TchTable);

            gridView.DataSource = TchTable;

            Page--;
            lbPage_teacher.Text = Page.ToString() + '/' + NOP.ToString();
            Count -= 10;
        }

        public static void NextPage_teacher_last(DataGridView gridView, ConnectionStringSettings conStrSettings, Label lbPage_teacher, ref int n, ref int Page, ref int Count, ref int NOP, string Information, RadioButton radioButton_TchName, RadioButton radioButton_TchGrade)
        {
            DbConnection Connection;
            DbProviderFactory factory;
            DbDataAdapter data;
            DataTable TchTable = new DataTable();

            factory = DbProviderFactories.GetFactory(conStrSettings.ProviderName);
            Connection = factory.CreateConnection();
            Connection.ConnectionString = conStrSettings.ConnectionString;

            int Temp = (Count + 10) - ((Count + 10) - n);
            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();

            if (radioButton_TchName.Checked == true)
            {
                data.SelectCommand.CommandText = @"SELECT * FROM (SELECT nd.*, k.TenKhoi, ROW_NUMBER() OVER (ORDER BY nd.MaKhoi) AS RowNum FROM NGUOIDUNG nd JOIN KHOI k on k.MaKhoi = nd.MaKhoi WHERE nd.HoTen like N'%" + Information + "%') AS MyDerivedTable WHERE MyDerivedTable.RowNum BETWEEN " + (Count + 1).ToString() + " AND " + Temp.ToString();
            }
            if (radioButton_TchGrade.Checked == true)
            {
                data.SelectCommand.CommandText = @"SELECT * FROM (SELECT nd.*, k.TenKhoi, ROW_NUMBER() OVER (ORDER BY nd.MaKhoi) AS RowNum FROM NGUOIDUNG nd JOIN KHOI k on k.MaKhoi = nd.MaKhoi WHERE k.TenKhoi like N'%" + Information + "%') AS MyDerivedTable WHERE MyDerivedTable.RowNum BETWEEN " + (Count + 1).ToString() + " AND " + Temp.ToString();
            }
            if (radioButton_TchName.Checked == false && radioButton_TchGrade.Checked == false)
            {
                return;
            }

            data.SelectCommand.Connection = Connection;
            var crbuilder_1 = factory.CreateCommandBuilder();
            crbuilder_1.DataAdapter = data;


            data.Fill(TchTable);

            gridView.DataSource = TchTable;

            Page++;
            lbPage_teacher.Text = Page.ToString() + '/' + NOP.ToString();
            Count += 10;
        }

        public static void NextPage_teacher(DataGridView gridView, ConnectionStringSettings conStrSettings, Label lbPage_teacher, ref int n, ref int Page, ref int Count, ref int NOP, string Information, RadioButton radioButton_TchName, RadioButton radioButton_TchGrade)
        {
            DbConnection Connection;
            DbProviderFactory factory;
            DbDataAdapter data;
            DataTable TchTable = new DataTable();

            factory = DbProviderFactories.GetFactory(conStrSettings.ProviderName);
            Connection = factory.CreateConnection();
            Connection.ConnectionString = conStrSettings.ConnectionString;

            data = factory.CreateDataAdapter();
            data.SelectCommand = factory.CreateCommand();

            if (radioButton_TchName.Checked == true)
            {
                data.SelectCommand.CommandText = @"SELECT * FROM (SELECT nd.*, k.TenKhoi, ROW_NUMBER() OVER (ORDER BY nd.MaKhoi) AS RowNum FROM NGUOIDUNG nd JOIN KHOI k on k.MaKhoi = nd.MaKhoi WHERE nd.HoTen like N'%" + Information + "%') AS MyDerivedTable WHERE MyDerivedTable.RowNum BETWEEN " + (Count + 1).ToString() + " AND " + (Count + 10).ToString();
            }
            if (radioButton_TchGrade.Checked == true)
            {
                data.SelectCommand.CommandText = @"SELECT * FROM (SELECT nd.*, k.TenKhoi, ROW_NUMBER() OVER (ORDER BY nd.MaKhoi) AS RowNum FROM NGUOIDUNG nd JOIN KHOI k on k.MaKhoi = nd.MaKhoi WHERE k.TenKhoi like N'%" + Information + "%') AS MyDerivedTable WHERE MyDerivedTable.RowNum BETWEEN " + (Count + 1).ToString() + " AND " + (Count + 10).ToString();
            }
            if (radioButton_TchName.Checked == false && radioButton_TchGrade.Checked == false)
            {
                return;
            }

            data.SelectCommand.Connection = Connection;
            var crbuilder = factory.CreateCommandBuilder();
            crbuilder.DataAdapter = data;

            TchTable = new System.Data.DataTable();
            data.Fill(TchTable);

            gridView.DataSource = TchTable;

            Page++;
            lbPage_teacher.Text = Page.ToString() + '/' + NOP.ToString();
            Count += 10;
        }
    }
}
