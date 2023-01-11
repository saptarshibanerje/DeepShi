using System;
using System.Collections.Generic;
using System.Text;

namespace DeepShiShared.Models
{
    public class LoginResponse
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string CompanyCode { get; set; }
        public string DivisionCode { get; set; }
        public string DepartmentCode { get; set; }
        public List<string> UserType { get; set; }
        public string RedirectUrl { get; set; }
        public string EmailId { get; set; }
        public string AuthToken { get; set; }
        public byte[] UserPhoto { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }

        public LoginResponse()
        {
            UserType = new List<string>();
        }

    }
}
