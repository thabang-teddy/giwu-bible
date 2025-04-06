namespace Website.Areas.Admin.ViewModels.Feedback
{
    public class FeedbackViewModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Subject { get; set; }
        public required string Message { get; set; }

        public string? Notes { get; set; }
        public DateTime AlertDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; }
        public required string UpdateBy { get; set; }
    }
}
