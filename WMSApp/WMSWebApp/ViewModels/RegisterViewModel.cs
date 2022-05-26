using System.ComponentModel.DataAnnotations;

namespace WMSWebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
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
    }
}
