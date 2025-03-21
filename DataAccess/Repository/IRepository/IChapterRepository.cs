using Models;

namespace DataAccess.Repository.IRepository
{
    public interface IChapterRepository : IRepository<Chapter>
    {
        void Update(Chapter obj);
    }
}
