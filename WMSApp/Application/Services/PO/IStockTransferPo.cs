using Domain.Model.PO;
using WMS.Core;

namespace Application.Services.PO
{
    public interface IStockTransferPo
    {
        void Insert(StockTransferPoDb stockTransferPo);
        void Update(StockTransferPoDb stockTransferPo);
        StockTransferPoDb GetById(int id);
        IPagedList<StockTransferPoDb> GetDetails(string category,int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<StockTransferPoDb> GetStockTransferPos(string poNumber = "0", int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<StockTransferPoDb> GetAllStockTrasnferPo(string status, string branchCode, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}