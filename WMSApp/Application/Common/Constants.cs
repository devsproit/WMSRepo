namespace Application.Common;
internal class Constants
{
    
    public const string GetAllCompaniesSP = @"[WMS].[CompanyGetAll]";
    public const string GetCompanyByIdSP = @"[WMS].[CompanyGetById]";
    public const string UpdateCompanyByIdSP = @"[WMS].[CompanyUpdate]";
    public const string CreateNewCompanySP = @"[WMS].[CompanyInsert]";
    public const string DeleteCompanyByIdSP = @"[WMS].[CompanyDeleteById]";

    public const string GetAllBranchSP = @"[WMS].[BranchGetAll]";
    public const string GetBranchIdSP = @"[WMS].[BranchGetById]";
    public const string UpdateBranchByIdSP = @"[WMS].[BranchUpdate]";
    public const string CreateNewBranchSP = @"[WMS].[BranchInsert]";
    public const string DeleteBranchByIdSP = @"sp";

    public const string GetAllLocationsSP = @"[WMS].[LocationGetAll]";
    public const string GetLocationIdSP = @"[WMS].[LocationGetById]";
    public const string UpdateLocationByIdSP = @"[WMS].[LocationUpdate]";
    public const string CreateNewLocationSP = @"[WMS].[LocationInsert]";
    public const string DeleteLocationByIdSP = @"sp";


}