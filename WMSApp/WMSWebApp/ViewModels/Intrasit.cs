using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WMSWebApp.ViewModels
{
    public class Intrasitc
    {
        public int Id { get; set; }
        public string LoginBranch { get; set; }
        public string SenderCompany { get; set; }
        [Display(Name = "Sender Branch")]
        public string Branch { get; set; }
        [Display(Name = "Purchase Order")]
        public string PurchaseOrder { get; set; }
        public string ItemCode { get; set; }
        public string SubItemCode { get; set; }
        public string SubItemName { get; set; }
        [Display(Name = "Material Description")]
        public string MaterialDescription { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; }
        public decimal Amt { get; set; }
        public DateTime ETA { get; set; }
        public bool IsDeleted { get; set; }
        public int Sno { get; set; }
        public bool AllowGRN { get; set; }

    }
}
