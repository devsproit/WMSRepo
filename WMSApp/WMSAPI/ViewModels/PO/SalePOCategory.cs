namespace WMSAPI.ViewModels.PO
{
    public class SalePO
    {
        public string PONumber { get; set; }
        public string SalePOCategory { get; set; }
        public string SalePOSendingTo { get; set; }
        public string SalePOItem { get; set; }

        public string SalePOSubItem { get; set; }
        public int SalePOQty { get; set; }
        public string SalePOAmt { get; set; }
        public string SalePOSerialNumber { get; set; }
        public string SubItemCode { get; set; }
    }
}