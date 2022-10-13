using Domain.Model.GRN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Core;
using WMS.Core.Domain;
namespace Domain.Model.Invoice
{
    public partial class InvoiceMaster : BaseEntity
    {

        public string PoNumber { get; set; }
        public string PoCategory { get; set; }
        public DateTime CreateOn { get; set; }
        public string InvoiceNumber { get; set; }
        public string BilledTo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int PickSlipId { get; set; }

        public String BranchCode { get; set; }
        public bool DispatchDone { get; set; }

        private ICollection<InvoiceDetails> _invoiceDetails;

        public virtual ICollection<InvoiceDetails>  InvoiceDetails
        {
            get => _invoiceDetails ?? (_invoiceDetails = new List<InvoiceDetails>());
            protected set => _invoiceDetails = value;
        }

    }
}
