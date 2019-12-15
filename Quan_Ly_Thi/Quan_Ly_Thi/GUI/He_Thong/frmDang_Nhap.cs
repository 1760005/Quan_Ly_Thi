using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quan_Ly_Thi.DTO;
using Quan_Ly_Thi.BUS;
using Quan_Ly_Thi.GUI.Hoc_Sinh;
using Quan_Ly_Thi.GUI.Giao_Vien;
using Quan_Ly_Thi.GUI.Adminn;

namespace Quan_Ly_Thi.GUI.He_Thong
{
    public partial class frmDang_Nhap : Form
    {
        public frmDang_Nhap()
        {
            InitializeComponent();
        }

        //Đăng Ký
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            frmThong_Tin thong_Tin = new frmThong_Tin();
            thong_Tin.ShowDialog();
        }

        //Đăng Nhập
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (BUS_Tai_Khoan.Kiem_tra_Tai_Khoan(txtUserName.Text, txtPassWord.Text))
            {
                this.Hide();
                if (BUS_Tai_Khoan.Quyen(txtUserName.Text).Contains( "HS") )
                {
                    Hoc_Sinhh hoc_Sinh = BUS_Tai_Khoan.layThongTinTaiKhoan(txtUserName.Text) as Hoc_Sinhh ;
                    frmHoc_Sinh.hs = hoc_Sinh;
                    frmHoc_Sinh hs = new frmHoc_Sinh();
                    hs.ShowDialog();
                   
                }
                else if (BUS_Tai_Khoan.Quyen(txtUserName.Text).Contains("AD"))
                {
                    Tai_khoan tk = BUS_Tai_Khoan.layThongTinTaiKhoan(txtUserName.Text) as Tai_khoan;
                    frmAdmin.tk = tk;
                    frmAdmin admin = new frmAdmin();
                    admin.ShowDialog();

                }
                this.Show();
                txtPassWord.Clear();
                txtUserName.Clear();
            }
        }
    }
}
