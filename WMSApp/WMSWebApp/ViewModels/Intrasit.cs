using System;
using System.ComponentModel.DataAnnotations;

namespace WMSWebApp.ViewModels
{
    public class Intrasitc
    {
        public int Id { get; set; }
        public string LoginBranch { get; set; }
        public string SenderCompany { get; set; }
        public string Branch { get; set; }
        public string PurchaseOrder { get; set; }
        public string ItemCode { get; set; }
        public string SubItemCode { get; set; }
        public string SubItemName { get; set; }
        public string MaterialDescription { get; set; }
        public string Qty { get; set; }
        public string Unit { get; set; }
        public string Amt { get; set; }
        public DateTime ETA { get; set; }
        public bool IsDeleted { get; set; }

    }
}
