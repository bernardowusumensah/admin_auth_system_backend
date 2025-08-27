using MediatR;

namespace UserIdentity.Application.Features.Admin.Support.Commands;

public class AddInternalNoteCommand : IRequest<bool>
{
    public Guid TicketId { get; init; }
    public string? Note { get; init; }
    public string? Author { get; init; }
}
