using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
namespace Domain.Model.StockManagement
{
   public partial class ItemStock: BaseEntity
    {
        public string ItemCode { get; set; }
        public int Qty { get; set; }
        public DateTime LastUpdate { get; set; }
        public string BranchCode { get; set; }

    }
}
