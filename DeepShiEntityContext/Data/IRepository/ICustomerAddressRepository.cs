using System;
using System.Collections.Generic;
using System.Text;
using DeepShiEntityModels.Models;
using DeepShiShared.Models;

namespace DeepShiEntityContext.Data.IRepository
{
    interface ICustomerAddressRepository
    {
        SqlResponseBaseModel AddUpdateCustomerAddress(CustomerAdress AddModel);
        SqlResponseBaseModel EditCustomerAddress(CustomerAdress UpdateModel);
        SqlResponseBaseModel DeleteCustomerAddress(CustomerAdress DeleteModel);
        IEnumerable<CustomerAdress> AllAddressByCustomerId(string customerid);
        IEnumerable<CustomerAdress> AllAddress();
        CustomerAdress GetCustomerAddressDetailsByAdrdressId(string addressid);
    }
}
