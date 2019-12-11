using Quan_Ly_Thi.DAO;
using Quan_Ly_Thi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Thi.BUS
{
    public class BUS_Admin
    {
        public static List<Giao_Vienn> layDanhSachGiaoVien()
        {
            return DAO_Admin.layDanhSachGiaoVien();
        }

        public static List<Hoc_Sinhh> layDanhSachHocSinh()
        {
            return DAO_Admin.layDanhSachHocSinh();
        }
    }
}
