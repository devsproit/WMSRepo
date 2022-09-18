namespace WMSWebApp.ViewModels.PO
{
    public class PoSrnViewModel
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
        public decimal Qty { get; set; }
        public string Unit { get; set; }
        public decimal Amt { get; set; }
        public bool IsDeleted { get; set; }
        public int Sno { get; set; }

        public string SendingTo { get; set; }
    }
}
