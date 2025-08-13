using System;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Domain.Entities;
using UserIdentity.Domain.ValueObjects;

namespace UserIdentity.Infrastructure.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<RequiredAction> RequiredActions { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
