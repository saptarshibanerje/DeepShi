using DeepShi.Utilities.CustomValidator;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepShi.ViewModels
{
    public class ForgetPasswordViewModel
    {
        //[Remote(action: "IsEmailInUse", controller: "Account")]
        [ValidEmailDomain(allowedDomain: "DeepShi.in", ErrorMessage = "Email domain must be DeepShi.in")]
        public string EmailId { get; set; }
    }
}
