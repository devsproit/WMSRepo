using System;

namespace WMSWebApp.ViewModels.Pickslip
{
    public class PickSlipListModel
    {
        public int Id { get; set; }
        public string BranchCode { get; set; }
        public string Branch { get; set; }
        public string PickSlipName { get; set; }
        public string DockType { get; set; }
        public int Item { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
