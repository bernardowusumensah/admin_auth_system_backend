using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.Interfaces
{
    public interface ISqlGenericRepository
    {
        Task<Account?> GetAccountByEmailAsync(string email);
        Task AddAccountAsync(Account account);
        Task SaveChangesAsync();
        
        // Generic CRUD operations
        Task<T?> GetByIdAsync<T>(Guid id) where T : class;
        Task<IEnumerable<T>> GetAllAsync<T>(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null) where T : class;
        Task InsertAsync<T>(T entity) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
    }
}
