using Quan_Ly_Thi.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quan_Ly_Thi.DTO;

namespace Quan_Ly_Thi.BUS
{
    public class BUS_Tai_Khoan
    {
        public static Tai_khoan layThongTinTaiKhoan(string taiKhoan)
        {
            return DAO_Tai_Khoan.layThongTinTaiKhoan(taiKhoan);
        }
    }
}
