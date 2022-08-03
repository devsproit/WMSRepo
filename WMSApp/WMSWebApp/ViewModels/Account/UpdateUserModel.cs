using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Domain.Model;

namespace WMSWebApp.ViewModels.Account
{
    public class UpdateUserModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
       
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public List<Branch> Branches { get; set; }
        public int BranchId { get; set; }
        [Required]
        public List<int> BranchList { get; set; }
        public int Id { get; set; }
        public DateTime CreateOn { get; set; }
        public string Branch { get; set; }

        public string Role { get; set; }
        public string UserId { get; set; }
    }
}
