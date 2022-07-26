using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
namespace Domain.Model.Invoice
{
    public partial class InvoiceDetails : BaseEntity
    {

        public int InvoiceMasterId { get; set; }
        public string PoCategory { get; set; }
        public string SubItem { get; set; }
        public string SubItemCode { get; set; }
        public string SerialNo { get; set; }
        public int Qty { get; set; }
        public decimal Amt { get; set; }

    }
}
