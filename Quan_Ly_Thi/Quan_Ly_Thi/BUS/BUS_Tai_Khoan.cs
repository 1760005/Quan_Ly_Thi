using Quan_Ly_Thi.DAO;
using Quan_Ly_Thi.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Thi.BUS
{
    public class BUS_Tai_Khoan
    {
        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X1"));

            return sb.ToString();
        }

        public static Tai_khoan layThongTinTaiKhoan(string taiKhoan)
        {
            return DAO_Tai_Khoan.layThongTinTaiKhoan(taiKhoan);
        }

        public static bool Kiem_tra_Tai_Khoan(string taiKhoan, string matKhau)
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from q in QLTTN.NGUOIDUNGs
                             where q.TaiKhoan == taiKhoan
                             select new { q.TaiKhoan, q.MatKhau };
                if (Querry.Count() == 0)
                {
                    MessageBox.Show("Tài khoản không tồn tại", "Thông Báo");
                    return false;
                }
                else
                {
                    if (Querry.First().TaiKhoan == taiKhoan && Querry.First().MatKhau == BUS_Tai_Khoan.GetHashString( matKhau))
                    {
                        return true;
                    }
                    MessageBox.Show("Sai Tài Khoản Hoặc Mật Khẩu", "Thông Báo");
                    return false;
                }
            }
        }

        public static string Quyen(string TaiKhoan)
        {
            return DAO_Tai_Khoan.Quyen(TaiKhoan);
        }

        public static void Them_nguoi_dung(NGUOIDUNG nguoi_dung, string _loi_ = null)
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from ng_dung in QLTTN.NGUOIDUNGs
                             where ng_dung.TaiKhoan == nguoi_dung.TaiKhoan
                             select ng_dung;
                if (Querry.Count() > 0)
                {
                    MessageBox.Show("Tài Khoản Đã Tồn Tại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Querry.Count() == 0 && _loi_ == null)
                { 
                    nguoi_dung.MatKhau = GetHashString(nguoi_dung.MatKhau);
                    DAO_Tai_Khoan.Them_Nguoi_Dung(nguoi_dung);
                    MessageBox.Show("Đăng Ký thành công ^_^", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public static string ID_Lop(string Ten_lop)
        {
            return DAO_Tai_Khoan.ID_Lop(Ten_lop);
        }

        public static string ID_Khoi(string TenKhoi)
        {
            return DAO_Tai_Khoan.ID_Khoi(TenKhoi);
        }

        public static string ID_Quyen(string Ten_Quyen)
        {
            return DAO_Tai_Khoan.ID_Quyen(Ten_Quyen);
        }

        public static bool Doi_mat_khau(string TaiKhoan, string Mat_Khau, string Mat_khau_moi)
        {
            using (var QLTTN = new QLTTNDataContext())
            {
                    var Querry = from p in QLTTN.NGUOIDUNGs
                                 where p.TaiKhoan == TaiKhoan
                                 select p;

                if (Querry.First().MatKhau == BUS_Tai_Khoan.GetHashString( Mat_Khau))
                {
                    DAO_Tai_Khoan.Doi_Mat_Khau(TaiKhoan, BUS_Tai_Khoan.GetHashString( Mat_khau_moi));
                    return true;
                }
                return false;
            }

        }


    }
}
