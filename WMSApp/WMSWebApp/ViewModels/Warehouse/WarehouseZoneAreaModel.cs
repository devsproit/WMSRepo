using System.ComponentModel.DataAnnotations;

namespace WMSWebApp.ViewModels
{
    public class WarehouseZoneAreaModel
    {
        public int Id { get; set; }
        public string ZoneName { get; set; }
        public string ZoneCode { get; set; }
        
        public string AreaName { get; set; }
        public string AreaCode { get; set; }
        public string AreaType { get; set; }
        public string Size { get; set; }
    }
}