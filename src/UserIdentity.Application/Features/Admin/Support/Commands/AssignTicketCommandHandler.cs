using MediatR;
using UserIdentity.Application.Interfaces;

namespace UserIdentity.Application.Features.Admin.Support.Commands;

public class AssignTicketCommandHandler : IRequestHandler<AssignTicketCommand, bool>
{
    private readonly ISqlGenericRepository _repository;

    public AssignTicketCommandHandler(ISqlGenericRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(AssignTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetSupportTicketByIdAsync(request.TicketId);
        if (ticket == null) return false;
        ticket.AssignedTo = request.AssignedTo; // allow null to unassign
        ticket.LastUpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(ticket);
        return true;
    }
}
