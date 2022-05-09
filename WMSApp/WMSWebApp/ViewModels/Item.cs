using System.ComponentModel.DataAnnotations;

namespace WMSWebApp.ViewModels
{
    public class Item
    {
        public int Id { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [DataType(DataType.MultilineText)]

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [DataType(DataType.MultilineText)]

        [Required]
        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }
        

    }
}
