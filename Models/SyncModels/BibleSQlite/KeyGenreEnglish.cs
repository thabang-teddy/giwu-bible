using System.ComponentModel.DataAnnotations;

namespace Models.SyncModels.BibleSQlite
{
    public class KeyGenreEnglish
    {
        [Key]
        public int g { get; set; }
        public string? t { get; set; }
    }
}
