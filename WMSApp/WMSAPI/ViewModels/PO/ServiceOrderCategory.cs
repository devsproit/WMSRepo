namespace WMSAPI.ViewModels.PO
{
    public class ServiceOrderCategory
    {
        public string ServiceOrderPOCategory { get; set; }
        public string ServiceOrderPOSendingTo { get; set; }
        public string ServiceOrderPOItem { get; set; }
        public string ServiceOrderPOSubitem { get; set; }

        public int ServiceOrderPOQty { get; set; }
        public string ServiceOrderPOServiceCatagry { get; set; }
        public string ServiceOrderPOServiceRequestNumber { get; set; }

        public string ServiceOrderPOSalePO { get; set; }
        public string ServiceOrderPOSaleDate { get; set; }

        public string ServiceOrderPOAmt { get; set; }

        public string ServiceOrderPOSerialNumber { get; set; }
        public string SubItemCode { get; set; }
    }
}