using System;
using System.Collections.Generic;
using System.Text;

namespace DeepShiEntityModels.Models
{
    public class CustomerAdress : UserData
    {
        public int AddressId { get; set; }
        public Int32 CustomerId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }       
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public int DistrictId { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public int StateId { get; set; }
        public string Landmark { get; set; }
        public string ContactNo { get; set; }
    }
}
