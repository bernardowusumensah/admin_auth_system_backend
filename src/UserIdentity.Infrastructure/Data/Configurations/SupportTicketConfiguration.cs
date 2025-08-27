using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Infrastructure.Data.Configurations;

public class SupportTicketConfiguration : IEntityTypeConfiguration<SupportTicket>
{
    public void Configure(EntityTypeBuilder<SupportTicket> builder)
    {
        builder.ToTable("support_tickets");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("id");
        builder.Property(t => t.Status).HasColumnName("status").HasConversion<int>();
        builder.Property(t => t.SubmittedAt).HasColumnName("submitted_at");
        builder.Property(t => t.LastUpdatedAt).HasColumnName("last_updated_at");
        builder.Property(t => t.AssignedTo).HasColumnName("assigned_to").HasMaxLength(100);

        builder.Property(t => t.PlayerUsername).HasColumnName("player_username").HasMaxLength(100);
        builder.Property(t => t.PlayerEmail).HasColumnName("player_email").HasMaxLength(200);

        builder.Property(t => t.Category).HasColumnName("category").HasMaxLength(100);
        builder.Property(t => t.Subject).HasColumnName("subject").HasMaxLength(200);
        builder.Property(t => t.Details).HasColumnName("details");

        builder.HasMany(t => t.InternalNotes)
            .WithOne(n => n.Ticket)
            .HasForeignKey(n => n.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.Status);
        builder.HasIndex(t => t.SubmittedAt);
        builder.HasIndex(t => t.PlayerUsername);
        builder.HasIndex(t => t.PlayerEmail);
    }
}

public class InternalNoteConfiguration : IEntityTypeConfiguration<InternalNote>
{
    public void Configure(EntityTypeBuilder<InternalNote> builder)
    {
        builder.ToTable("support_ticket_notes");
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Id).HasColumnName("id");
        builder.Property(n => n.TicketId).HasColumnName("ticket_id");
        builder.Property(n => n.Note).HasColumnName("note");
        builder.Property(n => n.Author).HasColumnName("author").HasMaxLength(100);
        builder.Property(n => n.Timestamp).HasColumnName("timestamp");

        builder.HasIndex(n => n.TicketId);
    }
}
