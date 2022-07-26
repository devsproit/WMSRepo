using Domain.Model.PO;
using WMS.Core;

namespace Application.Services.PO
{
    public interface IStockTransferPo
    {
        void Insert(StockTransferPoDb stockTransferPo);
        void Update(StockTransferPoDb stockTransferPo);

        IPagedList<StockTransferPoDb> GetStockTransferPos(string poNumber = "0", int pageIndex = 0, int pageSize = int.MaxValue);
    }
}