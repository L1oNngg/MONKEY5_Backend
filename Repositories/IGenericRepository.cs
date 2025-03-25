using System;
using System.Linq.Expressions;

namespace MONKEY5.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();  // Get all records
        Task<T?> GetByIdAsync(Guid id);  // Get a record by ID
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);  // Find by condition
        Task AddAsync(T entity);  // Create new record
        void Update(T entity);  // Update existing record
        void Delete(T entity);  // Delete record
        Task SaveChangesAsync();  // Save changes to the database
    }
}

