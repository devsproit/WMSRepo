using System.ComponentModel.DataAnnotations;
using Domain.Model.CompanyMaster;
using System.Collections.Generic;
using WMS.Data;
using Domain.Model.Masters;

namespace WMSWebApp.ViewModels
{
    public class BranchModel
    {

        public int Id { get; set; }
        [Display(Name = "Screen Code")]
        public string ScreenCode { get; set; }
        [Required]
        [Display(Name = "Branch Code")]
        public string BranchCode { get; set; }
        [Required]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }
        [DataType(DataType.MultilineText)]

        [Display(Name = "Company Name")]
        public int CompanyId { get; set; }
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [DataType(DataType.MultilineText)]
        public string Location { get; set; }

        [Display(Name = "Branch Contact Person")]
        public string ContactPersonBranch { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        [Phone]
        [Display(Name = "Branch Contact Number")]
        public string ContactNumberBranch { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Branch Email ID")]
        public string EmailIdBranch { get; set; }

        [Display(Name = "Associated Employee")]
        public string AssociatedEmployee { get; set; }

        [Required]
        [Display(Name = "Warehouse Name")]
        public List<int> WarehouseId { get; set; }

        public List<Company> Companies { get; set; }

        public List<UserProfileModel> Users { get; set; }

        public List<Warehouse> Warehouses { get; set; }

        public string CompanyName { get; set; }
        public string Werehouse { get; set; }
        public string UserName { get; set; }


    }
}
