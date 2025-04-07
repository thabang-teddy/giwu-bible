namespace Models
{
    public class BibleBook
    {
        public Guid Id { get; set; }
        public Guid BibleId { get; set; }
        public Bible Bible { get; set; }

        public required string BookList { get; set; }

        public List<Chapter> Chapters { get; set; } = new List<Chapter>();

        public BibleBook()
        {
            Id = Guid.NewGuid();
        }
    }
}
