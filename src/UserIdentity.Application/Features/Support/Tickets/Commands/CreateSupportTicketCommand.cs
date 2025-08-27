using MediatR;
using UserIdentity.Application.DTOs.Support;

namespace UserIdentity.Application.Features.Support.Tickets.Commands;

public class CreateSupportTicketCommand : IRequest<SupportTicketDto>
{
    public string? Username { get; init; }
    public string? Email { get; init; }
    public string? Category { get; init; }
    public string? Subject { get; init; }
    public string? Details { get; init; }
}
