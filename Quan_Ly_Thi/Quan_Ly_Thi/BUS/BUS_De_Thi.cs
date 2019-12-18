using Quan_Ly_Thi.DAO;
using Quan_Ly_Thi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Thi.BUS
{
    public class BUS_De_Thi
    {
        public static De_thi Lay_De_Thi_(string Ma_De)
        {
            return DAO_De_Thi.Lay_De_Thi_(Ma_De);
        }

        public static List<string> DanhSach_KyThi(string _ma_khoi_, string ma_loai_kt)
        {
            return DAO_De_Thi.DanhSach_KyThi(_ma_khoi_, ma_loai_kt);
        }

        public static List<string> DanhSach_Mon(string _TenKyThi_)
        {
            return DAO_De_Thi.DanhSach_Mon(_TenKyThi_);
        }

        public static List<string> DanhSach_MaDe(string _Ten_Mon_, string Ma_ky_Thi)
        {
            return DAO_De_Thi.DanhSach_MaDe(_Ten_Mon_, Ma_ky_Thi);
        }

        public static string Ma_Ky_Thi(string Ten_Ky_Thi)
        {
            return DAO_De_Thi.Ma_Ky_Thi(Ten_Ky_Thi);
        }

        public static int Thoi_Gian_Thi(string Ma_De_Thi)
        {
            return DAO_De_Thi.Thoi_Gian_Thi(Ma_De_Thi);
        }

        public static List<string> DanhSach_Mon()
        {
            List<string> Mon = new List<string>();
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from i in QLTTN.MONHOCs
                             select new { i.TenMonHoc };

                foreach (var item in Querry)
                {
                    Mon.Add(item.TenMonHoc);
                }
            }
            return Mon;
        }
        
        public static List<string> Danh_Sach_do_Kho()
        {
            List<string> do_kho = new List<string>();
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from i in QLTTN.CAPDOCAUHOIs
                             select new { i.TenCapDo };

                foreach (var item in Querry)
                {
                    do_kho.Add(item.TenCapDo);
                }
            }
            return do_kho;
        }

        public static List<string> Danh_Sach_Khoi()
        {
            List<string> Khoi = new List<string>();
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from i in QLTTN.KHOIs
                             select new { i.TenKhoi };

                foreach (var item in Querry)
                {
                    Khoi.Add(item.TenKhoi);
                }
            }
            return Khoi;
        }

        public static string ID_Mon(string Ten_mon)
        {
            string ID;
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from mon in QLTTN.MONHOCs
                             where mon.TenMonHoc == Ten_mon
                             select mon;
                ID = Querry.First().MaMonHoc;
            }
            return ID;
        }

        public static string ID_Khoi(string khoi)
        {
            string ID;
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from koi in QLTTN.KHOIs
                             where koi.TenKhoi == khoi
                             select koi;
                ID = Querry.First().MaKhoi;
            }
            return ID;
        }

        public static string ID_Do_Kho(string Kho)
        {
            string ID;
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from koi in QLTTN.CAPDOCAUHOIs
                             where koi.TenCapDo == Kho
                             select koi;
                ID = Querry.First().MaCapDo;
            }
            return ID;
        }
    }
}