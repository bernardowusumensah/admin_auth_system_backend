using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.Interfaces
{
    public interface ISqlGenericRepository
    {
        Task<Account?> GetAccountByEmailAsync(string email);
        Task AddAccountAsync(Account account);
        Task SaveChangesAsync();
    }
}
