using Domain.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
namespace Application.Services.PO
{
    public interface IPurchaseOrder
    {
        void Insert(PurchaseOrderDb entiry);
        void Update(PurchaseOrderDb entiry);
        IPagedList<PurchaseOrderDb> GetPurchaseOrders(string branchCode, string category, bool status = false, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
