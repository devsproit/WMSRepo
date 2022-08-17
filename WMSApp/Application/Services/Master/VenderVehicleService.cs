using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
using WMS.Core.Data;
using Domain.Model.Masters;

namespace Application.Services.Master
{
    public partial class VenderVehicleService : IVenderVehicleService
    {
        #region Fields
        private readonly IRepository<VendorVehicle> _vendorVehicleRepository;
        #endregion

        #region Ctor
        public VenderVehicleService(IRepository<VendorVehicle> vendorVehicleRepository)
        {
            _vendorVehicleRepository = vendorVehicleRepository;
        }
        #endregion

        #region Methods
        public virtual void Insert(VendorVehicle entity)
        {
            _vendorVehicleRepository.Insert(entity);
        }

        public virtual IPagedList<VendorVehicle> VendorVehicles(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = from x in _vendorVehicleRepository.Table
                        select x;
            var result = new PagedList<VendorVehicle>(query, pageIndex, pageSize);
            return result;
        }
        #endregion
    }
}
