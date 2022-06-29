using Domain.Model.PO;

namespace Application.Services.PO
{
    public interface ISalePo
    {
        void Insert(SalePoDb salePoDb);
        void Update(SalePoDb salePoDb);
    }
}