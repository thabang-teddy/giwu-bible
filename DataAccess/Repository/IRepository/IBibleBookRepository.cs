using Models;

namespace DataAccess.Repository.IRepository
{
    public interface IBibleBookRepository : IRepository<BibleBook>
    {
        void Update(BibleBook obj);
    }
}
