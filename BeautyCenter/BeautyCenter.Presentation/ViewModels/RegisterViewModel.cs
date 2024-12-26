using System.ComponentModel.DataAnnotations;

namespace BeautyCenter.Presentation.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName { get; set; } 

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string EmailAddress { get; set; } 

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } 

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } 
    }
}
