using Quan_Ly_Thi.DTO;
using Quan_Ly_Thi.BUS;
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
                var Querry1 = from ct_kt in QLTTN.CHITIETKYTHIs
                              where ct_kt.DETHI.KHOI.MaKhoi == ma_Khoi
                              select new {ct_kt.KYTHI.TenKyThi,ct_kt.DETHI.MONHOC.TenMonHoc,
                                  ct_kt.KYTHI.LOAIKYTHI.TenLoaiKyThi,ct_kt.ThoiGianKetThuc,ct_kt.ThoiGianBatDau };

                foreach (var item in Querry1)
                {
                    Lich_Thi k = new Lich_Thi();
                    k.TenKyThi = item.TenKyThi;
                    k.TenLoaiKyThi = item.TenLoaiKyThi;
                    k.TenMonHoc = item.TenMonHoc;
                    k.ThoiGianKetThuc = item.ThoiGianKetThuc.Value.ToString();
                    k.ThoiGianBatDau = item.ThoiGianBatDau.Value.ToString();
                    bool check = true;
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (k.TenKyThi == data[i].TenKyThi && k.TenLoaiKyThi == data[i].TenLoaiKyThi && k.TenMonHoc == data[i].TenMonHoc && k.ThoiGianKetThuc == data[i].ThoiGianKetThuc && k.ThoiGianBatDau == data[i].ThoiGianBatDau)
                        {
                            check = false;
                            break;
                        }
                    }
                    if (check == true)
                    {
                        data.Add(k);
                    }
                    check = true;

                }
                    return data;
            }
        }

        public static void Sua_Thong_Tin(Hoc_Sinhh hs_new)
        {
            using (var QLTTN = new QLTTNDataContext() )
            {
                var Querry = from _hs_ in QLTTN.NGUOIDUNGs
                             where _hs_.TaiKhoan == hs_new.Tai_Khoan
                             select _hs_;
                Querry.First().HoTen = hs_new.Ho_Ten;
                Querry.First().CMND_TCC = hs_new.CMND_TCC;
                Querry.First().Email = hs_new.Email;
                Querry.First().NgaySinh = hs_new.Ngay_Sinh;
                Querry.First().MaLop = BUS_Hoc_Sinh.ID_Lop(hs_new.Lop);
                QLTTN.SubmitChanges();
            }
        }

        public static string ID_Lop(string Ten_Lop)
        {
            string ID = null;
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from id in QLTTN.NGUOIDUNGs
                             join l in QLTTN.LOPHOCs on id.MaLop equals l.MaLop
                             where l.TenLop == Ten_Lop
                             select new { id.MaLop };
                ID = Querry.First().MaLop;
            }
            return ID;
        }

        public static string ID_Khoi(string Ten_Lop)
        {
            string ID = null;
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from id in QLTTN.NGUOIDUNGs
                             join l in QLTTN.LOPHOCs on id.MaLop equals l.MaLop
                             join k in QLTTN.KHOIs on l.MaKhoi equals k.MaKhoi
                             where l.TenLop == Ten_Lop
                             select new { k.MaKhoi };
                ID = Querry.First().MaKhoi;
            }
            return ID;
        }

        public static void Luu_Ket_Qua(KETQUATHI kqua)
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                QLTTN.KETQUATHIs.InsertOnSubmit(kqua);
                QLTTN.SubmitChanges();
            }
        }

        public static void Cap_Nhat_Ket_Qua(KETQUATHI kqua, double Diem)
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                var query = from q in QLTTN.KETQUATHIs
                            where q.TaiKhoan == kqua.TaiKhoan && q.MaKyThi == kqua.MaKyThi && q.MaDeThi == kqua.MaDeThi
                            select q;
                query.First().Diem = Diem;
                QLTTN.SubmitChanges();
            }
        }
    }
}
