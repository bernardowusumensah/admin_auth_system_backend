using MediatR;
using UserIdentity.Application.Interfaces;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.Features.Admin.Support.Commands;

public class AddInternalNoteCommandHandler : IRequestHandler<AddInternalNoteCommand, bool>
{
    private readonly ISqlGenericRepository _repository;

    public AddInternalNoteCommandHandler(ISqlGenericRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(AddInternalNoteCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetSupportTicketByIdAsync(request.TicketId);
        if (ticket == null) return false;

        var note = new InternalNote
        {
            Id = Guid.NewGuid(),
            TicketId = ticket.Id,
            Note = request.Note,
            Author = request.Author,
            Timestamp = DateTime.UtcNow
        };
        // Append note (immutable notes: no edits after creation)
        await _repository.AddInternalNoteAsync(note);

        // Update last updated time only
        ticket.LastUpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(ticket);
        return true;
    }
}
