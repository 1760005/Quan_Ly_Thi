using Quan_Ly_Thi.BUS;
using Quan_Ly_Thi.DTO;
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

namespace Quan_Ly_Thi.GUI.Adminn
{
    public partial class frmAdmin : Form
    {

        static ConnectionStringSettings conStrSettings;
        static DbConnection Connection;
        static DbProviderFactory factory;
        static DbDataAdapter Students, Teachers, Users, Datas;

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
            dt_student.DataSource = BUS_Admin.layDanhSachHocSinh();
        }

        private void btnList_Teacher_Click(object sender, EventArgs e)
        {
            dt_teacher.DataSource = BUS_Admin.layDanhSachGiaoVien();
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
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
            BUS_Admin.BackupDataBase(Path, factory, Datas, Connection);
        }

        System.Data.DataTable StdTable, TchTable;
        public frmAdmin()
        {
            InitializeComponent();
            ControlAdmin.TabPages.Clear();
            //AdminUser = Admin;

            Text = ConfigurationManager.AppSettings["title"];
            conStrSettings = ConfigurationManager.ConnectionStrings["Quan_Ly_Thi.Properties.Settings.QuanLyThiTracNghiemDBConnectionString2"];

            factory = DbProviderFactories.GetFactory(conStrSettings.ProviderName);
            Connection = factory.CreateConnection();
            Connection.ConnectionString = conStrSettings.ConnectionString;
        }
    }
}
