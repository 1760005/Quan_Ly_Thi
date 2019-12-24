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
using Quan_Ly_Thi.GUI.He_Thong;
using Quan_Ly_Thi.Validator;

namespace Quan_Ly_Thi.GUI.Adminn
{
    public partial class frmAdmin : Form
    {
        static ConnectionStringSettings conStrSettings;
        static string Student_User_Account = null;
        static string Teacher_User_Account = null;
        static List<Classes> listClasses = new List<Classes>();
        static List<Grades> listGrades = new List<Grades>();
        public static Tai_khoan tk;
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
                else if (ControlAdmin.TabPages[ControlAdmin.SelectedIndex] == TabList_teacher)
                {
                    BUS_Admin.ImportTeacher(Path, dt_teacher);
                }
                else
                {
                    return;
                }
            }
        }

        private void btnList_Student_Click(object sender, EventArgs e)
        {
            dt_student.DataSource = null;
            dt_student.Columns.Clear();
            ControlAdmin.TabPages.Clear();
            ControlAdmin.TabPages.Add(TabList_student);

            BUS_Admin.GetListOfStudent(dt_student, conStrSettings, lbPage_student);
        }

        private void btnList_Teacher_Click(object sender, EventArgs e)
        {
            dt_teacher.DataSource = null;
            dt_teacher.Columns.Clear();
            ControlAdmin.TabPages.Clear();
            ControlAdmin.TabPages.Add(TabList_teacher);

            BUS_Admin.GetListOfTeacher(dt_teacher, conStrSettings, lbPage_teacher);
        }

        private void btnResult_Click(object sender, EventArgs e)
        {

            dt_Result.DataSource = null;
            dt_Result.Columns.Clear();
            ControlAdmin.TabPages.Clear();
            ControlAdmin.TabPages.Add(TabResult);
            dt_Result.DataSource = BUS_Admin.GetExaminationResult();
        }

        private void btnAdd_student_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                Hoc_Sinhh Student = new Hoc_Sinhh()
                {
                    Ho_Ten = txtFull_name_student.Text,
                    Tai_Khoan = txtUser_name_student.Text,
                    Mat_Khau = txtUser_name_student.Text,
                    CMND_TCC = txtCMND_TCC_student.Text,
                    Lop = listClasses[Class_CBB.SelectedIndex].ClassID,
                    Khoi = null,
                    Email = txtMail_student.Text,
                    SDT = txtSDT_student.Text,
                    Ngay_Sinh = DateTime.Parse(dtStudent_Picker.Value.ToString())
                };
                BUS_Admin.InsertStudent(Student);
                MessageBox.Show("Add Student Success!");
            }
            MessageBox.Show("Add Student Failed!");
            return;
        }

        private void btnAdd_teacher_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                Giao_Vienn Teacher = new Giao_Vienn()
                {
                    Ho_Ten = txtFull_name_teacher.Text,
                    Tai_Khoan = txtUserName_teacher.Text,
                    Mat_Khau = txtUserName_teacher.Text,
                    CMND_TCC = txtCMND_TCC_teacher.Text,
                    Khoi = listGrades[Grade_CBB.SelectedIndex].GradeID,
                    Lop = null,
                    Email = txtMail_teacher.Text,
                    SDT = txtSDT_teacher.Text,
                    Ngay_Sinh = DateTime.Parse(dtTeacher_Picker.Value.ToString())
                };
                BUS_Admin.InsertTeacher(Teacher);
                MessageBox.Show("Add Teacher Success!");
            }
            MessageBox.Show("Add Teacher Failed!");
            return;
        }

        private void btnRemove_student_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtUser_name_student.Text;
            BUS_Admin.DeleteUser(taiKhoan);
            MessageBox.Show("Delete Student Success");
        }

        private void btnRemove_teacher_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtUserName_teacher.Text;
            BUS_Admin.DeleteUser(taiKhoan);
            MessageBox.Show("Delete Teacher Success");
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
            MessageBox.Show("Back up Success");
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            OpenFileDialog folder = new OpenFileDialog();
            string Path = null;
            if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Path = folder.FileName;
            }
            BUS_Admin.RestoreDataBase(Path, conStrSettings);
            MessageBox.Show("Restore Success");
        }

        public frmAdmin()
        {
            InitializeComponent();
            ControlAdmin.TabPages.Clear();

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
                Lop = listClasses[Class_CBB.SelectedIndex].ClassID,
                Khoi = null,
                Email = txtMail_student.Text,
                SDT = txtSDT_student.Text,
                Ngay_Sinh = DateTime.Parse(dtStudent_Picker.Value.ToString())
            };

            BUS_Admin.UpdateStudent(Student, Student_User_Account);
        }

        private void dt_student_SelectionChanged(object sender, EventArgs e)
        {
            if (dt_student.SelectedRows.Count > 0)
            {
                btnAdd_student.Visible = false;
                DataGridViewRow row = dt_student.SelectedRows[0];
                Student_User_Account = row.Cells[0].Value.ToString();
                txtUser_name_student.Text = row.Cells[0].Value.ToString();
                txtCMND_TCC_student.Text = row.Cells[5].Value.ToString();
                txtFull_name_student.Text = row.Cells[4].Value.ToString();
                txtMail_student.Text = row.Cells[8].Value.ToString();
                txtSDT_student.Text = row.Cells[7].Value.ToString();
                dtStudent_Picker.Value = DateTime.Parse(row.Cells[6].Value.ToString());
                Class_CBB.Text = BUS_Admin.findClassByID(row.Cells[3].Value.ToString());
            }
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            listClasses = BUS_Admin.LoadClasses();
            Class_CBB.DataSource = listClasses;
            listGrades = BUS_Admin.LoadGrades();
            Grade_CBB.DataSource = listGrades;

            RegexValidator SearchInfomation_Student = new RegexValidator();
            SearchInfomation_Student.ControlToValidate = txtSearch_student;
            SearchInfomation_Student.ErrorMessage = "Infomation Incorrect, Correct Form: Lop 10A or Nguyen Van A";
            SearchInfomation_Student.Pattern = @"^[0-9A-Za-z][A-Za-z0-9\s]{1,60}$";

            RegexValidator SearchInfomation_Teacher = new RegexValidator();
            SearchInfomation_Teacher.ControlToValidate = txtSearch_teacher;
            SearchInfomation_Teacher.ErrorMessage = "Infomation Incorrect, Correct Form: Khoi 10 or Nguyen Van A";
            SearchInfomation_Teacher.Pattern = @"^[0-9A-Za-z][A-Za-z0-9\s]{1,60}$";

            RegexValidator TeacherUserName = new RegexValidator();
            TeacherUserName.ControlToValidate = txtUserName_teacher;
            TeacherUserName.ErrorMessage = "Teacher Account Incorrect, Correct Form: TK000001";
            TeacherUserName.Pattern = @"^[A-Z][A-Z0-9]{8,9}$";

            RegexValidator StudentUserName = new RegexValidator();
            StudentUserName.ControlToValidate = txtUser_name_student;
            StudentUserName.ErrorMessage = "Student Account Incorrect, Correct Form: TK000001";
            StudentUserName.Pattern = @"^[A-Z][A-Z0-9]{8,9}$";

            RegexValidator TeacherName = new RegexValidator();
            TeacherName.ControlToValidate = txtFull_name_teacher;
            TeacherName.ErrorMessage = "Teacher Name Incorrect, Correct Form: Nguyen Van A";
            TeacherName.Pattern = @"^[A-Z][A-Za-z\s]{1,60}$";

            RegexValidator StudentName = new RegexValidator();
            StudentName.ControlToValidate = txtFull_name_student;
            StudentName.ErrorMessage = "Student Name Incorrect, Correct Form: Nguyen Van A";
            StudentName.Pattern = @"^[A-Z][A-Za-z\s]{1,60}$";

            DateValidator StudentDOB = new DateValidator();
            StudentDOB.ControlToValidate = dtStudent_Picker;
            StudentDOB.ErrorMessage = "Date Incorrect, Correct Form: 01/01/1753";

            DateValidator TeacherDOB = new DateValidator();
            TeacherDOB.ControlToValidate = dtTeacher_Picker;
            TeacherDOB.ErrorMessage = "Date Incorrect, Correct Form: 01/01/1753";

            RegexValidator StudentEmail = new RegexValidator();
            StudentEmail.ControlToValidate = txtMail_student;
            StudentEmail.ErrorMessage = "Email Incorrect, Correct Form: abc123@abc.com";
            StudentEmail.Pattern = @"^[A-Za-z][a-zA-Z0-9_\.@]{13,40}$";

            RegexValidator TeacherEmail = new RegexValidator();
            TeacherEmail.ControlToValidate = txtMail_teacher;
            TeacherEmail.ErrorMessage = "Email Incorrect, Correct Form: abc123@abc.com";
            TeacherEmail.Pattern = @"^[a-z][a-zA-Z0-9_\.@]{13,40}$";

            RegexValidator StudentPhoneNumber = new RegexValidator();
            StudentPhoneNumber.ControlToValidate = txtSDT_student;
            StudentPhoneNumber.ErrorMessage = "PhoneNumber Incorrect, Correct Form: 0944686099";
            StudentPhoneNumber.Pattern = @"^[0-9]{10,15}$";

            RegexValidator TeacherPhoneNumber = new RegexValidator();
            TeacherPhoneNumber.ControlToValidate = txtSDT_teacher;
            TeacherPhoneNumber.ErrorMessage = "PhoneNumber Incorrect, Correct Form: 0944686099";
            TeacherPhoneNumber.Pattern = @"^[0-9]{10,15}$";

            RegexValidator StudentCMND_TCC = new RegexValidator();
            StudentCMND_TCC.ControlToValidate = txtCMND_TCC_student;
            StudentCMND_TCC.ErrorMessage = "PhoneNumber Incorrect, Correct Form: 123456789";
            StudentCMND_TCC.Pattern = @"^[0-9]{9,15}$";

            RegexValidator TeacherCMND_TCC = new RegexValidator();
            TeacherCMND_TCC.ControlToValidate = txtCMND_TCC_teacher;
            TeacherCMND_TCC.ErrorMessage = "PhoneNumber Incorrect, Correct Form: 123456789";
            TeacherCMND_TCC.Pattern = @"^[0-9]{9,15}$";

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
                Khoi = listGrades[Grade_CBB.SelectedIndex].GradeID,
                Email = txtMail_teacher.Text,
                SDT = txtSDT_teacher.Text,
                Ngay_Sinh = DateTime.Parse(dtTeacher_Picker.Value.ToString())
            };

            BUS_Admin.UpdateTeacher(Teacher, Teacher_User_Account);
        }

        private void btnStudent_Seach_Click(object sender, EventArgs e)
        {
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
                btnAdd_teacher.Visible = false;
                DataGridViewRow row = dt_teacher.SelectedRows[0];
                Teacher_User_Account = row.Cells[0].Value.ToString();
                txtUserName_teacher.Text = row.Cells[0].Value.ToString();
                txtCMND_TCC_teacher.Text = row.Cells[5].Value.ToString();
                txtFull_name_teacher.Text = row.Cells[4].Value.ToString();
                txtMail_teacher.Text = row.Cells[8].Value.ToString();
                txtSDT_teacher.Text = row.Cells[7].Value.ToString();
                dtTeacher_Picker.Value = DateTime.Parse(row.Cells[6].Value.ToString());
                Grade_CBB.Text = BUS_Admin.findGradeByID(row.Cells[2].Value.ToString());
            }
        }

        static DataParameter _inputParameter = new DataParameter();

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (ControlAdmin.SelectedIndex == -1)
            {
                return;
            }
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

        private void btnRefresh_student_Click(object sender, EventArgs e)
        {
            txtFull_name_student.Text = "";
            txtCMND_TCC_student.Text = "";
            txtMail_student.Text = "";
            txtSDT_student.Text = "";
            txtUser_name_student.Text = "";
            btnAdd_student.Visible = true;
        }

        private void btnRefresh_teacher_Click(object sender, EventArgs e)
        {
            txtFull_name_teacher.Text = "";
            txtCMND_TCC_teacher.Text = "";
            txtMail_teacher.Text = "";
            txtSDT_teacher.Text = "";
            txtUserName_teacher.Text = "";
            btnAdd_teacher.Visible = true;
        }

        private void btnChange_pass_Click(object sender, EventArgs e)
        {
            frmDoi_Mat_Khau.Tai_Khoan = tk.Tai_Khoan;
            frmDoi_Mat_Khau ChangePass = new frmDoi_Mat_Khau();
            ChangePass.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmDang_Nhap frm = new frmDang_Nhap();
            frm.Show();
            this.Close();

        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserReport Report = new frmUserReport();
            Report.ShowDialog();
        }
    }
}
