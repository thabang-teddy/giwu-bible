using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class BookMark
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Marks { get; set; }
        public required string StopPoint { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public BookMark()
        {
            Id = Guid.NewGuid();
        }
    }
}
