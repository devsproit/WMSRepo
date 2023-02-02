namespace WMSAPI.ViewModels.GRN
{
    public class GrnItemListModel
    {
        public int Id { get; set; }
        public string POId { get; set; }

        public string ItemCode { get; set; }
        public string SubItemCode { get; set; }
        public string SubItemName { get; set; }
        public string MaterialDescription { get; set; }
        public int Qty { get; set; }
        public string Unit { get; set; }
        public decimal Amount { get; set; }
        public int AreaId { get; set; }
        public string Location { get; set; }
        public int InventoryId { get; set; }

        public string DockType { get; set; }
    }
}
