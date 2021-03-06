using Domain.Model.PO;
using WMS.Core;
namespace Application.Services.PO
{
    public interface ISalePo
    {
        void Insert(SalePoDb salePoDb);
        void Update(SalePoDb salePoDb);
        IPagedList<SalePoDb> GetSalePos(string poNumber = "0", int pageIndex = 0, int pageSize = int.MaxValue);
    }
}