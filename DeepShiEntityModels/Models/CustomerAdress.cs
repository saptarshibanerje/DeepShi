using System;
using System.Collections.Generic;
using System.Text;

namespace DeepShiEntityModels.Models
{
    public class CustomerAdress : UserData
    {
        public int AddressId { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string CityOrDistrict { get; set; }
        public string PinCode { get; set; }
        public string State { get; set; }
        public string Landmark { get; set; }
        public string ContactNo { get; set; }
    }
}
