using Domain.Model;
using System.Collections.Generic;
using System.Linq;
using WMS.Core;
namespace Application.Services
{


    public interface ICompanyHelper
    {
        bool DeleteCompanyById(int Id);
        List<CompanyDb> GetAllCompanies();
        CompanyDb GetCompanyById(int Id);
        bool UpdateCompanyById(CompanyDb company);
        bool CreateNewCompany(CompanyDb company);
        void Insert(CompanyDb entity);
        IPagedList<CompanyDb> GetAllCOmpany(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}