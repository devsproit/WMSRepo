using System.ComponentModel.DataAnnotations;

namespace WMSWebApp.ViewModels
{
    public class Branch
    {
        public int Id { get; set; }
        public string ScreenCode { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string ContactPersonBranch { get; set; }
        [Required]
        [Phone]
        public string ContactNumberBranch { get; set; }
        [Required]
        [EmailAddress]
        public string EmailIdBranch { get; set; }
        public string AssociatedEmployee { get; set; }
        public string WarehouseName { get; set; }

    }
}
