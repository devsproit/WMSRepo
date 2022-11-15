using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;

namespace Domain.Model.Masters
{
    public partial class Dispatch : BaseEntity
    {

        public int InvoiceId { get; set; }
        public DateTime DispatchDate { get; set; }
        public string PO { get; set; }
        public string VendorName { get; set; }
        public string VehicleNumber { get; set; }
        public string Location { get; set; }
        public DateTime CreateOn { get; set; }
        public string BranchCode { get; set; }
        public string DocketNo { get; set; }
        public string LRNo { get; set; }

    }
}
