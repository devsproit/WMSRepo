using System.ComponentModel.DataAnnotations;

namespace WMSWebApp.ViewModels
{
    public class Company
    {
        public int Id { get; set; }
        public string ScreenCode { get; set; }
        [Display(Name = "Company Code")]
        public string CompanyCode { get; set; }
        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [DataType(DataType.MultilineText)]
        public string Location { get; set; }
        [Display(Name = "HO Contact Person")]
        public string ContactPersonHO { get; set; }
        [Required]
        [Phone]
        [Display(Name = "HO Contact Number")]
        public string ContactNumberHO { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "HO Email-Id")]
        public string EmailIdHO { get; set; }
        [Display(Name = "Space Size Format")]
        public string SpaceSizeFormat { get; set; }
        public string Items { get; set; }
        [Display(Name = "Sub Item")]
        public string SubItem { get; set; }

    }
}
