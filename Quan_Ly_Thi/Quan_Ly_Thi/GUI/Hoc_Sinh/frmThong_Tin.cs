using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quan_Ly_Thi.BUS;
using Quan_Ly_Thi.DTO;

namespace Quan_Ly_Thi.GUI.Hoc_Sinh
{
    public partial class frmThong_Tin : Form
    {
        public Hoc_Sinhh hs_new;

        private void Disable()
        {
            txtCMND.Enabled = false;
            txtStudent_Name.Enabled = false;
            txtStudent_email.Enabled = false;
            txtStudent_phone_number.Enabled = false;
            dpStudent_birth_date.Enabled = false;
        }

        private void Enable()
        {
            txtCMND.Enabled = true;
            txtStudent_Name.Enabled = true;
            txtStudent_email.Enabled = true;
            txtStudent_phone_number.Enabled = true;
            dpStudent_birth_date.Enabled = true;
        }

        public frmThong_Tin()
        {
            InitializeComponent();
        }

        private void frmThong_Tin_Load(object sender, EventArgs e)
        {
            Disable();
            txtSudent_code.Text = hs_new.Tai_Khoan;
            txtStudent_Name.Text = hs_new.Ho_Ten;
            txtCMND.Text = hs_new.CMND_TCC;
            txtStudent_email.Text = hs_new.Email;
            txtStudent_phone_number.Text = hs_new.SDT;
            dpStudent_birth_date.Value = hs_new.Ngay_Sinh;
            txtStudent_class.Text = hs_new.Lop;
        }

        //Sửa Thông Tin
        private void btnFix_Click(object sender, EventArgs e)
        {
            Enable();
        }

        //Lưu Thông Tin
        private void btnSave_Click(object sender, EventArgs e)
        {
            Disable();
            Hoc_Sinhh hs_new = new Hoc_Sinhh();
            hs_new.Tai_Khoan = txtSudent_code.Text;
            hs_new.Ho_Ten = txtStudent_Name.Text;
            hs_new.CMND_TCC = txtCMND.Text;
            hs_new.Email = txtStudent_email.Text;
            hs_new.SDT = txtStudent_phone_number.Text;
            hs_new.Ngay_Sinh = dpStudent_birth_date.Value;
            hs_new.Lop = txtStudent_class.Text;

            BUS_Hoc_Sinh.Sua_Thong_Tin(hs_new);

        }
    }
}
