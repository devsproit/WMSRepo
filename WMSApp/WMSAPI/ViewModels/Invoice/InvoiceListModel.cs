using System;

namespace WMSAPI.ViewModels.Invoice
{
    public class InvoiceListModel
    {
        public int Id { get; set; }
        public string PoNumber { get; set; }
        public string PoCategory { get; set; }
        public DateTime CreateOn { get; set; }
        public string InvoiceNumber { get; set; }
        public string BilledTo { get; set; }
    }
}
