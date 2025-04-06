using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Feedback
    {
        public Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required, EmailAddress]
        public required string Email { get; set; }
        public string? Subject { get; set; }
        [Required]
        public required string Message { get; set; }

        public string? Notes { get; set; }
        public DateTime? AlertDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateAt { get; set; }
        public string? UpdateBy { get; set; }

        public Feedback()
        {
            Id = Guid.NewGuid();
        }
    }
}
