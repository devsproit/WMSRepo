using Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IIntrasitHelper
    {
        bool DeleteIntrasitById(int Id);
        List<IntrasitDb> GetAllIntrasit();
        IntrasitDb GetIntrasitById(int Id);
        bool UpdateIntrasitById(IntrasitDb intrasit);
        bool CreateNewIntrasit(IntrasitDb intrasit);
        void blukUpload(DataTable ds);

        List<Branch> GetAllBranches();

        List<CompanyDb> GetAllCompany();

        List<ItemDb>GetAllItem();
        List<SubItemDb> GetSubItem(int subItemId);

        string GetSubItemTitle(string subName);

        
    }
}
