using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;

namespace Domain.Model.PO
{
    public class ServiceOrderPODb: BaseEntity
    {
        public string PONumber { get; set; }
        public string ServiceOrderPOCategory { get; set; }
        public string ServiceOrderPOSendingTo { get; set; }
        public string ServiceOrderPOItem { get; set; }
        public string ServiceOrderPOSubitem { get; set; }

        public int ServiceOrderPOQty { get; set; }
        public string ServiceOrderPOServiceCatagry { get; set; }
        public string ServiceOrderPOServiceRequestNumber { get; set; }

        public string ServiceOrderPOSalePO { get; set; }
        public string ServiceOrderPOSaleDate { get; set; }

        public string ServiceOrderPOAmt { get; set; }

        public string ServiceOrderPOSerialNumber { get; set; }
        public string SubItemCode { get; set; }

        public bool IsProcessed { get; set; } = false;
    }
}
