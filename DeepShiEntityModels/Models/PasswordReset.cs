using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepShiEntityModels.Models
{
    public class PasswordReset
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
        public string Token { get; set; }
    }
}
