using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Chapter
    {
        public Guid Id { get; set; }

        public int Number { get; set; }
        public required string Verses { get; set; }

        public Guid BobleBookId { get; set; }
        public BibleBook BobleBook { get; set; }

        public Chapter()
        {
            Id = Guid.NewGuid();
        }

    }
}
