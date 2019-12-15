using Quan_Ly_Thi.BUS;
using Quan_Ly_Thi.DTO;
using Quan_Ly_Thi.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Office.Interop.Excel;

namespace Quan_Ly_Thi.GUI.Adminn
{
    public partial class frmAdmin : Form
    {
        static ConnectionStringSettings conStrSettings;
        static string Student_User_Account = null;
        static string Teacher_User_Account = null;
        static List<Classes> listClasses = new List<Classes>();
        static List<Grades> listGrades = new List<Grades>();

        
        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            string Path = null;
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Path = file.FileName;
                if (ControlAdmin.TabPages[ControlAdmin.SelectedIndex] == TabList_student)
                {
                    BUS_Admin.ImportStudent(Path, dt_student);
                }
                else
                {
                    BUS_Admin.ImportTeacher(Path, dt_teacher);
                }
            }
        }

        private void btnList_Student_Click(object sender, EventArgs e)
        {
            ControlAdmin.TabPages.Clear();
            ControlAdmin.TabPages.Add(TabList_student);
            
            BUS_Admin.GetListOfStudent(dt_student, conStrSettings, lbPage_student);
        }

        private void btnList_Teacher_Click(object sender, EventArgs e)
        {
            ControlAdmin.TabPages.Clear();
            ControlAdmin.TabPages.Add(TabList_teacher);
            
            BUS_Admin.GetListOfTeacher(dt_teacher, conStrSettings, lbPage_teacher);
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            ControlAdmin.TabPages.Clear();
            ControlAdmin.TabPages.Add(TabResult);
            dt_Result.DataSource = BUS_Admin.GetExaminationResult();
        }

        private void btnAdd_student_Click(object sender, EventArgs e)
        {
            Hoc_Sinhh Student = new Hoc_Sinhh()
            {
                Ho_Ten = txtFull_name_student.Text,
                Tai_Khoan = txtUser_name_student.Text,
                Mat_Khau = txtUser_name_student.Text,
                CMND_TCC = txtCMND_TCC_student.Text,
                Lop = Class_CBB.Items[Class_CBB.SelectedIndex].ToString(),
                Khoi = null,
                Email = txtMail_student.Text,
                SDT = txtSDT_student.Text,
                Ngay_Sinh = DateTime.Parse(maskedStdDOB.Text)
            };
            BUS_Admin.InsertStudent(Student);
        }

        private void btnAdd_teacher_Click(object sender, EventArgs e)
        {
            Giao_Vienn Teacher = new Giao_Vienn()
            {
                Ho_Ten = txtFull_name_teacher.Text,
                Tai_Khoan = txtUserName_teacher.Text,
                Mat_Khau = txtUserName_teacher.Text,
                CMND_TCC = txtCMND_TCC_teacher.Text,
                Khoi = Grade_CBB.Items[Grade_CBB.SelectedIndex].ToString(),
                Lop = null,
                Email = txtMail_teacher.Text,
                SDT = txtSDT_teacher.Text,
                Ngay_Sinh = DateTime.Parse(maskedTchDOB.Text)
            };
            BUS_Admin.InsertTeacher(Teacher);
        }

        private void btnRemove_student_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtUser_name_student.Text;
            BUS_Admin.DeleteUser(taiKhoan);
        }

        private void btnRemove_teacher_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtUserName_teacher.Text;
            BUS_Admin.DeleteUser(taiKhoan);
        }

        private void btnBack_up_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            string Path = null;
            if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Path = folder.SelectedPath;
            }
            BUS_Admin.BackupDataBase(Path, conStrSettings);
        }

        //System.Data.DataTable StdTable, TchTable;

        private void btnRestore_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            string Path = null;
            if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Path = folder.SelectedPath;
            }
            BUS_Admin.RestoreDataBase(Path, conStrSettings);
        }

        public frmAdmin()
        {
            InitializeComponent();
            ControlAdmin.TabPages.Clear();
            //AdminUser = Admin;

            Text = ConfigurationManager.AppSettings["title"];
            conStrSettings = ConfigurationManager.ConnectionStrings["Quan_Ly_Thi.Properties.Settings.QuanLyThiTracNghiemDBConnectionString4"];
        }

        private void btnUpdate_student_Click(object sender, EventArgs e)
        {
            Hoc_Sinhh Student = new Hoc_Sinhh()
            {
                Ho_Ten = txtFull_name_student.Text,
                Tai_Khoan = txtUser_name_student.Text,
                Mat_Khau = txtUser_name_student.Text,
                CMND_TCC = txtCMND_TCC_student.Text,
                Lop = Class_CBB.Items[Class_CBB.SelectedIndex].ToString(),
                Khoi = null,
                Email = txtMail_student.Text,
                SDT = txtSDT_student.Text,
                Ngay_Sinh = DateTime.Parse(maskedStdDOB.Text)
            };

            BUS_Admin.UpdateStudent(Student, Student_User_Account);
        }

        private void dt_student_SelectionChanged(object sender, EventArgs e)
        {
            if (dt_student.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dt_student.SelectedRows[0];
                Student_User_Account = row.Cells[0].Value.ToString();
                txtUser_name_student.Text = row.Cells[0].Value.ToString();
                txtCMND_TCC_student.Text = row.Cells[6].Value.ToString();
                txtFull_name_student.Text = row.Cells[5].Value.ToString();
                txtMail_student.Text = row.Cells[9].Value.ToString();
                txtSDT_student.Text = row.Cells[8].Value.ToString();
                maskedStdDOB.Text = row.Cells[7].Value.ToString();
                Class_CBB.Text = row.Cells[10].Value.ToString();
            }
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            listClasses = BUS_Admin.LoadClasses();
            Class_CBB.DataSource = listClasses;
            listGrades = BUS_Admin.LoadGrades();
            Grade_CBB.DataSource = listGrades;
        }

        private void btnUpdate_teacher_Click(object sender, EventArgs e)
        {
            Giao_Vienn Teacher = new Giao_Vienn()
            {
                Ho_Ten = txtFull_name_teacher.Text,
                Tai_Khoan = txtUserName_teacher.Text,
                Mat_Khau = txtUserName_teacher.Text,
                CMND_TCC = txtCMND_TCC_teacher.Text,
                Lop = null,
                Khoi = Grade_CBB.Items[Grade_CBB.SelectedIndex].ToString(),
                Email = txtMail_teacher.Text,
                SDT = txtSDT_teacher.Text,
                Ngay_Sinh = DateTime.Parse(maskedTchDOB.Text)
            };

            BUS_Admin.UpdateTeacher(Teacher, Teacher_User_Account);
        }

        private void btnStudent_Seach_Click(object sender, EventArgs e)
        {
            if (txtSearch_student.Text.ToString().StartsWith("0") || txtSearch_student.Text.ToString().StartsWith("1") || txtSearch_student.Text.ToString().StartsWith("2") || txtSearch_student.Text.ToString().StartsWith("3") || txtSearch_student.Text.ToString().StartsWith("4") || txtSearch_student.Text.ToString().StartsWith("5") || txtSearch_student.Text.ToString().StartsWith("6") || txtSearch_student.Text.ToString().StartsWith("7") || txtSearch_student.Text.ToString().StartsWith("8") || txtSearch_student.Text.ToString().StartsWith("9"))
            {
                MessageBox.Show("InCorrect Form");
                return;
            }
            if (Radiobtn_FullName_student.Checked == false && Radiobtn_ClassName.Checked == false)
            {
                return;
            }
            if (Radiobtn_FullName_student.Checked == true)
            {
                dt_student.DataSource = BUS_Admin.SearchingForStudentWithName(txtSearch_student.Text, conStrSettings, lbPage_student);
            }
            if (Radiobtn_ClassName.Checked == true)
            {
                dt_student.DataSource = BUS_Admin.SearchingForStudentWithClass(txtSearch_student.Text, conStrSettings, lbPage_student);
            }
        }

        private void btnTeacher_Search_Click(object sender, EventArgs e)
        {
            if (txtSearch_teacher.Text.ToString().StartsWith("0") || txtSearch_teacher.Text.ToString().StartsWith("1") || txtSearch_teacher.Text.ToString().StartsWith("2") || txtSearch_teacher.Text.ToString().StartsWith("3") || txtSearch_teacher.Text.ToString().StartsWith("4") || txtSearch_teacher.Text.ToString().StartsWith("5") || txtSearch_teacher.Text.ToString().StartsWith("6") || txtSearch_teacher.Text.ToString().StartsWith("7") || txtSearch_teacher.Text.ToString().StartsWith("8") || txtSearch_teacher.Text.ToString().StartsWith("9"))
            {
                MessageBox.Show("InCorrect Form");
                return;
            }
            if (Radiobtn_FullName_teacher.Checked == false && Radiobtn_GradeName.Checked == false)
            {
                return;
            }
            if (Radiobtn_FullName_teacher.Checked == true)
            {
                dt_student.DataSource = BUS_Admin.SearchingForTeacherWithName(txtSearch_student.Text, conStrSettings, lbPage_student);
            }
            if (Radiobtn_GradeName.Checked == true)
            {
                dt_student.DataSource = BUS_Admin.SearchingForTeacherWithGrade(txtSearch_student.Text, conStrSettings, lbPage_teacher);
            }
        }

        private void btnNext_student_Click(object sender, EventArgs e)
        {
            BUS_Admin.NextPage_student(dt_student, conStrSettings, lbPage_student, txtSearch_student.Text, Radiobtn_FullName_student, Radiobtn_ClassName);
        }

        private void btnPrev_student_Click(object sender, EventArgs e)
        {
            BUS_Admin.PrevPage_student(dt_student, conStrSettings, lbPage_student, txtSearch_student.Text, Radiobtn_FullName_student, Radiobtn_ClassName);
        }

        private void btnPrev_teacher_Click(object sender, EventArgs e)
        {
            BUS_Admin.NextPage_teacher(dt_teacher, conStrSettings, lbPage_teacher, txtSearch_teacher.Text, Radiobtn_FullName_teacher, Radiobtn_GradeName);
        }

        private void btnNext_teacher_Click(object sender, EventArgs e)
        {
            BUS_Admin.PrevPage_teacher(dt_teacher, conStrSettings, lbPage_teacher, txtSearch_teacher.Text, Radiobtn_FullName_teacher, Radiobtn_GradeName);
        }

        private void dt_teacher_SelectionChanged(object sender, EventArgs e)
        {
            if (dt_teacher.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dt_teacher.SelectedRows[0];
                Teacher_User_Account = row.Cells[0].Value.ToString();
                txtUserName_teacher.Text = row.Cells[0].Value.ToString();
                txtCMND_TCC_teacher.Text = row.Cells[6].Value.ToString();
                txtFull_name_teacher.Text = row.Cells[5].Value.ToString();
                txtMail_teacher.Text = row.Cells[9].Value.ToString();
                txtSDT_teacher.Text = row.Cells[8].Value.ToString();
                maskedTchDOB.Text = row.Cells[7].Value.ToString();
                Grade_CBB.Text = row.Cells[10].Value.ToString();
            }
        }

        static DataParameter _inputParameter = new DataParameter();

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (ControlAdmin.TabPages[ControlAdmin.SelectedIndex] == TabList_student || ControlAdmin.TabPages[ControlAdmin.SelectedIndex] == TabList_teacher)
            {
                BUS_Admin.btnExport_Click(_inputParameter, bgWorker_Export, dt_student, dt_teacher, ControlAdmin, TabList_student, TabList_teacher, pgBar);
            }
            else
            {
                return;
            }
        }

        private void bgWorker_Export_DoWork(object sender, DoWorkEventArgs e)
        {
            BUS_Admin.bgWorker_Export_DoWork(sender, e, bgWorker_Export);
        }

        private void bgWorker_Export_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BUS_Admin.bgWorker_Export_ProgressChanged(sender, e, pgBar, lbStatus);
        }

        private void bgWorker_Export_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BUS_Admin.bgWorker_Export_RunWorkerCompleted(sender, e, lbStatus);
        }
    }
}
