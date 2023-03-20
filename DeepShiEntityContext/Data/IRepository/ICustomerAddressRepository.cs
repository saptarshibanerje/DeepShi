using System;
using System.Collections.Generic;
using System.Text;
using DeepShiEntityModels.Models;
using DeepShiShared.Models;

namespace DeepShiEntityContext.Data.IRepository
{
    interface ICustomerAddressRepository
    {
        SqlResponseBaseModel Add(CustomerAdress AddModel);
        SqlResponseBaseModel Edit(CustomerAdress UpdateModel);
        SqlResponseBaseModel Delete(CustomerAdress DeleteModel);
        IEnumerable<CustomerAdress> AllAddress(string customerid);
        IEnumerable<CustomerAdress> AllAddress();
        CustomerAdress GetAddressById(string addressid);
    }
}
