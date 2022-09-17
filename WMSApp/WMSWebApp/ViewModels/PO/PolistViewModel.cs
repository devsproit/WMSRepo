namespace WMSWebApp.ViewModels.PO
{
    public class PolistViewModel
    {
        public int Id { get; set; }
        public string PoNumber { get; set; }
        public string stockTransferPOCatagry { get; set; }
        public string stockTransferPoSendingTo { get; set; }
        public string stockTransferPoItem { get; set; }
        public string subItemCode { get; set; }
        public string stockTransferPoSubitem { get; set; }
        public string stockTransferPoQty { get; set; }
        public string stockTransferPoAmt { get; set; }
        public string stockTransferPoSerialNumber { get; set; }
        public string serviceCategory { get; set; }
        public string salePo { get; set; }
        public string saleDate { get; set; }
        public string ServiceRequestNumber { get; set; }
        public string invNumber { get; set; }
    }
}
