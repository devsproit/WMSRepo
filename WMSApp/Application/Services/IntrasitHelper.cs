using Application.Common;
using DatabaseLibrary;
using DatabaseLibrary.SQL;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class IntrasitHelper : IIntrasitHelper
    {
        private readonly IAdoConnection _adoConnection;

        public IntrasitHelper(IAdoConnection adoConnection)
        {
            _adoConnection = adoConnection;
        }

        public void blukUpload(DataTable ds)
        {
            _adoConnection.bulkImport(Constants.BulkImportintransit, ds);
        }

        public bool CreateNewIntrasit(IntrasitDb intrasit)
        {
            int result = 0;
            List<SqlParameter> sqlParameters;
            if (intrasit != null)
            {
                sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@LoginBranchId", intrasit.Login_Branch),
                    new SqlParameter("@SenderCompanyId", intrasit.Sender_Company),
                    new SqlParameter("@Branch", intrasit.Branch),
                    new SqlParameter("@PurchaseOrder", intrasit.PurchaseOrder),
                    new SqlParameter("@ItemCode", intrasit.Item_Code),
                    new SqlParameter("@SubItemCode", intrasit.SubItem_Code),
                    new SqlParameter("@SubItemName", intrasit.SubItem_Name),
                    new SqlParameter("@MaterialDescription", intrasit.MaterialDescription),
                    new SqlParameter("@Qty", intrasit.Qty),
                    new SqlParameter("@Unit", intrasit.Unit),
                    new SqlParameter("@Amt", intrasit.Amt),
                };
                result = _adoConnection.InsertUpdateWithSP(Constants.CreateNewIntrasItSP, sqlParameters);
            }
            return result > 0 ? true : false;
        }

        public bool DeleteIntrasitById(int Id)
        {
            throw new NotImplementedException();
        }

        public List<BranchDb> GetAllBranches()
        {
            List<BranchDb> data = new List<BranchDb>();
            DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetAllBranchSP);
            if (dbDT != null)
                data = dbDT.ToList<BranchDb>();
            return data.ToList();
        }

        public List<CompanyDb> GetAllCompany()
        {
            List<CompanyDb> data = new List<CompanyDb>();
            DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetAllCompaniesSP);
            if (dbDT != null)
                data = dbDT.ToList<CompanyDb>();
            return data.ToList();
        }

        public List<IntrasitDb> GetAllIntrasit()
        {
            List<IntrasitDb> data = new List<IntrasitDb>();
            DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetAllIntrasitSP);
            if (dbDT != null)
                data = dbDT.ToList<IntrasitDb>();
            return data.ToList();
        }

        public List<ItemDb> GetAllItem()
        {
            List<ItemDb> data = new List<ItemDb>();
            DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetAllItemSP);
            if (dbDT != null)
                data = dbDT.ToList<ItemDb>();
            return data.ToList();
        }

        public IntrasitDb GetIntrasitById(int Id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateIntrasitById(IntrasitDb company)
        {
            throw new NotImplementedException();
        }

        public List<SubItemDb> GetSubItem(int itemCode)
        {
            List<SubItemDb> data = new List<SubItemDb>();
            List<SqlParameter> sqlParameters;
            sqlParameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Id", itemCode),

                };

            DataTable dbDT = _adoConnection.GetDatatableFromSqlWithSP(Constants.GetSubItemIdSP, sqlParameters);
            if (dbDT != null)
                data = dbDT.ToList<SubItemDb>();
            return data.ToList();
        }

        
        

    }
}
