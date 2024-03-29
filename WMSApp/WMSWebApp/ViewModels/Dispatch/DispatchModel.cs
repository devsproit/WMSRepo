﻿
using Domain.Model.Invoice;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WMSWebApp.ViewModels.Dispatch
{
    public class DispatchModel
    {
        [Required]
        public int InvoiceId { get; set; }
        [Required]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        
        public string DispatchDate { get; set; }
        public string PO { get; set; }
        public string VendorName { get; set; }
        public string VehicleNumber { get; set; }
        public string Location { get; set; }
        
        public DateTime CreateOn { get; set; }

        public List<InvoiceMaster> InvoiceList { get; set; }

        public string DocketNo { get; set; }

        public string LRNo { get; set; }
    }
}
