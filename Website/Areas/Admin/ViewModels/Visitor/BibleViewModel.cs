

namespace Website.Areas.Admin.ViewModels.Visitor
{
    public class BibleViewModel
    {
        public Guid Id { get; set; }
        public string? LagacyId { get; set; }
        public string? RootTable { get; set; }
        public string? RootUrl { get; set; }
        public required string Name { get; set; }
        public required string Abbreviation { get; set; }
        public required string About { get; set; }
        public required string Url { get; set; }
        public required string Publisher { get; set; }
        public required string Copyright { get; set; }
        public required string Language { get; set; }
        public required string OtherInfo { get; set; }

        public List<BibleBookViewModel> BobleBooks { get; set; } = new List<BibleBookViewModel>();
    }

    public class BibleBookViewModel
    {
        public required string Name { get; set; }
        public int ChapterCount { get; set; }
        public List<ChapterViewModel> Chapters { get; set; } = new List<ChapterViewModel>();
    }

    public class ChapterViewModel
    {
        public int Number { get; set; }
        public List<VersesViewModel> Verses { get; set; } = new List<VersesViewModel>();
    }
    public class VersesViewModel
    {
        public int Number { get; set; }
        public required string Verse { get; set; }
    }
}
