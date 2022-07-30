using Domain.Model.StockManagement;

using System.Collections.Generic;

using WMS.Core;

namespace Application.Services.StockMgnt
{
    public partial interface IItemStockService
    {
        void Insert(ItemStock entity);
        void Update(ItemStock entity);

        void Update(List<ItemStock> entities);
        IPagedList<ItemStock> GetItemStocks(string branchCode = "", string itemCode = "", int pageIndex = 0, int pageSize = int.MaxValue);
        ItemStock ItemByCode(string itemCode, string branchCode);
        ItemStock GetById(int id);

    }
}
