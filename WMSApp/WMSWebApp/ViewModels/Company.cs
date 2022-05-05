using System.ComponentModel.DataAnnotations;

namespace WMSWebApp.ViewModels
{
    public class Company
    {
        public int Id { get; set; }
        public string ScreenCode { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string ContactPersonHO { get; set; }
        [Required]
        [Phone]
        public string ContactNumberHO { get; set; }
        [Required]
        [EmailAddress]
        public string EmailIdHO { get; set; }
        public string SpaceSizeFormat { get; set; }
        public string Items { get; set; }
        public string SubItem { get; set; }

    }
}
