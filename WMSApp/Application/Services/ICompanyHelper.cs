using Domain.Model;

namespace Application.Services;

public interface ICompanyHelper
{
    bool DeleteCompanyById(int Id);
    List<CompanyDb> GetAllCompanies();
    CompanyDb GetCompanyById(int Id);
    bool UpdateCompanyById(CompanyDb company);
    bool CreateNewCompany(CompanyDb company);
}