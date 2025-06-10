using System.Linq.Expressions;
using Dreslay.Models;

namespace Dreslay.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByNameAsync(string name);
        Task<T?> GetByEmailAsync(string email);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> DeleteByNameAsync(string name);
        Task<bool> DeleteByEmailAsync(string email);
        Task SaveAsync();
        Task <IEnumerable<T>> FindAsync(Expression<Func<T, bool>>predicate);

    }
}
