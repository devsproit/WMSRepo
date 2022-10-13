using Domain.Model;
using System.Collections.Generic;
using System.Linq;
using WMS.Core;

namespace Application.Services

{


    public interface ISubItemHelper
    {
        bool DeleteSubItemById(int Id);
        List<SubItemDb> GetAllSubItem();
        SubItemDb GetSubItemById(int Id);
        bool UpdateSubItemById(SubItemDb SubItem);
        bool CreateNewSubItem(SubItemDb SubItem);
        List<SubItemDb> GetSubItemByItemId(int id);
        void Insert(SubItemDb entity);
        SubItemDb GetSubItemCustomerAmt(string subItemName,string type);
        SubItemDb GetItemByCOde(string subItemCode);

        IPagedList<SubItemDb> GetSubItem(string subItemCode, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}