using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WMSWebApp.ViewModels
{
    public class WarehouseModel
    {
        public int Id { get; set; }
        [Display(Name = "Warehouse Code")]
        public string WarehouseCode { get; set; }
        [Display(Name = "Warehouse Name")]
        public string WarehouseName { get; set; }
        [Display(Name = "Location")]
        public string Location { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }
        [Display(Name = "Total SpaceSize Type")]
        public string TotalSpaceSizeType { get; set; }
        [Display(Name = "Total Space Size")]
        public string TotalSpaceSize { get; set; }
        
        public List<WarehouseZoneAreaModel> ZoneAreaList { get; set; }

    }
}
