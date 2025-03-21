using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Bible
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Abbreviation { get; set; }
        public required string About { get; set; }
        public required string Url { get; set; }
        public required string Publisher { get; set; }
        public required string Copyright { get; set; }
        public required string Language { get; set; }
        public required string OtherInfo { get; set; }

        public List<BibleBook> BobleBooks { get; set; } = new List<BibleBook>();
    }
}
