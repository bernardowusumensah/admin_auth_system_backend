using System;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Infrastructure.Repositories;

public interface ISqlGenericRepository
{
        Task<Account?> GetAccountByEmailAsync(string email);
        Task AddAccountAsync(Account account);
        Task SaveChangesAsync();
}
