using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Thi.GUI.Hoc_Sinh
{
    public partial class frmKetQua : Form
    {
        public static double Diem ;
        public frmKetQua()
        {
            InitializeComponent();
        }

        private void frmKetQua_Load(object sender, EventArgs e)
        {
            btn_KetQua.Text += Diem.ToString();
        }

        private void btn_KetQua_Click(object sender, EventArgs e)
        {
            frmDap_An da = new frmDap_An();
            da.ShowDialog();
        }
    }
}
