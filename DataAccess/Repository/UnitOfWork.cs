
using DataAccess.Repository.IRepository;
using DataAcess.Data;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IBookMarkRepository BookMark { get; private set; }
        public IBibleRepository Bible  { get; private set; }
        public IBibleBookRepository BibleBook  { get; private set; }
        public IChapterRepository Chapter  { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            BookMark = new BookMarkRepository(_db);
            Bible = new BibleRepository(_db);
            BibleBook = new BibleBookRepository(_db);
            Chapter = new ChapterRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
