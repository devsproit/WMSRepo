namespace WMSAPI.ViewModels.Pickslip
{
    public class PickslipDetailModel
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string SubItemCode { get; set; }
        public string SubItemName { get; set; }
        public int Qty { get; set; }
        public string Unit { get; set; }
        public decimal Amount { get; set; }
        public int AreaId { get; set; }
        public int PickSlipId { get; set; }
        public string Guid { get; set; }
        public string Location { get; set; }
        public int GRN { get; set; }
        
    }
}
