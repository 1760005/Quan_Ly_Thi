using Quan_Ly_Thi.DAO;
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

namespace Quan_Ly_Thi.BUS
{
    public class BUS_Admin
    {
        static int n, NOP;
        static int Page = 0;
        static int Count = 0;

        public static void GetListOfTeacher(DataGridView gridView, ConnectionStringSettings conStrSettings, Label lbPage_teacher)
        {
            DAO_Admin.GetListOfTeacher(gridView, conStrSettings, lbPage_teacher, ref n, ref Page, ref Count, ref NOP);
        }

        public static void GetListOfStudent(DataGridView gridView, ConnectionStringSettings conStrSettings, Label lbPage_student)
        {
            DAO_Admin.GetListOfStudent(gridView, conStrSettings, lbPage_student, ref n, ref Page, ref Count, ref NOP);
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

        public static void BackupDataBase(string Path, ConnectionStringSettings conStrSettings)
        {
            DAO_Admin.BackupDataBase(Path, conStrSettings);
        }

        public static void RestoreDataBase(string Path, ConnectionStringSettings conStrSettings)
        {
            DAO_Admin.RestoreDataBase(Path, conStrSettings);
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

        public static void UpdateStudent(Hoc_Sinhh Student, string Student_User_Account)
        {
            if (Student.Tai_Khoan == Student_User_Account)
            {
                DAO_Admin.UpdateStudent(Student);
            }
            MessageBox.Show("You Can't Change The Name Account Of User!");
        }

        public static void UpdateTeacher(Giao_Vienn Teacher, string Teacher_User_Account)
        {
            if (Teacher.Tai_Khoan == Teacher_User_Account)
            {
                DAO_Admin.UpdateTeacher(Teacher);
            }
            MessageBox.Show("You Can't Change The Name Account Of User!");
        }

        public static List<Classes> LoadClasses()
        {
            return DAO_Admin.LoadClasses();
        }

        public static List<Grades> LoadGrades()
        {
            return DAO_Admin.LoadGrades();
        }

        public static DataTable SearchingForStudentWithName(string Information, ConnectionStringSettings conStrSettings, Label lbPage_student)
        {
            return DAO_Admin.SearchingForStudentWithName(Information, conStrSettings, lbPage_student, ref n, ref Page, ref Count, ref NOP);
        }

        public static DataTable SearchingForStudentWithClass(string Information, ConnectionStringSettings conStrSettings, Label lbPage_student)
        {
            return DAO_Admin.SearchingForStudentWithClass(Information, conStrSettings, lbPage_student, ref n, ref Page, ref Count, ref NOP);
        }

        public static DataTable SearchingForTeacherWithName(string Information, ConnectionStringSettings conStrSettings, Label lbPage_teacher)
        {
            return DAO_Admin.SearchingForTeacherWithName(Information, conStrSettings, lbPage_teacher, ref n, ref Page, ref Count, ref NOP);
        }

        public static DataTable SearchingForTeacherWithGrade(string Information, ConnectionStringSettings conStrSettings, Label lbPage_teacher)
        {
            return DAO_Admin.SearchingForTeacherWithGrade(Information, conStrSettings, lbPage_teacher, ref n, ref Page, ref Count, ref NOP);
        }

        public static void PrevPage_student(DataGridView gridView,ConnectionStringSettings conStrSettings, Label lbPage_student, string Information, RadioButton radioButton_StdName, RadioButton radioButton_StdClass)
        {
            if (Page > 1)
            {
                DAO_Admin.PrevPage_student(gridView, conStrSettings, lbPage_student, ref n, ref Page, ref Count, ref NOP, Information, radioButton_StdName, radioButton_StdClass);
            }
            
        }

        public static void NextPage_student(DataGridView gridView, ConnectionStringSettings conStrSettings, Label lbPage_student, string Information, RadioButton radioButton_StdName, RadioButton radioButton_StdClass)
        {
            if (Page < NOP)
            {
                if (Count + 10 > n)
                {
                    DAO_Admin.NextPage_student_last(gridView, conStrSettings, lbPage_student, ref n, ref Page, ref Count, ref NOP, Information, radioButton_StdName, radioButton_StdClass);
                }
                else
                {
                    DAO_Admin.NextPage_student(gridView, conStrSettings, lbPage_student, ref n, ref Page, ref Count, ref NOP, Information, radioButton_StdName, radioButton_StdClass);
                }
            }
        }

        public static void PrevPage_teacher(DataGridView gridView, ConnectionStringSettings conStrSettings, Label lbPage_teacher, string Information, RadioButton radioButton_TchName, RadioButton radioButton_TchGrade)
        {
            if (Page > 1)
            {
                DAO_Admin.PrevPage_teacher(gridView, conStrSettings, lbPage_teacher, ref n, ref Page, ref Count, ref NOP, Information, radioButton_TchName, radioButton_TchGrade);
            }

        }

        public static void NextPage_teacher(DataGridView gridView, ConnectionStringSettings conStrSettings, Label lbPage_teacher, string Information, RadioButton radioButton_TchName, RadioButton radioButton_TchGrade)
        {
            if (Page <= NOP)
            {
                if (Count + 10 > n)
                {
                    DAO_Admin.NextPage_teacher_last(gridView, conStrSettings, lbPage_teacher, ref n, ref Page, ref Count, ref NOP, Information, radioButton_TchName, radioButton_TchGrade);
                }
                else
                {
                    DAO_Admin.NextPage_teacher(gridView, conStrSettings, lbPage_teacher, ref n, ref Page, ref Count, ref NOP, Information, radioButton_TchName, radioButton_TchGrade);
                }
            }
        }
    }
}
