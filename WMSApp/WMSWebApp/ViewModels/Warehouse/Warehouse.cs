using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WMSWebApp.ViewModels
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string Location { get; set; }
        public string ContactMaintainenceIncharge { get; set; }
        public string ContactNumberIncharge { get; set; }
        public string TotalSpaceSizeType { get; set; }
        public string TotalSpaceSize { get; set; }
        List<WarehouseZone> ZoneList { get; set; }
        List<WarehouseAisle> AisleList { get; set; }
        List<WarehouseArea> AreasList { get; set; }
    }
}
