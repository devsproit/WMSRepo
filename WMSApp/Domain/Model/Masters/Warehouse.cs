using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
namespace Domain.Model.Masters
{
    public partial class Warehouse: BaseEntity
    {
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public int TotalSpaceSize { get; set; }

        private ICollection<WarehouseZoneArea> _warehouseZoneAreas;
        public virtual ICollection<WarehouseZoneArea> WarehouseZoneAreas
        {
            get=>_warehouseZoneAreas ??= new List<WarehouseZoneArea>();
            protected set => _warehouseZoneAreas = value;
        }

        private ICollection<WarehouseZone> _warehouseZones;
        public virtual ICollection<WarehouseZone> WarehouseZones
        {
            get => _warehouseZones ??= new List<WarehouseZone>();
            protected set => _warehouseZones = value;

        }

        private ICollection<BranchWiseWarehouse> _branchWiseWarehouses;
        public virtual ICollection<BranchWiseWarehouse> BranchWiseWarehouses
        {
            get=>_branchWiseWarehouses??=new List<BranchWiseWarehouse>();
            protected set => _branchWiseWarehouses = value;
        }


    }
}
