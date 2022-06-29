using Domain.Model.PO;

namespace Domain.Model.PO
{
    public interface IStockTransferPo
    {
        void Insert(StockTransferPoDb stockTransferPo);
        void Update(StockTransferPoDb stockTransferPo);
    }
}