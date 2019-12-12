using Quan_Ly_Thi.DAO;
using Quan_Ly_Thi.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Thi.BUS
{
    public class BUS_Admin
    {
        static ConnectionStringSettings conStrSettings;
        static DbConnection Connection;
        static DbProviderFactory factory;
        static DbDataAdapter Students, Teachers, Users, Datas;
        System.Data.DataTable StdTable, TchTable;
        public static List<Giao_Vienn> layDanhSachGiaoVien()
        {
            return DAO_Admin.layDanhSachGiaoVien();
        }

        public static List<Hoc_Sinhh> layDanhSachHocSinh()
        {
            return DAO_Admin.layDanhSachHocSinh();
        }

        public static List<Ket_Qua_Thi> GetExaminationResult()
        {
            return DAO_Admin.GetExaminationResult();
        }

        public static List<Ket_Qua_Thi> GetExaminationResultWithAccount(string taiKhoan)
        {
            return DAO_Admin.GetExaminationResult(taiKhoan);
        }

        public static List<Ket_Qua_Thi> GetExaminationResultWithGrade(string maKhoi)
        {
            return DAO_Admin.GetExaminationResultForTeacher(maKhoi);
        }

        public static void BackupDataBase(string Path, DbProviderFactory factory, DbDataAdapter data, DbConnection connection)
        {
            DAO_Admin.BackupDataBase(Path, factory, data, connection);
        }

        public static void RestoreDataBase(string Path, DbProviderFactory factory, DbDataAdapter data, DbConnection connection)
        {
            DAO_Admin.RestoreDataBase(Path, factory, data, connection);
        }

        public static void ImportStudent(string Path, DataGridView gridView)
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            Microsoft.Office.Interop.Excel.Range xlRange;

            if (Path != string.Empty)
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(Path);
                xlWorkSheet = xlWorkBook.Worksheets["Sheet1"];
                xlRange = xlWorkSheet.UsedRange;

                gridView.ColumnCount = xlRange.Columns.Count;
                int n = 0;
                for (int i = 1; i < gridView.ColumnCount; i++)
                {
                    gridView.Columns[n].Name = xlRange.Cells[1, i].Text;
                    n++;
                }

                for (int i = 2; i < xlRange.Rows.Count; i++)
                {
                    gridView.Rows.Add(xlRange.Cells[i, 1].Text, xlRange.Cells[i, 2].Text, xlRange.Cells[i, 3].Text, xlRange.Cells[i, 4].Text, xlRange.Cells[i, 5].Text, xlRange.Cells[i, 6].Text, xlRange.Cells[i, 7].Text, xlRange.Cells[i, 8].Text);

                    DAO_Admin.InsertStudentWithExcel(xlRange, i);
                }

                xlWorkBook.Close();
                xlApp.Quit();
            }
        }

        public static void ImportTeacher(string Path, DataGridView gridView)
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            Microsoft.Office.Interop.Excel.Range xlRange;

            if (Path != string.Empty)
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(Path);
                xlWorkSheet = xlWorkBook.Worksheets["Sheet1"];
                xlRange = xlWorkSheet.UsedRange;

                gridView.ColumnCount = xlRange.Columns.Count;
                int n = 0;
                for (int i = 1; i < gridView.ColumnCount; i++)
                {
                    gridView.Columns[n].Name = xlRange.Cells[1, i].Text;
                    n++;
                }

                for (int i = 2; i < xlRange.Rows.Count; i++)
                {
                    gridView.Rows.Add(xlRange.Cells[i, 1].Text, xlRange.Cells[i, 2].Text, xlRange.Cells[i, 3].Text, xlRange.Cells[i, 4].Text, xlRange.Cells[i, 5].Text, xlRange.Cells[i, 6].Text, xlRange.Cells[i, 7].Text, xlRange.Cells[i, 8].Text);

                    DAO_Admin.InsertTeacherWithExcel(xlRange, i);
                }

                xlWorkBook.Close();
                xlApp.Quit();
            }
        }

        public static void DeleteUser(string taiKhoan)
        {
            DAO_Admin.DeleteUserWithLinq(taiKhoan);
        }

        public static void InsertStudent(Hoc_Sinhh Student)
        {
            DAO_Admin.InsertStudentWithLinq(Student);
        }

        public static void InsertTeacher(Giao_Vienn Teacher)
        {
            DAO_Admin.InsertTeacherWithLinq(Teacher);
        }
    }
}
