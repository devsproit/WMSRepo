
using WMS.Core;

using Domain.Model.Masters;

namespace Application.Services.WarehouseMaster
{
    public partial interface IWarehouseService
    {
        void Insert(Warehouse entity);
        void Update(Warehouse entity);
        IPagedList<Warehouse> GetAllWarehouse(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
