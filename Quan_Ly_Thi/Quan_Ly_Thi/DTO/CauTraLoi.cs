using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Thi.DTO
{
    public class CauTraLoi
    {
        public int index { get; set; }
        public string cau_tra_loi { get; set; }
        public string Dap_An { get; set; }

        public void new_Cau_Tra_Loi(int index1, string cau_tra_loi1, string Dap_An1)
        {
            index = index1;
            cau_tra_loi = cau_tra_loi1;
            Dap_An = Dap_An1;
        }
    }
}
