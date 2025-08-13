using System;
using System.Linq.Expressions;
using UserIdentity.Application.Interfaces;
using UserIdentity.Domain.Entities;
using UserIdentity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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

    // Generic CRUD operations
    public async Task<T?> GetByIdAsync<T>(Guid id) where T : class
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync<T>(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null) where T : class
    {
        IQueryable<T> query = _context.Set<T>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (include != null)
        {
            query = include(query);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }

    public async Task InsertAsync<T>(T entity) where T : class
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync<T>(T entity) where T : class
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync<T>(T entity) where T : class
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}


