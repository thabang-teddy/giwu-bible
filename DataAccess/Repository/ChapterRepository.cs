using DataAccess.Repository;
using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ChapterRepository : Repository<Chapter>, IChapterRepository
    {
        private ApplicationDbContext _db;
        public ChapterRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Chapter obj)
        {
            _db.Chapters.Update(obj);
        }
    }
}
