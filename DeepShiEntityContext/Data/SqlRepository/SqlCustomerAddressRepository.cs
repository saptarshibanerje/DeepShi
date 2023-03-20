using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DeepShiEntityContext.Data.IRepository;
using DeepShiEntityModels.Models;
using DeepShiShared.Models;
using Microsoft.Data.SqlClient;

namespace DeepShiEntityContext.Data.SqlRepository
{
    public class SqlCustomerAddressRepository : ICustomerAddressRepository
    {
        public SqlResponseBaseModel Add(CustomerAdress AddModel)
        {
            SqlResponseBaseModel DBResponse = new SqlResponseBaseModel();
            SqlParameter[] sqlParameters = new SqlParameter[8];

            try
            {
                sqlParameters[0] = new SqlParameter("@CustomerId", SqlDbType.NVarChar, 50);
                sqlParameters[0].Value = AddModel.CustomerId;
            }
            catch (Exception ex)
            {
                DBResponse.ErrorCode = 1;
                DBResponse.Message = Convert.ToString(ex.InnerException.Message);
            }
            return DBResponse;
        }

        public IEnumerable<CustomerAdress> AllAddress(string customerid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerAdress> AllAddress()
        {
            throw new NotImplementedException();
        }

        public SqlResponseBaseModel Delete(CustomerAdress DeleteModel)
        {
            throw new NotImplementedException();
        }

        public SqlResponseBaseModel Edit(CustomerAdress UpdateModel)
        {
            throw new NotImplementedException();
        }

        public CustomerAdress GetAddressById(string addressid)
        {
            throw new NotImplementedException();
        }
    }
}
