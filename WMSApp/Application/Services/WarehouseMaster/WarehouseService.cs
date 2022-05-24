using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
using WMS.Core.Data;
using WMS.Data;
using Domain.Model.Masters;


namespace Application.Services.WarehouseMaster
{
    public partial class WarehouseService : IWarehouseService
    {
        #region Fields
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IRepository<WarehouseZone> _warehouseZoneRepository;
        private readonly IRepository<WarehouseZoneArea> _warehouseZoneAreaRepository;

        #endregion

        #region Ctor
        public WarehouseService(IRepository<Warehouse> warehouseRepository, IRepository<WarehouseZone> warehouseZoneRepository, IRepository<WarehouseZoneArea> warehouseZoneAreaRepository)
        {
            _warehouseRepository = warehouseRepository;
            _warehouseZoneRepository = warehouseZoneRepository;
            _warehouseZoneAreaRepository = warehouseZoneAreaRepository;
        }
        #endregion

        #region Methods

        public virtual int Insert(Warehouse entity)
        {
            _warehouseRepository.Insert(entity);
            return entity.Id;
        }

        public virtual void Update(Warehouse entity)
        {
            _warehouseRepository.Update(entity);
        }

        public Warehouse GetById(int id)
        {
            return _warehouseRepository.GetById(id);
        }

        public virtual IPagedList<WarehouseZone> GetAllZone(int warehouseId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _warehouseZoneRepository.Table
                        where x.WarehouseId == warehouseId
                        select x;
            var result = new PagedList<WarehouseZone>(query, pageIndex, pageSize);
            return result;
        }
        public virtual IPagedList<Warehouse> GetAllWarehouse(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _warehouseRepository.Table
                        select x;
            var result = new PagedList<Warehouse>(query, pageIndex, pageSize);
            return result;
        }

        public virtual void InsertArea(List<WarehouseZoneArea> entities)
        {
            _warehouseZoneAreaRepository.Insert(entities);
        }

        #endregion
    }
}
