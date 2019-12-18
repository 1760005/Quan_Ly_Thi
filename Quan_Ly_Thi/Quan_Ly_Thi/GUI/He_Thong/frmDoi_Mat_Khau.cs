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

namespace Quan_Ly_Thi.GUI.He_Thong
{
    public partial class frmDoi_Mat_Khau : Form
    {
        public static string Tai_Khoan; 

        public frmDoi_Mat_Khau()
        {
            InitializeComponent();
        }

        //đổi mật khẩu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (BUS_Tai_Khoan.Doi_mat_khau(Tai_Khoan,txtOldPass.Text,txtPassWord.Text))
            {
                MessageBox.Show("Đổi mật khẩu thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu không thành công", "Thông Báo",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
