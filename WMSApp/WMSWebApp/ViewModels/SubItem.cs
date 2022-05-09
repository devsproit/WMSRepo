﻿using System.ComponentModel.DataAnnotations;

namespace WMSWebApp.ViewModels
{
    public class SubItem
    {

        public int Id { get; set; }
        [Display(Name = "SubItem Code")]
        public string SubItemCode { get; set; }

        [Display(Name = "SubItem Name")]
        public string SubItemName { get; set; }
        [DataType(DataType.MultilineText)]

        [Required]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [DataType(DataType.MultilineText)]

        [Display(Name = "Material Description")]
        public string MaterialDescription { get; set; }
        [DataType(DataType.MultilineText)]

        [Display(Name = "SubItem Size")]
        public string SubItemSize { get; set; }

        [Display(Name = "SubItem FOC")]
        public string FOC { get; set; }

        [Display(Name = "SubItem Category")]
        public string SubItemCategory { get; set; }

        [Display(Name = "SubItem SR")]
        public string SubItemSR { get; set; }


    }
}
