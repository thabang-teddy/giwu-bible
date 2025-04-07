using System.ComponentModel.DataAnnotations;

namespace Models.SyncModels.BibleSQlite
{
    public class BibleVersionKey
    {
        [Key]
        public int id { get; set; }
        public string? table { get; set; }
        public required string abbreviation { get; set; }
        public required string language { get; set; }
        public required string version { get; set; }
        public string? info_text { get; set; }
        public string? info_url { get; set; }
        public string? publisher { get; set; }
        public string? copyright { get; set; }
        public string? copyright_info { get; set; }
    }
}
