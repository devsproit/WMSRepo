
using Domain.Model;
using Domain.Model.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WMSAPI.ViewModels.SubItemMapping
{
    public class SubItemMappingModel
    {
        public int Id { get; set; }
        [Required]
        public int WareHouseId { get; set; }
        [Required]
        public int WareHouseAreaId { get; set; }
        [Required]
        public int LocationId { get; set; }
        [Required]
        public string SubItemCode { get; set; }
        public DateTime CreateOn { get; set; }

        public List<Warehouse> Warehouse { get; set; }
       


    }
}
