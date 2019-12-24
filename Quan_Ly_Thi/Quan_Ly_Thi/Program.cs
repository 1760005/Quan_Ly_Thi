
using Quan_Ly_Thi.GUI.Hoc_Sinh;
using Quan_Ly_Thi.GUI.Adminn;
using Quan_Ly_Thi.GUI.He_Thong;
using Quan_Ly_Thi.GUI.Giao_Vien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quan_Ly_Thi.GUI.Hoc_Sinh.Report_Viewer;

using System.Windows.Forms;
namespace Quan_Ly_Thi
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmGiao_Vien());
        }
    }
}
