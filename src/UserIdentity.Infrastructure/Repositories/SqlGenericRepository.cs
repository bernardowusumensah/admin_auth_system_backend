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

    // Support Tickets specialized operations
    public async Task<(IEnumerable<SupportTicket> Tickets, int TotalCount)> GetSupportTicketsAsync(
        string? search,
        TicketStatus? status,
        string? category,
        string? assignedTo,
        DateTime? fromDate,
        DateTime? toDate,
        int page,
        int pageSize)
    {
        IQueryable<SupportTicket> query = _context.SupportTickets.Include(t => t.InternalNotes);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.ToLower();
            query = query.Where(t => (t.PlayerUsername != null && t.PlayerUsername.ToLower().Contains(s)) ||
                                      (t.PlayerEmail != null && t.PlayerEmail.ToLower().Contains(s)) ||
                                      (t.Subject != null && t.Subject.ToLower().Contains(s)));
        }
        if (status.HasValue)
            query = query.Where(t => t.Status == status);
        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(t => t.Category == category);
        if (!string.IsNullOrWhiteSpace(assignedTo))
            query = query.Where(t => t.AssignedTo == assignedTo);
        if (fromDate.HasValue)
            query = query.Where(t => t.SubmittedAt >= fromDate);
        if (toDate.HasValue)
            query = query.Where(t => t.SubmittedAt <= toDate);

        var total = await query.CountAsync();

        var tickets = await query
            .OrderByDescending(t => t.LastUpdatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (tickets, total);
    }

    public async Task<SupportTicket?> GetSupportTicketByIdAsync(Guid id)
    {
        return await _context.SupportTickets
            .Include(t => t.InternalNotes)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AddInternalNoteAsync(InternalNote note)
    {
        await _context.InternalNotes.AddAsync(note);
        await _context.SaveChangesAsync();
    }
}


