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

        #endregion

        #region Ctor
        public WarehouseService(IRepository<Warehouse> warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }
        #endregion

        #region Methods

        public virtual void Insert(Warehouse entity)
        {
            _warehouseRepository.Insert(entity);
        }

        public virtual void Update(Warehouse entity)
        {
            _warehouseRepository.Update(entity);
        }


        public virtual IPagedList<Warehouse> GetAllWarehouse(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _warehouseRepository.Table
                        select x;
            var result = new PagedList<Warehouse>(query, pageIndex, pageSize);
            return result;
        }

        #endregion
    }
}
