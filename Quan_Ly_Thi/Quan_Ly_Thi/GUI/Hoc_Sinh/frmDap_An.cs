using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quan_Ly_Thi.DTO;

namespace Quan_Ly_Thi.GUI.Hoc_Sinh
{
    public partial class frmDap_An : Form
    {


        public static List<CauTraLoi> DS_Cau_Tra_Loi;
        public frmDap_An()
        {
            InitializeComponent();
            dt_dap_an.DataSource = DS_Cau_Tra_Loi;
            for (int i = 0; i <  DS_Cau_Tra_Loi.Count;i++)
            {
                dt_dap_an.Rows[i].Cells[0].Value = DS_Cau_Tra_Loi[i].index + 1;

            }
        }
    }
}
