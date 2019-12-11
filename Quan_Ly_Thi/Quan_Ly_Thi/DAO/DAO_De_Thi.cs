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
            var CauHoi = new Cau_Hoi();
            var De = new De_thi();
            De.Ma_De = Ma_De;

            using (var QLTTN = new QLTTNDataContext())
            {
                var Querry = from ct_dt in QLTTN.CHITIETDETHIs
                             join dt in QLTTN.DETHIs on ct_dt.MaDeThi equals dt.MaDeThi
                             join ch in QLTTN.CAUHOIs on ct_dt.MaCauHoi equals ch.MaCauHoi
                             where dt.MaDeThi == Ma_De
                             orderby ch.MaCauHoi
                             select new { dt, ch };

                foreach (var item in Querry)
                {
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
        
    }
}
