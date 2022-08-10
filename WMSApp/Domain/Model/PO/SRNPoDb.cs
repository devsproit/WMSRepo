using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;

namespace Domain.Model.PO
{
    public class SRNPoDb : BaseEntity
    {
        public string PONumber { get; set; }
        public string SrnPOCategory { get; set; }
        public string SrnPOSendingTo { get; set; }

        public string SrnPOItem { get; set; }
        public string SrnPOSubItem { get; set; }

        public int SrnPOQty { get; set; }
        public string SrnPOSrnCause { get; set; }
        public string SrnSerialNumber { get; set; }
        public string SubItemCode { get; set; }

        public string InvoiceNo { get; set; }

        public bool IsProcess { get; set; }
    }
}
