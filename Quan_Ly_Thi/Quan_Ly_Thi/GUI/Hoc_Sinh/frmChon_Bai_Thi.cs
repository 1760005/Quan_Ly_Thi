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

namespace Quan_Ly_Thi.GUI.Hoc_Sinh
{
    public partial class frmChon_Bai_Thi : Form
    {
        public static string lop;

        public frmChon_Bai_Thi()
        {
            InitializeComponent();
        }

        private void frmChon_Bai_Thi_Load(object sender, EventArgs e)
        {
            List<string> Kythi = new List<string>();
            if (this.Text.Contains("Thử"))
            {
                Kythi = BUS_De_Thi.DanhSach_KyThi(BUS_Hoc_Sinh.ID_Khoi(lop), "LKT000002");
            }
            else
            {
                Kythi = BUS_De_Thi.DanhSach_KyThi(BUS_Hoc_Sinh.ID_Khoi(lop), "LKT000001");
            }
        
            cbb_Ky_Thi.DataSource = Kythi;
        }

        //cập nhật Môn THi
        private void cbb_Mon_TextChanged(object sender, EventArgs e)
        {
            cbb_MaDe.DataSource = BUS_De_Thi.DanhSach_MaDe(cbb_Mon.Text , BUS_De_Thi.Ma_Ky_Thi(cbb_Ky_Thi.Text));
        }

        //Lấy Mã Đề Thi
        private void cbb_Ky_Thi_TextChanged(object sender, EventArgs e)
        {
            cbb_Mon.DataSource = BUS_De_Thi.DanhSach_Mon(cbb_Ky_Thi.Text);
        }

        //Chọn Đề
        private void btn_Chon_Click(object sender, EventArgs e)
        {
            frmHoc_Sinh._ma_de_thi_ = cbb_MaDe.Text;
            frmHoc_Sinh._ma_ky_thi_ = BUS_De_Thi.Ma_Ky_Thi(cbb_Ky_Thi.Text);
            this.Close();
        }
    }
}
