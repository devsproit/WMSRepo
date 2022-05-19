using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
namespace Domain.Model.Masters
{
    public partial class WarehouseZone : BaseEntity
    {
        public int WarehouseId { get; set; }
        public string ZoneCode { get; set; }
        public string ZoneName { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        private ICollection<WarehouseZoneArea> _warehouseZoneAreas;
        public virtual ICollection<WarehouseZoneArea> WarehouseZoneAreas
        {
            get => _warehouseZoneAreas ?? (_warehouseZoneAreas = new List<WarehouseZoneArea>());
            protected set => _warehouseZoneAreas = value;
        }
    }
}
