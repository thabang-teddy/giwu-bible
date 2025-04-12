

namespace Website.ViewModels.Visitor
{
    public class VisitorBibleViewModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Abbreviation { get; set; }
        public string? About { get; set; }
        public string? Url { get; set; }
        public string? Publisher { get; set; }
        public string? Copyright { get; set; }
        public required string Language { get; set; }
        public string? OtherInfo { get; set; }

        public List<VisitorBibleBookViewModel> BobleBooks { get; set; } = new List<VisitorBibleBookViewModel>();
    }

    public class VisitorBibleBookViewModel
    {
        public int Book { get; set; }
        public required string Name { get; set; }
        public int ChapterCount { get; set; }
    }

    public class VisitorChapterViewModel
    {
        public string? Bible { get; set; }
        public string? Book { get; set; }
        public int Number { get; set; }
        public List<VersesViewModel> Verses { get; set; } = new List<VersesViewModel>();
    }
    public class VersesViewModel
    {
        public int Verse { get; set; }
        public required string Text { get; set; }
    }
}
