using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Infrastructure.Data.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("accounts");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Email).HasColumnName("email");
        builder.Property(a => a.HashedPassword).HasColumnName("hashed_password");
        builder.Property(a => a.Username).HasColumnName("username");
        builder.Property(a => a.EmailConfirmation).HasColumnName("email_confirmation");
        builder.Property(a => a.LockedOut).HasColumnName("locked_out");
        builder.Property(a => a.CreatedOn).HasColumnName("created_on");
        builder.Property(a => a.UserId).HasColumnName("user_id");
        
        builder.HasOne(a => a.User)
            .WithOne()
            .HasForeignKey<Account>(a => a.UserId);
            
        builder.HasMany(a => a.RequiredActions)
            .WithOne(ra => ra.Account)
            .HasForeignKey(ra => ra.AccountId);
            
        builder.HasMany(a => a.Subscriptions)
            .WithOne(s => s.Account)
            .HasForeignKey(s => s.AccountId);
    }
}
