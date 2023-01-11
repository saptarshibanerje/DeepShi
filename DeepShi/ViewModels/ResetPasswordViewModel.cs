using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeepShiApp.ViewModels
{
    public class ResetPasswordViewModel
    {

        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password and Confirm password must be same")]
        public string ConfirmPassword { get; set; }
        public string OldPassword { get; set; }
        public string Token { get; set; }
    }
}
