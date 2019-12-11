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
    public partial class frmHoc_Sinh : Form
    {
        public Hoc_Sinhh hs;

        public frmHoc_Sinh()
        {
            InitializeComponent();
        }

        private void frmHoc_Sinh_Load(object sender, EventArgs e)
        {
            controlStudent.TabPages.Clear();
            hs = BUS_Tai_Khoan.layThongTinTaiKhoan("TK000003") as Hoc_Sinhh;
        }

        //thoát
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //xem lịch thi
        private void btnTest_day_Click(object sender, EventArgs e)
        {
            controlStudent.TabPages.Clear();
            controlStudent.TabPages.Add(Tab_test_day);
            controlStudent.SelectedTab = Tab_test_day;


            dt_test_day.DataSource = BUS_Hoc_Sinh.Lay_lich_Thi("LH000003");
        }

        //Xem Thông Tin Cá Nhân
        private void btnInformation_Click(object sender, EventArgs e)
        {
            frmThong_Tin thong_Tin = new frmThong_Tin();
            thong_Tin.hs_new = hs;
            thong_Tin.ShowDialog();

        }
    }
}
