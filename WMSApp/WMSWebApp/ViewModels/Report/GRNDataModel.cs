using System;
namespace WMSWebApp.ViewModels.Report
{
    public class GRNDataModel
    {
        public int Id { get; set; }
        public string PONo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string BranchCode { get; set; }
        public string SenderCompany { get; set; }
        public string ItemCode { get; set; }
        public string SubItemCode { get; set; }
        public string SubItemName { get; set; }
        public string MaterialDescription { get; set; }
        public int Qty { get; set; }
        public string Unit { get; set; }
        public decimal Amount { get; set; }
       
        public string Area { get; set; }

        public string SAPNO { get; set; }
        public string IRN { get; set; } 

    }
}
