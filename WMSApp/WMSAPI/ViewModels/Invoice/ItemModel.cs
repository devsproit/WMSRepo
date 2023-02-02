namespace WMSAPI.ViewModels.Invoice
{
    public class ItemModel
    {
        public int Id { get; set; }
        public int InvoiceMasterId { get; set; }
        public string PoCategory { get; set; }
        public string SubItem { get; set; }
        public string SubItemCode { get; set; }
        public string SerialNo { get; set; }
        public int Qty { get; set; }
        public decimal Amt { get; set; }

        public string MaterialDescription { get; set; }
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
        public string ZoneCode { get; set; }

        public string ZoneName { get; set; }
        public string Warehouse { get; set; }

        public string WarehouseCode { get; set; }
        public string SubItemName { get; set; }
        public int AreaId { get; set; }
        public string PONumber { get; set; }
    }
}
