using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Thi.DTO
{
    public class Classes
    {
        public string ClassID { get; set; }
        public string ClassName { get; set; }

        public override string ToString()
        {
            return ClassName;
        }
    }
}
