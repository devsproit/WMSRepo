using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
namespace Domain.Model.Masters
{
    public partial class VendorVehicle : BaseEntity
    {
        public string VehicleNo { get; set; }
        public bool Active { get; set; }
    }
}
