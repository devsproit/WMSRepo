using System;

namespace WMSWebApp.ViewModels.Report
{
    public class PickSlipModel
    {
        public int Id { get; set; }
        public string BranchCode { get; set; }
        public string Branch { get; set; }
        public string PickSlipName { get; set; }
        public string DockType { get; set; }
        public int Item { get; set; }
        public DateTime CreateOn { get; set; }
        public string ItemCode { get; set; }
        public string SubItemCode { get; set; }
        public string SubItemName { get; set; }
        public int Qty { get; set; }
        public string Unit { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int InventoryId { get; set; }

    }
}
