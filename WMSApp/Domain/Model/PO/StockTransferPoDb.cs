using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;

namespace Domain.Model.PO
{
    public class StockTransferPoDb: BaseEntity
    {
        public string PONumber { get; set; }
        public string StockTransferPOCategory { get; set; }
        public string StockTransferPOSendingTo { get; set; }
        public string StockTransferPOItem { get; set; }
        public string StockTransferPOSubItem { get; set; }
        public int StockTransferPOQty { get; set; }
        public string StockTransferPOAmt { get; set; }
        public string StockTransferPOSerialNumber { get; set; }
        public string SubItemCode { get; set; }
    }
}
