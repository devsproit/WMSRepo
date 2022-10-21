using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;

namespace Domain.Model.SubItemMapping
{
    public partial class SubItemWareHouseMapping : BaseEntity
    {

        public int WareHouseId { get; set; }
        public int WareHouseAreaId { get; set; }
        public int LocationId { get; set; }
        public string SubItemCode { get; set; }
        public DateTime CreateOn { get; set; }
       

    }
}
