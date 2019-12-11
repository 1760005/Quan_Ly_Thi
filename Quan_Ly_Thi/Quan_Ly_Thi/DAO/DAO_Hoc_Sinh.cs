using Quan_Ly_Thi.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Thi.DAO
{
    public class DAO_Hoc_Sinh
    {
        public static List<Lich_Thi> Lay_lich_Thi(string Ma_Lop)
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from k in QLTTN.KHOIs
                             join l in QLTTN.LOPHOCs on k.MaKhoi equals l.MaKhoi
                             where l.MaLop == Ma_Lop
                             select new { k.MaKhoi };


                string ma_Khoi = Querry.First().MaKhoi.ToString() ;
                List<Lich_Thi> data = new List<Lich_Thi>();
        
                Querry = null;
                var Querry1 = from kt in QLTTN.KYTHIs
                              join l_kt in QLTTN.LOAIKYTHIs on kt.MaLoaiKyThi equals l_kt.MaLoaiKyThi
                              join ct_kt in QLTTN.CHITIETKYTHIs on kt.MaKyThi equals ct_kt.MaKyThi
                              join d in QLTTN.DETHIs on ct_kt.MaDeThi equals d.MaDeThi
                              join mh in QLTTN.MONHOCs on d.MaMonHoc equals mh.MaMonHoc
                              where d.MaKhoi == ma_Khoi
                              select new { kt.TenKyThi, l_kt.TenLoaiKyThi, mh.TenMonHoc, ct_kt.ThoiGianBatDau, ct_kt.ThoiGianKetThuc };
                foreach (var item in Querry1)
                {
                    Lich_Thi k = new Lich_Thi();
                    k.TenKyThi = item.TenKyThi;
                    k.TenLoaiKyThi = item.TenLoaiKyThi;
                    k.TenMonHoc = item.TenMonHoc;
                    k.ThoiGianKetThuc = item.ThoiGianKetThuc.Value.ToString();
                    k.ThoiGianBatDau = item.ThoiGianBatDau.Value.ToString();
                    data.Add(k);
                }
                return data;
            }
        }
    }
}
