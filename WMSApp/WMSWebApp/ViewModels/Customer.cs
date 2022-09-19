using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WMSWebApp.ViewModels
{
    public class Customer
    {

        public int Id { get; set; }
        [Display(Name = "Screen Code")]
        public string ScreenCode { get; set; }
        
        [Display(Name = "Customer Category")]
        public string CustomerCategory { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [DataType(DataType.MultilineText)]

        [Display(Name = "Customer Code")]
        public string CustomerCode { get; set; }
        public string Location { get; set; }

        public List<string> DistrictList { get; set; }

        public int Selected { get; set; }
    }
}
