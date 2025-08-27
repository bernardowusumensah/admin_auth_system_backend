using MediatR;
using UserIdentity.Application.Interfaces;

namespace UserIdentity.Application.Features.Admin.Support.Commands;

public class UpdateTicketStatusCommandHandler : IRequestHandler<UpdateTicketStatusCommand, bool>
{
    private readonly ISqlGenericRepository _repository;

    public UpdateTicketStatusCommandHandler(ISqlGenericRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateTicketStatusCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetSupportTicketByIdAsync(request.Id);
        if (ticket == null) return false;
        ticket.Status = request.NewStatus;
        ticket.LastUpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(ticket);
        return true;
    }
}
