using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Infrastructure.Data.Configurations;

public class RequiredActionConfiguration : IEntityTypeConfiguration<RequiredAction>
{
    public void Configure(EntityTypeBuilder<RequiredAction> builder)
    {
        builder.ToTable("required_actions");
        builder.HasKey(ra => ra.Id);
        builder.Property(ra => ra.RequiredActionType).HasColumnName("required_action_type");
        builder.Property(ra => ra.IsCompleted).HasColumnName("is_completed");
        builder.Property(ra => ra.CreatedOn).HasColumnName("created_on");
        builder.Property(ra => ra.AccountId).HasColumnName("account_id");
        
        builder.HasOne(ra => ra.Account)
            .WithMany(a => a.RequiredActions)
            .HasForeignKey(ra => ra.AccountId);
    }
}
