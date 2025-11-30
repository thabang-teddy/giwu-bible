using System.ComponentModel.DataAnnotations;

namespace Website.ViewModels.Auth
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public required string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public required string LastName { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public required string ConfirmPassword { get; set; }
    }
}
