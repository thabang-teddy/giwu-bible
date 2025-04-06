using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Website.Areas.Admin.ViewModels.Bibles
{
    public class BibleViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string? LagacyId { get; set; }
        public string? RootTable { get; set; }
        public string? RootUrl { get; set; }
        public required string Name { get; set; }
        public required string Abbreviation { get; set; }
        public string? About { get; set; }
        public string? Url { get; set; }
        public string? Publisher { get; set; }
        public string? Copyright { get; set; }
        public required string Language { get; set; }
        public string? OtherInfo { get; set; }

        public List<BibleBookViewModel> BibleBooks { get; set; } = new List<BibleBookViewModel>();
    }

    public class BibleBookViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public int ChapterCount { get; set; }

        public Guid BibleId { get; set; }
        [ForeignKey("BibleId")]
        public BibleViewModel? Bible { get; set; }

        public List<ChapterViewModel> Chapters { get; set; } = new List<ChapterViewModel>();
    }

    public class ChapterViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public int Number { get; set; }

        public Guid BibleBookId { get; set; }
        [ForeignKey("BibleBookId")]
        public BibleBookViewModel? BibleBook { get; set; }

        public List<VersesViewModel> Verses { get; set; } = new List<VersesViewModel>();
    }

    public class VersesViewModel
    {
        public int Number { get; set; }

        [Required]
        public required string Verse { get; set; }
    }
}
