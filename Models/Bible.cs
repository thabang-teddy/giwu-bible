namespace Models
{
    public class Bible
    {
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

        public Guid? BobleBookId { get; set; }
        public BibleBook BibleBook { get; set; }

        public Bible()
        {
            Id = Guid.NewGuid();
        }
    }
}
