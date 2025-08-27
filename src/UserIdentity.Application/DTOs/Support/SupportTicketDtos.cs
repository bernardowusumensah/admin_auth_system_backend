using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.DTOs.Support;

public class SupportTicketDto
{
    public string? TicketId { get; set; }
    public TicketStatus Status { get; set; }
    public DateTime SubmittedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public string? AssignedTo { get; set; }
    public PlayerInfoDto? PlayerInfo { get; set; }
    public IssueDetailsDto? IssueDetails { get; set; }
    public List<InternalNoteDto>? InternalNotes { get; set; }
}

public class PlayerInfoDto
{
    public string? Username { get; set; }
    public string? Email { get; set; }
}

public class IssueDetailsDto
{
    public string? Category { get; set; }
    public string? Subject { get; set; }
    public string? Details { get; set; }
}

public class InternalNoteDto
{
    public Guid Id { get; set; }
    public string? Note { get; set; }
    public string? Author { get; set; }
    public DateTime Timestamp { get; set; }
}

public class UpdateTicketStatusRequest
{
    public string? TicketId { get; set; }
    public TicketStatus NewStatus { get; set; }
}

public class AddInternalNoteRequest
{
    public string? TicketId { get; set; }
    public string? NoteContent { get; set; }
}

public class AssignTicketRequest
{
    public string? TicketId { get; set; }
    public string? AssignedTo { get; set; }
}
