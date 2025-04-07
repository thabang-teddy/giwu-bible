using System.ComponentModel.DataAnnotations;

namespace Models.SyncModels.BibleSQlite
{
    public class KeyEnglish
    {
        [Key]
        public int b { get; set; }
        public required string n { get; set; }
    }
}
