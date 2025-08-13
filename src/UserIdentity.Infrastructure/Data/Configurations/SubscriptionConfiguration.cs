using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Infrastructure.Data.Configurations;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("subscriptions");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.SubscriptionType).HasColumnName("subscription_type");
        builder.Property(s => s.Status).HasColumnName("status");
        builder.Property(s => s.Plan).HasColumnName("plan");
        builder.Property(s => s.StartDate).HasColumnName("start_date");
        builder.Property(s => s.EndDate).HasColumnName("end_date");
        builder.Property(s => s.CreatedOn).HasColumnName("created_on");
        builder.Property(s => s.UpdatedOn).HasColumnName("updated_on");
        builder.Property(s => s.AccountId).HasColumnName("account_id");
        
        builder.HasOne(s => s.Account)
            .WithMany(a => a.Subscriptions)
            .HasForeignKey(s => s.AccountId);
    }
}
