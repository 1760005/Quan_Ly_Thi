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

        public static string ID_Lop(string Ten_Lop)
        {
            return DAO_Hoc_Sinh.ID_Lop(Ten_Lop);
        }

        public static string ID_Khoi(string Ten_Lop)
        {
            return DAO_Hoc_Sinh.ID_Khoi(Ten_Lop);
        }

        public static void Sua_Thong_Tin(Hoc_Sinhh hs_new)
        {
            DAO_Hoc_Sinh.Sua_Thong_Tin(hs_new);
        }

        public static bool Luu_Ket_Qua(KETQUATHI kqua)
        {
            using (var QLTTN = new  QLTTNDataContext() )
            {
                var Querry = (from kq in QLTTN.KETQUATHIs
                             where kq.MaKyThi == kqua.MaKyThi && kq.MaDeThi == kqua.MaDeThi && kq.TaiKhoan == kqua.TaiKhoan
                             select kq).Count();
                if (Querry == 0)
                {
                    DAO_Hoc_Sinh.Luu_Ket_Qua(kqua);
                    return true;
                }

                else
                {
                    return false;
                }
            }
        }

    }
}
