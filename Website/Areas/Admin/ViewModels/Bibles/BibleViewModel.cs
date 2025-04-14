using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Models;

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
    }

    public class BibleBooksViewModel
    {

        [Key]
        public Guid Id { get; set; }
        public Guid BibleId { get; set; }

        public List<BibleBookViewModel> BookList { get; set; } = new List<BibleBookViewModel>();
    }
    
    public class BibleBookViewModel
    {
        public int Book { get; set; }
        [Required]
        public required string Name { get; set; }
        public int ChapterCount { get; set; }
    }

    public class ChapterViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public int Book { get; set; }
        public int Number { get; set; }

        public List<VersesViewModel> Verses { get; set; } = new List<VersesViewModel>();
    }

    public class VersesViewModel
    {
        public int Verse { get; set; }
        [Required]
        public required string Text { get; set; }
    }
}
