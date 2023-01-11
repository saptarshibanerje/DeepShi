using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepShiEntityModels.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string EmployeeId { get; set; }
        public string Department { get; set; }
        public string EmployeeName { get; set; }
        public string EmpDivision { get; set; }
      
        public string EmpDesignation { get; set; }
    }
}
