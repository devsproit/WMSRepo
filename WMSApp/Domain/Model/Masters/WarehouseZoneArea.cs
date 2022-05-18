using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;

namespace Domain.Model.Masters
{
    public partial class WarehouseZoneArea:BaseEntity
    {
        public int WarehouseId { get; set; }
        public string ZoneCode { get; set; }
        public string ZoneName { get; set; }
        public string AreaType { get; set; }
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
        public string Size { get; set; }

        public virtual Warehouse Warehouse { get; set; }

    }
}
