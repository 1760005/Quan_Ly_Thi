using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Quan_Ly_Thi.Validator
{
    public class RegexValidator : BaseValidator
    {
        public string Pattern { get; set; }
        public override bool Validate()
        {
            var Re = new Regex(Pattern);
            return Re.IsMatch(ControlToValidate.Text);
        }
    }
}
