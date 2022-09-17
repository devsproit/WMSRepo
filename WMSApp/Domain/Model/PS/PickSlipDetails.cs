using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
namespace Domain.Model.PS
{
    public partial class PickSlipDetails : BaseEntity
    {

        public string ItemCode { get; set; }
        public string SubItemCode { get; set; }
        public string SubItemName { get; set; }
        public int Qty { get; set; }
        public string Unit { get; set; }
        public decimal Amount { get; set; }
        public int AreaId { get; set; }
        public int PickSlipId { get; set; }
        public int InventoryId { get; set; }
        public virtual PickSlipMaster PickSlipMaster { get; set; }
             

    }
}
