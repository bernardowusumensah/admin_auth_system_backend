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
        builder.Property(a => a.UserId).HasColumnName("user_id");
        builder.HasOne(a => a.User)
            .WithOne()
            .HasForeignKey<Account>(a => a.UserId);
    }
}
