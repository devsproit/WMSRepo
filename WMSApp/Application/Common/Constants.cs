namespace Application.Common
{
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

        public const string GetAllCustomerSP = @"[WMS].[CustomerGetAll]";
        public const string GetCustomerIdSP = @"[WMS].[CustomerGetById]";
        public const string UpdateCustomerByIdSP = @"[WMS].[CustomerUpdate]";
        public const string CreateNewCustomerSP = @"[WMS].[CustomerInsert]";
        public const string DeleteCustomerByIdSP = @"sp";

        public const string GetAllItemSP = @"[WMS].[ItemGetAll]";
        public const string GetItemIdSP = @"[WMS].[ItemGetById]";
        public const string UpdateItemByIdSP = @"[WMS].[ItemUpdate]";
        public const string CreateNewItemSP = @"[WMS].[ItemInsert]";
        public const string DeleteItemByIdSP = @"sp";

        public const string GetAllSubItemSP = @"[WMS].[SubItemGetAll]";
        public const string GetSubItemIdSP = @"[WMS].[SubItemGetById]";
        public const string UpdateSubItemByIdSP = @"[WMS].[SubItemUpdate]";
        public const string CreateNewSubItemSP = @"[WMS].[SubItemInsert]";
        public const string DeleteSubItemByIdSP = @"sp";
        public const string GetSubItemMaterialDesc = @"[WMS].[SubItemGetMaterialDescription]";

        public const string CreateNewIntrasItSP = @"[WMS].[IntrasitInsert]";
        public const string GetAllIntrasitSP = @"[WMS].[IntrasitGetAll]";
        public const string BulkImportintransit = @"[dbo].[spBulkImportIntransit]";
        public const string DeleteIntasitByIdSP = @"[WMS].[IntrasitDelete]";


        public const string GetAllSenderCompanySP = @"[WMS].[SenderGetAll]";
        public const string GetCustomerByNameSP = @"[WMS].[CustomerGetByName]";
        public const string GetSubItemCustAmtSP = @"[WMS].[SubItemGetByName]";


    }
}