namespace WMSWebApp.ViewModels.GRN
{
    public class GrnItemListModel
    {
        public int Id { get; set; }
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
