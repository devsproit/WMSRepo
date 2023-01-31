using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Domain.Model;
namespace WMSAPI.ViewModels
{
    public class Item
    {
        public int Id { get; set; }

        //[Display(Name = "Company Name")]
        //public int CompanyId { get; set; }
        [DataType(DataType.MultilineText)]

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [DataType(DataType.MultilineText)]

        [Required]
        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        //public List<CompanyDb> Companies { get; set; }



    }
}
