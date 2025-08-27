using UserIdentity.Application.DTOs.Support;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.Mapping;

public static class SupportTicketMappingExtensions
{
    public static SupportTicketDto ToDto(this SupportTicket ticket)
    {
        return new SupportTicketDto
        {
            TicketId = ticket.Id.ToString(),
            Status = ticket.Status,
            SubmittedAt = ticket.SubmittedAt,
            LastUpdatedAt = ticket.LastUpdatedAt,
            AssignedTo = ticket.AssignedTo,
            PlayerInfo = new PlayerInfoDto
            {
                Username = ticket.PlayerUsername,
                Email = ticket.PlayerEmail
            },
            IssueDetails = new IssueDetailsDto
            {
                Category = ticket.Category,
                Subject = ticket.Subject,
                Details = ticket.Details
            },
            InternalNotes = ticket.InternalNotes?.OrderBy(n => n.Timestamp).Select(n => new InternalNoteDto
            {
                Id = n.Id,
                Note = n.Note,
                Author = n.Author,
                Timestamp = n.Timestamp
            }).ToList()
        };
    }
}
