using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeepShiApp.Utilities.CustomValidator
{
    public class ValidEmailDomainAttribute : ValidationAttribute
    {
        private readonly string allowedDomain;

        public ValidEmailDomainAttribute(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }
        public override bool IsValid(object value)
        {
            string[] strs = value.ToString().Split('@');

            return strs[1].ToUpper() == allowedDomain.ToUpper();
        }
    }
}
