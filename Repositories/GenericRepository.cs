using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MONKEY5.DataAccessObjects;

namespace MONKEY5.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly MyDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(MyDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();  // Get DbSet for any entity type
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();  // Fetch all records
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);  // Find record by ID
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();  // Find records based on a condition
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);  // Add new record
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);  // Update existing record
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);  // Delete record
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();  // Save changes to the database
        }
    }
}
