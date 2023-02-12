using DeepShi.Utilities.CustomValidator;
using DeepShiEntityModels.Models;
using DeepShiShared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepShi.ViewModels
{
    public class RegisterViewModel
    {
        public RegisterModel Appusers { get; set; }

        [Remote(action: "IsEmailInUse", controller: "Account")]
        [ValidEmailDomain(allowedDomain: "DeepShi.in", ErrorMessage = "Email domain must be DeepShi.in")]
        public string EmailId { get; set; }
        public string RegisterBy { get; set; }
        public List<RegisterModel> AppUserList { get; set; }
    }
}
