using System.ComponentModel.DataAnnotations;

namespace Website.ViewModels.Visitor
{
    public class FeedbackViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string? Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
