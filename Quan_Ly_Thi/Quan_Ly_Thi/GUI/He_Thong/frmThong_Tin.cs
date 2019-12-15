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
using Quan_Ly_Thi.DAO;
namespace Quan_Ly_Thi.GUI.He_Thong
{
    public partial class frmThong_Tin : Form
    {

        public frmThong_Tin()
        {
            InitializeComponent();
        }

        private void frmThong_Tin_Load(object sender, EventArgs e)
        {
            //Quyền
            using (var QLTTN = new QLTTNDataContext())
            {
                foreach (var item in QLTTN.PHANQUYENs)
                {
                    cbb_Quyen.Items.Add(item.TenPhanQuyen);
                }
            }
            cbb_Quyen.SelectedIndex = 0;

            //Lớp
            using (var QLTTN = new QLTTNDataContext())
            {
                foreach (var item in QLTTN.LOPHOCs)
                {
                    txtStudent_class.AutoCompleteCustomSource.Add(item.TenLop);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            NGUOIDUNG nguoi_dung = new NGUOIDUNG();
            nguoi_dung.TaiKhoan = txtSudent_code.Text;
            nguoi_dung.MatKhau = txtPassword.Text;
            nguoi_dung.HoTen = txtStudent_Name.Text;
            nguoi_dung.CMND_TCC = txtCMND.Text;
            nguoi_dung.NgaySinh = dpStudent_birth_date.Value;
            if (txtStudent_class.Text != "")
            {
                nguoi_dung.MaKhoi = BUS_Tai_Khoan.ID_Khoi(txtStudent_class.Text);
                nguoi_dung.MaLop = BUS_Tai_Khoan.ID_Lop(txtStudent_class.Text);
               
            }
            nguoi_dung.MaPhanQuyen = BUS_Tai_Khoan.ID_Quyen(cbb_Quyen.Text);
            BUS_Tai_Khoan.Them_nguoi_dung(nguoi_dung);
        }
    }
}
