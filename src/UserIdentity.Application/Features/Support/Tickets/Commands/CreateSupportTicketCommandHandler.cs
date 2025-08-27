using MediatR;
using UserIdentity.Application.DTOs.Support;
using UserIdentity.Application.Interfaces;
using UserIdentity.Application.Mapping;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.Features.Support.Tickets.Commands;

public class CreateSupportTicketCommandHandler : IRequestHandler<CreateSupportTicketCommand, SupportTicketDto>
{
    private readonly ISqlGenericRepository _repository;

    public CreateSupportTicketCommandHandler(ISqlGenericRepository repository)
    {
        _repository = repository;
    }

    public async Task<SupportTicketDto> Handle(CreateSupportTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = new SupportTicket
        {
            Id = Guid.NewGuid(),
            Status = TicketStatus.New,
            SubmittedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            PlayerUsername = request.Username,
            PlayerEmail = request.Email,
            Category = request.Category,
            Subject = request.Subject,
            Details = request.Details,
            InternalNotes = new List<InternalNote>()
        };

        await _repository.InsertAsync(ticket);
        return ticket.ToDto();
    }
}
