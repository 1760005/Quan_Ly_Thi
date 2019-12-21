using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Thi.DTO
{
    public class Grades
    {
        public string GradeID { get; set; }
        public string GradeName { get; set; }

        public override string ToString()
        {
            return GradeName;
        }
    }
}
