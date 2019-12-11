using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quan_Ly_Thi.DAO;
using Quan_Ly_Thi.DTO;

namespace Quan_Ly_Thi.BUS
{
    public class BUS_Hoc_Sinh
    {
        public static List<Lich_Thi> Lay_lich_Thi(string Ma_Lop)
        {
            return DAO_Hoc_Sinh.Lay_lich_Thi(Ma_Lop);
        }
    }
}
