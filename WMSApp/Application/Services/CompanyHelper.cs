using System.Data;
using System.Data.SqlClient;
using Application.Common;
using DatabaseLibrary;
using DatabaseLibrary.SQL;
using Domain.Model;
using System.Collections.Generic;
using System.Linq;
using WMS.Core.Data;
using WMS.Core;
namespace Application.Services
{


    public class CompanyHelper : ICompanyHelper
    {
        private readonly IAdoConnection _adoConnection;
        private readonly IRepository<CompanyDb> _companyRepository;

        public CompanyHelper(IAdoConnection adoConnection, IRepository<CompanyDb> companyRepository)
        {
            _adoConnection = adoConnection;
            _companyRepository = companyRepository;
        }

        public List<CompanyDb> GetAllCompanies()
        {
            List<CompanyDb> data = new List<CompanyDb>();
            DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetAllCompaniesSP);
            if (dbDT != null)
                data = dbDT.ToList<CompanyDb>();
            return data;
        }

        public CompanyDb GetCompanyById(int Id)
        {
            List<CompanyDb> data = new List<CompanyDb>();
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@Id", Id));
            DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetCompanyByIdSP, sqlParameters);
            if (dbDT != null)
                data = dbDT.ToList<CompanyDb>();
            return data.FirstOrDefault();
        }
        public bool CreateNewCompany(CompanyDb company)
        {
            int result = 0;
            List<SqlParameter> sqlParameters = null;
            if (company != null)
            {
                sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@ScreenCode", company.ScreenCode),
                    new SqlParameter("@CompanyCode", company.CompanyCode),
                    new SqlParameter("@CompanyName", company.CompanyName),
                    new SqlParameter("@Address", company.Address),
                    new SqlParameter("@Location", company.Location),
                    new SqlParameter("@ContactPersonHO", company.ContactPersonHO),
                    new SqlParameter("@ContactNumberHO", company.ContactNumberHO),
                    new SqlParameter("@EmailIdHO", company.EmailIdHO),
                    new SqlParameter("@SpaceSizeFormat", company.SpaceSizeFormat),
                    new SqlParameter("@Items", company.Items),
                    new SqlParameter("@SubItem", company.SubItem),
                };
                result = _adoConnection.InsertUpdateWithSP(Constants.CreateNewCompanySP, sqlParameters);
            }
            return result > 0 ? true : false;
        }
        public bool UpdateCompanyById(CompanyDb company)
        {
            int result = 0;
            List<SqlParameter> sqlParameters = null;
            if (company != null)
            {
                sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Id", company.Id),
                    new SqlParameter("@ScreenCode", company.ScreenCode),
                    new SqlParameter("@CompanyCode", company.CompanyCode),
                    new SqlParameter("@CompanyName", company.CompanyName),
                    new SqlParameter("@Address", company.Address),
                    new SqlParameter("@Location", company.Location),
                    new SqlParameter("@ContactPersonHO", company.ContactPersonHO),
                    new SqlParameter("@ContactNumberHO", company.ContactNumberHO),
                    new SqlParameter("@EmailIdHO", company.EmailIdHO),
                    new SqlParameter("@SpaceSizeFormat", company.SpaceSizeFormat),
                    new SqlParameter("@Items", company.Items),
                    new SqlParameter("@SubItem", company.SubItem),
                };
                result = _adoConnection.InsertUpdateWithSP(Constants.UpdateCompanyByIdSP, sqlParameters);
            }
            return result > 0 ? true : false;
        }
        public bool DeleteCompanyById(int Id)
        {
            int result = 0;
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@Id", Id));
            result = _adoConnection.InsertUpdateWithSP(Constants.DeleteCompanyByIdSP, sqlParameters);
            return result > 0 ? true : false;
        }


        public virtual void Insert(CompanyDb entity)
        {
            _companyRepository.Insert(entity);
        }

        public virtual IPagedList<CompanyDb> GetAllCOmpany(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _companyRepository.Table
                        orderby x.CompanyName ascending
                        select x;
            var result = new PagedList<CompanyDb>(query, pageIndex, pageSize);
            return result;


        }
    }
}
