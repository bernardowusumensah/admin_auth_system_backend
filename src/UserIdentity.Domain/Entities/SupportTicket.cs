using System.ComponentModel.DataAnnotations.Schema;

namespace UserIdentity.Domain.Entities;

public class SupportTicket
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("status")]
    public TicketStatus Status { get; set; }

    [Column("submitted_at")]
    public DateTime SubmittedAt { get; set; }

    [Column("last_updated_at")]
    public DateTime LastUpdatedAt { get; set; }

    [Column("assigned_to")]
    public string? AssignedTo { get; set; }

    // Player info (denormalized for quick admin view)
    [Column("player_username")]
    public string? PlayerUsername { get; set; }

    [Column("player_email")]
    public string? PlayerEmail { get; set; }

    // Issue details
    [Column("category")]
    public string? Category { get; set; }

    [Column("subject")]
    public string? Subject { get; set; }

    [Column("details")]
    public string? Details { get; set; }

    // Navigation
    public List<InternalNote> InternalNotes { get; set; } = new();
}

public class InternalNote
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("ticket_id")]
    public Guid TicketId { get; set; }

    [Column("note")] 
    public string? Note { get; set; }

    [Column("author")] 
    public string? Author { get; set; }

    [Column("timestamp")]
    public DateTime Timestamp { get; set; }

    // Navigation
    public SupportTicket Ticket { get; set; } = null!;
}

public enum TicketStatus
{
    New = 0,
    Open = 1,
    PendingPlayerResponse = 2,
    Closed = 3
}
