using System;

namespace WMSWebApp.ViewModels.Dispatch
{
    public class DispatchList
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public DateTime DispatchDate { get; set; }
        public string PO { get; set; }
        public string VendorName { get; set; }
        public string VehicleNumber { get; set; }
        public string Location { get; set; }
        public DateTime CreateOn { get; set; }
        public string BranchCode { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Invoice { get; set; }
        public string LRN0 { get; set; }

        public string  BillTo { get; set; }

        public int Qty { get; set; }

    }
}
