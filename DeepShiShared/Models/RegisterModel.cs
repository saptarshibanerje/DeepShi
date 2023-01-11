using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepShiShared.Models
{
    public class RegisterModel
    {
        public string EmailId { get; set; }
        public string EmployeeId { get; set; }
        public string Department { get; set; }
        public string Password { get; set; }
        public string EmpName { get; set; }
        public string EmpDivision { get; set; }
        public string EmpDesignation { get; set; }
        public string IsConfirmedEmail { get; set; }
    }
}
