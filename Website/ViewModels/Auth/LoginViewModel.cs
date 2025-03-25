using System.ComponentModel.DataAnnotations;

namespace Website.ViewModels.Auth
{
    public class LoginViewModel
    {
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
