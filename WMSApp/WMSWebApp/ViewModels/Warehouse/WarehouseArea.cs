using Domain.Model.Masters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WMSWebApp.ViewModels
{
    public class WarehouseArea
    {

        [Required]
        public int WarehouseId { get; set; }
        [Required]
        public int ZoneId { get; set; }
        [Required]
        public string AreaName { get; set; }
        [Required]
        public string AreaCode { get; set; }
        [Required]
        public int Size { get; set; }

        public string AreaType { get; set; }
        public int AvailableArea { get; set; }
        //public List<WarehouseZone> ZoneList { get; set; }
    }

    public class WarehouseAreaList
    {
        public List<WarehouseArea>  WarehouseAreas { get; set; }
        public int WarehouseId { get; set; }
    }

    public class WarehouseZoneModel
    {
        public int Id { get; set; }
        public string ZoneName { get; set; }
    }
}