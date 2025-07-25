using System;
using UserIdentity.Domain.Entities;
using UserIdentity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace UserIdentity.Infrastructure.Repositories;

public class SqlGenericRepository : ISqlGenericRepository
{
    private readonly AppDbContext _context;

    public SqlGenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetAccountByEmailAsync(string email)
    {
        return await _context.Accounts
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Email == email);
    }

    public async Task AddAccountAsync(Account account)
    {
        await _context.Accounts.AddAsync(account);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}


