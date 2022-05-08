using System.ComponentModel.DataAnnotations;

namespace WMSWebApp.ViewModels
{
    public class WarehouseZone
    {
        public int Id { get; set; }
        public string ZoneName { get; set; }
        public string ZoneCode { get; set; }
        //public string ZoneSizeType { get; set; }
        public string ZoneSize { get; set; }
    }
}