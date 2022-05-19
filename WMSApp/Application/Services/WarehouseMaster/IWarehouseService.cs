using System.Collections.Generic;
using WMS.Core;

using Domain.Model.Masters;

namespace Application.Services.WarehouseMaster
{
    public partial interface IWarehouseService
    {
        int Insert(Warehouse entity);
        void Update(Warehouse entity);
        Warehouse GetById(int id);
        IPagedList<Warehouse> GetAllWarehouse(int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<WarehouseZone> GetAllZone(int warehouseId, int pageIndex = 0, int pageSize = int.MaxValue);
        void InsertArea(List<WarehouseZoneArea> entities);
    }
}
