using System;
using System.Collections.Generic;
namespace WMSWebApp.ViewModels.GRN
{
    public class GRNModel
    {
        public string PONo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; } = new DateTime();
        public string BranchCode { get; set; }
        public string SenderCompany { get; set; }
        public List<GrnItems> Items { get; set; }
    }
    public class GrnItems
    {
        public int GRNId { get; set; }
        public string ItemCode { get; set; }
        public string SubItemCode { get; set; }
        public string SubItemName { get; set; }
        public string MaterialDescription { get; set; }
        public int Qty { get; set; }
        public string Unit { get; set; }
        public decimal Amount { get; set; }
        public int AreaId { get; set; }
    }
}
