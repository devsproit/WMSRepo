using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;

namespace Domain.Model.PO
{
    public class PurchaseOrderDb : BaseEntity
    {
        public DateTime PODate { get; set; }

        public string PONumber { get; set; }

        public string POCategory { get; set; }

    }
}
