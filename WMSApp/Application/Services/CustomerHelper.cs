using System.Data;
using System.Data.SqlClient;
using Application.Common;
using DatabaseLibrary;
using DatabaseLibrary.SQL;
using Domain.Model;
using System.Collections.Generic;
using System.Linq;
namespace Application.Services
{


    public class CustomerHelper : ICustomerHelper
    {
        private readonly IAdoConnection _adoConnection;

        public CustomerHelper(IAdoConnection adoConnection)
        {
            _adoConnection = adoConnection;
        }

        public List<CustomerDb> GetAllCustomer()
        {
            List<CustomerDb> data = new List<CustomerDb>();
            DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetAllCustomerSP);
            if (dbDT != null)
                data = dbDT.ToList<CustomerDb>();
            return data;
        }

        public CustomerDb GetCustomerById(int Id)
        {
            List<CustomerDb> data = new List<CustomerDb>();
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@Id", Id));
            DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetCustomerIdSP, sqlParameters);
            if (dbDT != null)
                data = dbDT.ToList<CustomerDb>();
            return data.FirstOrDefault();
        }
        public bool CreateNewCustomer(CustomerDb Customer)
        {
            int result = 0;
            List<SqlParameter> sqlParameters = null;
            if (Customer != null)
            {
                sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@ScreenCode", Customer.ScreenCode),
                    new SqlParameter("@CustomerCategory", Customer.CustomerCategory),
                    new SqlParameter("@CustomerName", Customer.CustomerName),
                    new SqlParameter("@CustomerCode", Customer.CustomerCode),
                    new SqlParameter("@Location", Customer.Location),

                };
                result = _adoConnection.InsertUpdateWithSP(Constants.CreateNewCustomerSP, sqlParameters);
            }
            return result > 0 ? true : false;
        }
        public bool UpdateCustomerById(CustomerDb Customer)
        {
            int result = 0;
            List<SqlParameter> sqlParameters = null;
            if (Customer != null)
            {
                sqlParameters = new List<SqlParameter>()
                {
                new SqlParameter("@Id", Customer.Id),
                   new SqlParameter("@ScreenCode", Customer.ScreenCode),
                    new SqlParameter("@CustomerCategory", Customer.CustomerCategory),
                    new SqlParameter("@CustomerName", Customer.CustomerName),
                    new SqlParameter("@CustomerCode", Customer.CustomerCode),
                    new SqlParameter("@Location", Customer.Location),

                };
                result = _adoConnection.InsertUpdateWithSP(Constants.UpdateCustomerByIdSP, sqlParameters);
            }
            return result > 0 ? true : false;
        }
        public bool DeleteCustomerById(int Id)
        {
            int result = 0;
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@Id", Id));
            result = _adoConnection.InsertUpdateWithSP(Constants.DeleteCustomerByIdSP, sqlParameters);
            return result > 0 ? true : false;
        }

        public List<CustomerDb> GetCustomerByName(string cname)
        {
            List<CustomerDb> data = new List<CustomerDb>();
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@cuscategory", cname));
            DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetCustomerByNameSP, sqlParameters);
            if (dbDT != null)
                data = dbDT.ToList<CustomerDb>();
            return data;
        }
    }
}