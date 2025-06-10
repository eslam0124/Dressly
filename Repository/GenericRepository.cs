using System;
using System.Linq.Expressions;
using Dreslay.Models;
using Dreslay.Repository;
using Microsoft.EntityFrameworkCore;
namespace Dreslay.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DresslyContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DresslyContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<T?> GetByNameAsync(string name)
        {
            var property = typeof(T).GetProperty("Name");
            if (property == null) return null;

            return await _dbSet.FirstOrDefaultAsync(e =>
                EF.Property<string>(e, "Name").ToLower() == name.ToLower());
        }

        public async Task<T?> GetByEmailAsync(string email)
        {
            var property = typeof(T).GetProperty("Email");
            if (property == null) return null;

            return await _dbSet.FirstOrDefaultAsync(e =>
                EF.Property<string>(e, "Email").ToLower() == email.ToLower());
        }

        public async Task<bool> DeleteByNameAsync(string name)
        {
            var entity = await GetByNameAsync(name);
            if (entity == null) return false;
            _dbSet.Remove(entity);
            return true;
        }

        public async Task<bool> DeleteByEmailAsync(string email)
        {
            var entity = await GetByEmailAsync(email);
            if (entity == null) return false;
            _dbSet.Remove(entity);
            return true;
        }

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public Task AddAsync(Admin admin)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T,bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }
    }
}