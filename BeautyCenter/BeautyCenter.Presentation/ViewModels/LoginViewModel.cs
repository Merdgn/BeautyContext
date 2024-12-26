using System.ComponentModel.DataAnnotations;

namespace BeautyCenter.Presentation.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Email Address is required")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
