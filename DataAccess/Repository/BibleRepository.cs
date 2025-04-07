using DataAccess.Repository.IRepository;
using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BibleRepository : Repository<Bible>, IBibleRepository
    {
        private ApplicationDbContext _db;
        public BibleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Bible obj)
        {
            _db.Bibles.Update(obj);
        }
    }
}
