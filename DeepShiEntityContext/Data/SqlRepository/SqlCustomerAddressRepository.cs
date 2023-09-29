using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DeepShiEntityContext.Data.IRepository;
using DeepShiEntityContext.DBContext;
using DeepShiEntityContext.Helper;
using DeepShiEntityModels.Models;
using DeepShiShared.Models;
using Microsoft.Data.SqlClient;

namespace DeepShiEntityContext.Data.SqlRepository
{
    public class SqlCustomerAddressRepository : ICustomerAddressRepository
    {
        public SqlResponseBaseModel AddUpdateCustomerAddress(CustomerAdress Model)
        {
            SqlResponseBaseModel DBResponse = new SqlResponseBaseModel();
            SqlParameter[] sqlParameters = new SqlParameter[15];

            try
            {
                sqlParameters[0] = new SqlParameter("@ActionId", SqlDbType.Int);
                sqlParameters[0].Value = Model.AddressId == 0 ? 1 : 2;

                sqlParameters[1] = new SqlParameter("@AddressId", SqlDbType.BigInt);
                sqlParameters[1].Value = Model.AddressId;

                sqlParameters[2] = new SqlParameter("@CustomerId", SqlDbType.BigInt);
                sqlParameters[2].Value = Model.CustomerId;

                sqlParameters[3] = new SqlParameter("@FirstName", SqlDbType.NVarChar, 100);
                sqlParameters[3].Value = Model.FirstName;

                sqlParameters[4] = new SqlParameter("@MiddleName", SqlDbType.NVarChar, 100);
                sqlParameters[4].Value = Model.MiddleName;

                sqlParameters[5] = new SqlParameter("@LastName", SqlDbType.NVarChar, 100);
                sqlParameters[5].Value = Model.LastName;

                sqlParameters[6] = new SqlParameter("@Address1", SqlDbType.NVarChar, 100);
                sqlParameters[6].Value = Model.Address1;

                sqlParameters[7] = new SqlParameter("@Address2", SqlDbType.NVarChar, 100);
                sqlParameters[7].Value = Model.Address2;

                sqlParameters[8] = new SqlParameter("@Address3", SqlDbType.NVarChar, 100);
                sqlParameters[8].Value = Model.Address3;

                sqlParameters[9] = new SqlParameter("@DistrictId", SqlDbType.BigInt);
                sqlParameters[9].Value = Model.DistrictId;

                sqlParameters[10] = new SqlParameter("@City", SqlDbType.NVarChar, 100);
                sqlParameters[10].Value = Model.City;

                sqlParameters[11] = new SqlParameter("@PinCode", SqlDbType.NVarChar, 10);
                sqlParameters[11].Value = Model.PinCode;

                sqlParameters[12] = new SqlParameter("@StateId", SqlDbType.BigInt);
                sqlParameters[12].Value = Model.StateId;

                sqlParameters[13] = new SqlParameter("@Landmark", SqlDbType.NVarChar, 15);
                sqlParameters[13].Value = Model.Landmark;

                sqlParameters[14] = new SqlParameter("@ContactNo", SqlDbType.NVarChar, 15);
                sqlParameters[14].Value = Model.ContactNo;

                DBHelper dbAccess = new DBHelper(UtilityHelper.GetConnectionstring());

                SqlResponseBaseModel dbresponse = UtilityHelper.ConvertDataTableToList<SqlResponseBaseModel>(dbAccess.ExecuteDataSetSP(SqlProcedures.SP_CustomerAddress, sqlParameters).Tables[0])[0];

            }
            catch (Exception ex)
            {
                DBResponse.ErrorCode = 1;
                DBResponse.Message = Convert.ToString(ex.InnerException.Message);
            }
            return DBResponse;
        }

        public IEnumerable<CustomerAdress> AllAddressByCustomerId(string customerid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerAdress> AllAddress()
        {
            throw new NotImplementedException();
        }

        public SqlResponseBaseModel DeleteCustomerAddress(CustomerAdress DeleteModel)
        {
            throw new NotImplementedException();
        }

        public SqlResponseBaseModel EditCustomerAddress(CustomerAdress UpdateModel)
        {
            throw new NotImplementedException();
        }

        public CustomerAdress GetCustomerAddressDetailsByAdrdressId(string addressid)
        {
            throw new NotImplementedException();
        }
    }
}
