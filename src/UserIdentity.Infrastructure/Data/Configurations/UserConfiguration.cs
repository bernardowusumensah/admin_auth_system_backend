using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.FirstName).HasColumnName("first_name");
        builder.Property(u => u.LastName).HasColumnName("last_name");
          builder.OwnsMany(u => u.UserRoles, ur =>
        {
            ur.ToTable("user_roles");
            ur.WithOwner().HasForeignKey("user_id");
            ur.Property<Guid>("id").HasColumnName("id");
            ur.Property(r => r.Role).HasColumnName("role");
            ur.HasKey("id");
        });

           
    }
}
