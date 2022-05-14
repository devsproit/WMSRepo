using System.ComponentModel.DataAnnotations;
using Domain.Model.CompanyMaster;
using System.Collections.Generic;
namespace WMSWebApp.ViewModels
{
    public class Branch
    {

        [Display(Name = "Screen Code")]
        public string ScreenCode { get; set; }
        [Display(Name = "Branch Code")]
        public string BranchCode { get; set; }

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

        [Display(Name = "Warehouse Name")]
        public int WarehouseId{ get; set; }

        public List<Company> Companies { get; set; }

    }
}
