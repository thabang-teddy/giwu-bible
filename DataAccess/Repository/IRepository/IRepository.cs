using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - Category
        IEnumerable<T> GetAll(string? includeProperties = null);
        IEnumerable<T> GetRow();
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        void Add(T entity);
        Task AddAsync(T entity);
        void AddRange(List<T> entities);
        Task AddRangeAsync(List<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
