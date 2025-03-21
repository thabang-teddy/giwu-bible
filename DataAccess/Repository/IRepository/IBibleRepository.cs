using Models;

namespace DataAccess.Repository.IRepository
{
    public interface IBibleRepository : IRepository<Bible>
    {
        void Update(Bible obj);
    }
}
