using System.ComponentModel.DataAnnotations;

namespace Website.ViewModels.Auth
{
    public class RegisterViewModel
    {
        [Required]
        public required string FirtName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required] 
        public required string Password { get; set; }
        [Required] 
        public required string ConfirmedPassword { get; set; }
    }
}
