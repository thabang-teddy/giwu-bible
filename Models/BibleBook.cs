using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BibleBook
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int ChapterCount { get; set; }

        public Guid? BibleId { get; set; }
        public Bible? Bible { get; set; }

        public List<Chapter> Chapters { get; set; } = new List<Chapter>();

        public BibleBook()
        {
            Id = Guid.NewGuid();
        }
    }
}
