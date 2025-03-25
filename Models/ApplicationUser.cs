
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string FirtName { get; set; }
        [Required]
        public required string LastName { get; set; }

        public string? AppData { get; set; }

        public Guid? BookMarkId { get; set; }
        [ForeignKey("BookMarkId")]
        public BookMark? BookMark { get; set; }
    }
}
