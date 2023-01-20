using System;
namespace WMSWebApp.ViewModels.GRN
{
	public class GRNListReportModel
	{
        public int Id { get; set; }
        public string PONo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string BranchCode { get; set; }
        public string SenderCompany { get; set; }
        public string Branch { get; set; }
        public string GRNNumberOfSAP { get; set; }
        public string IRN { get; set; }
    }
}
