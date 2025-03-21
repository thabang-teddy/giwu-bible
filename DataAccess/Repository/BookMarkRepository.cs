using DataAccess.Repository;
using DataAcess.Data;
using DataAccess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BookMarkRepository : Repository<BookMark>, IBookMarkRepository
    {
        private ApplicationDbContext _db;
        public BookMarkRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(BookMark obj)
        {
            _db.BookMarks.Update(obj);
        }
    }
}
