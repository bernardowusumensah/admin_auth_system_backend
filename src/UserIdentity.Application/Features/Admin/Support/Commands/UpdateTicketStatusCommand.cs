using MediatR;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.Features.Admin.Support.Commands;

public class UpdateTicketStatusCommand : IRequest<bool>
{
    public Guid Id { get; init; }
    public TicketStatus NewStatus { get; init; }
}
