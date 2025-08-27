using MediatR;

namespace UserIdentity.Application.Features.Admin.Support.Commands;

public class AssignTicketCommand : IRequest<bool>
{
    public Guid TicketId { get; init; }
    public string? AssignedTo { get; init; }
}
