using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quan_Ly_Thi.GUI.Hoc_Sinh.Report_Viewer;
using Quan_Ly_Thi.BUS;

namespace Quan_Ly_Thi.GUI.Hoc_Sinh
{
    public partial class frmChon_De_On : Form
    {
        public frmChon_De_On()
        {
            InitializeComponent();
        }

        private void btn_Chon_Click(object sender, EventArgs e)
        {
            De_On de = new De_On();
            De_On._ma_khoi_ = BUS_De_Thi.ID_Khoi(cbb_Khoi.Text);
            De_On._do_kho_ = BUS_De_Thi.ID_Do_Kho(cbb_DoKho.Text);
            De_On._ma_mon_hoc_ = BUS_De_Thi.ID_Mon(cbb_MonHoc.Text);
            de.ShowDialog();
        }

        private void frmChon_De_On_Load(object sender, EventArgs e)
        {
            cbb_MonHoc.Items.Clear();
            cbb_DoKho.Items.Clear();
            cbb_Khoi.Items.Clear();


            foreach (var item in BUS_De_Thi.Danh_Sach_do_Kho())
            {
                cbb_DoKho.Items.Add(item);
            }


            foreach (var item in BUS_De_Thi.DanhSach_Mon())
            {
                cbb_MonHoc.Items.Add(item);
            }

            foreach (var item in BUS_De_Thi.Danh_Sach_Khoi())
            {
                cbb_Khoi.Items.Add(item);
            }

            cbb_Khoi.SelectedIndex = 0;
            cbb_MonHoc.SelectedIndex = 0;
            cbb_DoKho.SelectedIndex = 0;
        }
    }
}
