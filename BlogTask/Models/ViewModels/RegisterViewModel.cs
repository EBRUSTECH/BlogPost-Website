using System.ComponentModel.DataAnnotations;

namespace BlogTask.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Must be between 3 and 15 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(15, MinimumLength = 3, ErrorMessage ="Must be between 3 and 15 characters")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="Password mismatch!")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
