namespace Models
{
    public class Chapter
    {
        public Guid Id { get; set; }

        public int Book { get; set; }
        public int Number { get; set; }
        public required string Verses { get; set; }

        public Guid BibleBookId { get; set; }
        public BibleBook BibleBook { get; set; }

        public Chapter()
        {
            Id = Guid.NewGuid();
        }

    }
}
