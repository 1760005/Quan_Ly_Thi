using Quan_Ly_Thi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Thi.DAO
{
    public class DAO_De_Thi
    {
        public static De_thi Lay_De_Thi_(string Ma_De)
        {
            
            var De = new De_thi();
            De.Ma_De = Ma_De;

            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from ct_dt in QLTTN.CHITIETDETHIs
                             join dt in QLTTN.DETHIs on ct_dt.MaDeThi equals dt.MaDeThi
                             join ch in QLTTN.CAUHOIs on ct_dt.MaCauHoi equals ch.MaCauHoi
                             where dt.MaDeThi == Ma_De
                             select new { dt, ch  };

                De.De = new List<Cau_Hoi>();
                foreach (var item in Querry)
                {
                    var CauHoi = new Cau_Hoi();
                    CauHoi.noi_dung = item.ch.CauHoi1;
                    CauHoi.Dap_An = item.ch.DapAn;
                    CauHoi.Cau_A = item.ch.CauA;
                    CauHoi.Cau_B = item.ch.CauB;
                    CauHoi.Cau_C = item.ch.CauC;
                    CauHoi.Cau_D = item.ch.CauD;
                    De.De.Add(CauHoi);
                }
            }

            return De;
        }

        public static List<string> DanhSach_KyThi(string _ma_khoi_,string maLoai)
        {
            List<string> DS_Ten_KyThi = new List<string>();
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from kt in QLTTN.KYTHIs
                             join ct_kt in QLTTN.CHITIETKYTHIs on kt.MaKyThi equals ct_kt.MaKyThi
                             join D in QLTTN.DETHIs on ct_kt.MaDeThi equals D.MaDeThi
                             join l in QLTTN.LOAIKYTHIs on kt.MaLoaiKyThi equals l.MaLoaiKyThi
                             where D.MaKhoi == _ma_khoi_ && l.MaLoaiKyThi == maLoai
                             && ct_kt.ThoiGianBatDau <= DateTime.Now && ct_kt.ThoiGianKetThuc >= DateTime.Now
                             select new { kt.TenKyThi };
                foreach (var item in Querry )
                {
                    DS_Ten_KyThi.Add(item.TenKyThi);
                }
                return DS_Ten_KyThi;
            }

        }

        public static List<string> DanhSach_Mon(string _TenKyThi_)
        {
            List<string> DS_Mon = new List<string>();
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from kt in QLTTN.KYTHIs
                             join ct_kt in QLTTN.CHITIETKYTHIs on kt.MaKyThi equals ct_kt.MaKyThi
                             join D in QLTTN.DETHIs on ct_kt.MaDeThi equals D.MaDeThi
                             join M in QLTTN.MONHOCs on D.MaMonHoc equals M.MaMonHoc
                             where kt.TenKyThi == _TenKyThi_
                             && ct_kt.ThoiGianBatDau <= DateTime.Now && ct_kt.ThoiGianKetThuc >= DateTime.Now
                             select new {M.TenMonHoc  };
                foreach (var item in Querry)
                {
                    DS_Mon.Add(item.TenMonHoc);
                }
                return DS_Mon;
            }

        }

        public static List<string> DanhSach_MaDe(string _Ten_Mon_ ,string Ma_ky_Thi)
        {
            List<string> DS_MaDe = new List<string>();
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from kt in QLTTN.KYTHIs
                             join ct_kt in QLTTN.CHITIETKYTHIs on kt.MaKyThi equals ct_kt.MaKyThi
                             join D in QLTTN.DETHIs on ct_kt.MaDeThi equals D.MaDeThi
                             join M in QLTTN.MONHOCs on D.MaMonHoc equals M.MaMonHoc
                             where M.TenMonHoc == _Ten_Mon_ && kt.MaKyThi == Ma_ky_Thi
                             && ct_kt.ThoiGianBatDau <= DateTime.Now && ct_kt.ThoiGianKetThuc >= DateTime.Now
                             select new { D.MaDeThi };
                foreach (var item in Querry)
                {
                    DS_MaDe.Add(item.MaDeThi);
                }
                return DS_MaDe;
            }

        }

        public static string Ma_Ky_Thi(string Ten_Ky_Thi)
        {
            if (string.IsNullOrEmpty(Ten_Ky_Thi))
            {
                return null;
            }
            using (var QLTTN = new QLTTNDataContext())
            {
                var QUerry = from id in QLTTN.KYTHIs
                             where id.TenKyThi == Ten_Ky_Thi
                             select new { id.MaKyThi };
                return QUerry.First().MaKyThi;
            }
        }

        public static int Thoi_Gian_Thi(string Ma_De_Thi)
        {
            int ThoiGian = 0;
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from tgian in QLTTN.DETHIs
                             where tgian.MaDeThi == Ma_De_Thi
                             select new { tgian.ThoiGianLamBai };
                ThoiGian = int.Parse(Querry.First().ThoiGianLamBai.ToString());
            }
            return ThoiGian;
        }
    }
}
