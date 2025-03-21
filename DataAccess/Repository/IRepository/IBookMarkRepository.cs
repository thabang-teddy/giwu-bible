using Models;

namespace DataAccess.Repository.IRepository
{
    public interface IBookMarkRepository : IRepository<BookMark>
    {
        void Update(BookMark obj);
    }
}
