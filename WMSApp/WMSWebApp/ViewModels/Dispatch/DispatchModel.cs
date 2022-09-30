using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Domain.Model.Invoice;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;

namespace WMSWebApp.ViewModels.Dispatch
{
    public class DispatchModel
    {
        [Required]
        public int InvoiceId { get; set; }
        [Required]
        public DateTime DispatchDate { get; set; }
        public string PO { get; set; }
        public string VendorName { get; set; }
        public string VehicleNumber { get; set; }
        public string Location { get; set; }
        [Required]
        public DateTime CreateOn { get; set; }

        public List<InvoiceMaster> InvoiceList { get; set; }
    }
}
