using System.ComponentModel.DataAnnotations;

namespace Website.Areas.Admin.ViewModels.Users
{
    public class UserViewModel
    {
        public string? Id { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? AppData { get; set; }

        public Guid? BookMarkId { get; set; }
    }
}
