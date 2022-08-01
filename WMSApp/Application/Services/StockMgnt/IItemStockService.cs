using Domain.Model.StockManagement;

using System.Collections.Generic;

using WMS.Core;

namespace Application.Services.StockMgnt
{
    public partial interface IItemStockService
    {
        void Insert(InventoryStock entity);
        void Update(InventoryStock entity);

        void Update(List<InventoryStock> entities);
        IPagedList<InventoryStock> GetItemStocks(int warehouse = 0, string itemCode = "", int pageIndex = 0, int pageSize = int.MaxValue);
        InventoryStock ItemByCode(string itemCode, int warehouse);
        InventoryStock GetById(int id);

    }
}
