using System.ComponentModel.DataAnnotations;

namespace WMSWebApp.ViewModels
{
    public class Location
    {
        public int Id { get; set; }
        public string ScreenCode { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        

    }
}
