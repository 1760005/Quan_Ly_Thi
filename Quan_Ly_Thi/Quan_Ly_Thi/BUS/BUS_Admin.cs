using Microsoft.Office.Interop.Excel;
using Quan_Ly_Thi.DAO;
using Quan_Ly_Thi.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Thi.BUS
{
    public class BUS_Admin
    {
        static int n, NOP;
        static int Page = 0;
        static int Count = 0;

        public static void GetListOfTeacher(DataGridView gridView, ConnectionStringSettings conStrSettings, System.Windows.Forms.Label lbPage_teacher)
        {
            DAO_Admin.GetListOfTeacher(gridView, conStrSettings, lbPage_teacher, ref n, ref Page, ref Count, ref NOP);
        }

        public static void GetListOfStudent(DataGridView gridView, ConnectionStringSettings conStrSettings, System.Windows.Forms.Label lbPage_student)
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

        public static System.Data.DataTable SearchingForStudentWithName(string Information, ConnectionStringSettings conStrSettings, System.Windows.Forms.Label lbPage_student)
        {
            return DAO_Admin.SearchingForStudentWithName(Information, conStrSettings, lbPage_student, ref n, ref Page, ref Count, ref NOP);
        }

        public static System.Data.DataTable SearchingForStudentWithClass(string Information, ConnectionStringSettings conStrSettings, System.Windows.Forms.Label lbPage_student)
        {
            return DAO_Admin.SearchingForStudentWithClass(Information, conStrSettings, lbPage_student, ref n, ref Page, ref Count, ref NOP);
        }

        public static System.Data.DataTable SearchingForTeacherWithName(string Information, ConnectionStringSettings conStrSettings, System.Windows.Forms.Label lbPage_teacher)
        {
            return DAO_Admin.SearchingForTeacherWithName(Information, conStrSettings, lbPage_teacher, ref n, ref Page, ref Count, ref NOP);
        }

        public static System.Data.DataTable SearchingForTeacherWithGrade(string Information, ConnectionStringSettings conStrSettings, System.Windows.Forms.Label lbPage_teacher)
        {
            return DAO_Admin.SearchingForTeacherWithGrade(Information, conStrSettings, lbPage_teacher, ref n, ref Page, ref Count, ref NOP);
        }

        public static void PrevPage_student(DataGridView gridView,ConnectionStringSettings conStrSettings, System.Windows.Forms.Label lbPage_student, string Information, RadioButton radioButton_StdName, RadioButton radioButton_StdClass)
        {
            if (Page > 1)
            {
                DAO_Admin.PrevPage_student(gridView, conStrSettings, lbPage_student, ref n, ref Page, ref Count, ref NOP, Information, radioButton_StdName, radioButton_StdClass);
            }
            
        }

        public static void NextPage_student(DataGridView gridView, ConnectionStringSettings conStrSettings, System.Windows.Forms.Label lbPage_student, string Information, RadioButton radioButton_StdName, RadioButton radioButton_StdClass)
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

        public static void PrevPage_teacher(DataGridView gridView, ConnectionStringSettings conStrSettings, System.Windows.Forms.Label lbPage_teacher, string Information, RadioButton radioButton_TchName, RadioButton radioButton_TchGrade)
        {
            if (Page > 1)
            {
                DAO_Admin.PrevPage_teacher(gridView, conStrSettings, lbPage_teacher, ref n, ref Page, ref Count, ref NOP, Information, radioButton_TchName, radioButton_TchGrade);
            }

        }

        public static void NextPage_teacher(DataGridView gridView, ConnectionStringSettings conStrSettings, System.Windows.Forms.Label lbPage_teacher, string Information, RadioButton radioButton_TchName, RadioButton radioButton_TchGrade)
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

        public static void btnExport_Click(DataParameter _inputParameter, BackgroundWorker bgWorker_Export, DataGridView dt_student, DataGridView dt_teacher, TabControl ControlAdmin, TabPage TabList_student, TabPage TabList_teacher, ProgressBar pgBar)
        {
            if (bgWorker_Export.IsBusy)
            {
                return;
            }
            using (SaveFileDialog s_File = new SaveFileDialog() { Filter = "Excel Workbook|*.xls" })
            {
                if (s_File.ShowDialog() == DialogResult.OK)
                {
                    _inputParameter.FileName = s_File.FileName;
                    List<NGUOIDUNG> listUser = new List<NGUOIDUNG>();
                    for (int i = 0; i < dt_student.Rows.Count - 1; i++)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        if (ControlAdmin.TabPages[ControlAdmin.SelectedIndex] == TabList_student)
                        {
                            row = dt_student.Rows[i];
                        }
                        if (ControlAdmin.TabPages[ControlAdmin.SelectedIndex] == TabList_teacher)
                        {
                            row = dt_teacher.Rows[i];
                        }
                        NGUOIDUNG user = new NGUOIDUNG()
                        {
                            TaiKhoan = row.Cells[0].Value.ToString(),
                            MatKhau = row.Cells[1].Value.ToString(),
                            MaPhanQuyen = row.Cells[2].Value.ToString(),
                            MaKhoi = row.Cells[3].Value.ToString(),
                            MaLop = row.Cells[4].Value.ToString(),
                            HoTen = row.Cells[5].Value.ToString(),
                            CMND_TCC = row.Cells[6].Value.ToString(),
                            NgaySinh = DateTime.Parse(row.Cells[7].Value.ToString()),
                            SoDienThoai = row.Cells[8].Value.ToString(),
                            Email = row.Cells[9].Value.ToString()
                        };
                        listUser.Add(user);
                    }

                    _inputParameter.listUser = listUser;

                    pgBar.Minimum = 0;
                    pgBar.Value = 0;
                    bgWorker_Export.RunWorkerAsync(_inputParameter);
                }
            }
        }

        public static void bgWorker_Export_DoWork(object sender, DoWorkEventArgs e, BackgroundWorker bgWorker_Export)
        {
            List<NGUOIDUNG> listUser = ((DataParameter)e.Argument).listUser;
            string FileName = ((DataParameter)e.Argument).FileName;

            Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = Excel.Workbooks.Add(XlSheetType.xlWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet = (Worksheet)Excel.ActiveSheet;
            Excel.Visible = false;
            int Index = 1;
            int Process = listUser.Count;

            xlWorkSheet.Cells[1, 1] = "TaiKhoan";
            xlWorkSheet.Cells[1, 2] = "MaPhanQuyen";
            xlWorkSheet.Cells[1, 3] = "MaKhoi";
            xlWorkSheet.Cells[1, 4] = "MaLop";
            xlWorkSheet.Cells[1, 5] = "HoTen";
            xlWorkSheet.Cells[1, 6] = "CMND_TCC";
            xlWorkSheet.Cells[1, 7] = "NgaySinh";
            xlWorkSheet.Cells[1, 8] = "SoDienThoai";
            xlWorkSheet.Cells[1, 9] = "Email";



            foreach (NGUOIDUNG nd in listUser)
            {
                if (!bgWorker_Export.CancellationPending)
                {
                    bgWorker_Export.ReportProgress(Index++ * 100 / Process);
                    xlWorkSheet.Cells[Index, 1] = nd.TaiKhoan;
                    xlWorkSheet.Cells[Index, 2] = nd.MaPhanQuyen;
                    xlWorkSheet.Cells[Index, 3] = nd.MaKhoi;
                    xlWorkSheet.Cells[Index, 4] = nd.MaLop;
                    xlWorkSheet.Cells[Index, 5] = nd.HoTen;
                    xlWorkSheet.Cells[Index, 6] = nd.CMND_TCC;
                    xlWorkSheet.Cells[Index, 7] = nd.NgaySinh.ToString();
                    xlWorkSheet.Cells[Index, 8] = nd.SoDienThoai;
                    xlWorkSheet.Cells[Index, 9] = nd.Email;
                }
            }

            xlWorkSheet.SaveAs(FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
            xlWorkBook.Close();
            Excel.Quit();
        }

        public static void bgWorker_Export_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e, System.Windows.Forms.Label lbStatus)
        {
            if (e.Error == null)
            {
                Thread.Sleep(100);
                lbStatus.Text = "Your Data Has Been Sucessfully Exported!";
            }
        }

        public static void bgWorker_Export_ProgressChanged(object sender, ProgressChangedEventArgs e, ProgressBar pgBar, System.Windows.Forms.Label lbStatus)
        {
            pgBar.Value = e.ProgressPercentage;
            lbStatus.Text = string.Format("Process...{0}%", e.ProgressPercentage);
            pgBar.Update();
        }
    }
}
