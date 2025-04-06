namespace Website.Areas.Admin.ViewModels.Feedback
{
    public class FeedbackEditViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }

        public string? Notes { get; set; }
        public DateTime? AlertDate { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateAt { get; set; }
        public string? UpdateBy { get; set; }
    }
}
