using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Thi.Validator
{
    public class DateValidator : BaseValidator
    {
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }

        public bool KTNN(int year)
        {
            if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
            {
                return true;
            }
            return false;
        }

        public int DayOfMonth(int month, int year)
        {
            int NumDayOfMonth;
            if (month == 2)
            {
                if (KTNN(year))
                {
                    NumDayOfMonth = 29;
                }
                else
                {
                    NumDayOfMonth = 28;
                }
            }
            else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                NumDayOfMonth = 31;
            }
            else
            {
                NumDayOfMonth = 30;
            }
            return NumDayOfMonth;
        }

        public override bool Validate()
        {
            int day, month, year;
            bool checkday = false, checkmonth = false, checkyear = false;
            var Takens = ControlToValidate.Text.Split(new char[] { '/' }, StringSplitOptions.None);
            if (int.TryParse(Takens[0], out day) && int.TryParse(Takens[1], out month) && int.TryParse(Takens[2], out year))
            {
                checkday = day > 0 && day <= DayOfMonth(month, year);
                checkmonth = month > 0 && month < 13;
                checkyear = year > 0;

                return checkday && checkmonth && checkyear;
            }
            else
            {
                return checkday && checkmonth && checkyear;
            }
        }
    }
}
