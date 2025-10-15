using Aurex_Core.Interfaces;
using Aurex_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Aurex_Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AurexDBcontext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AurexDBcontext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // ✅ Get all (read-only)
        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        // ✅ Get by Id
        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        // ✅ Find with condition
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

      
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

      
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

       
        public IQueryable<T> GetQueryable(bool asTracking = false)
        {
            return asTracking ? _dbSet.AsQueryable() : _dbSet.AsNoTracking();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            return predicate == null
                ? await _dbSet.CountAsync(cancellationToken)
                : await _dbSet.CountAsync(predicate, cancellationToken);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(predicate, cancellationToken);
        }
    }
}
