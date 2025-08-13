using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Infrastructure.Data.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("addresses");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Street).HasColumnName("street");
        builder.Property(a => a.City).HasColumnName("city");
        builder.Property(a => a.Country).HasColumnName("country");
        builder.Property(a => a.PostalCode).HasColumnName("postal_code");
        builder.Property(a => a.UserId).HasColumnName("user_id");
        
        builder.HasOne(a => a.User)
            .WithOne(u => u.Address)
            .HasForeignKey<Address>(a => a.UserId);
    }
}
