using DataAccess.Repository.IRepository;
using DataAcess.Data;
using Models;

namespace DataAccess.Repository
{
    public class BibleBookRepository : Repository<BibleBook>, IBibleBookRepository
    {
        private ApplicationDbContext _db;

        public BibleBookRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(BibleBook obj)
        {
            _db.BibleBooks.Update(obj);
        }
    }
}
