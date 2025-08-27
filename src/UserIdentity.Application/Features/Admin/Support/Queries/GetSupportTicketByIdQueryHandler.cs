using MediatR;
using UserIdentity.Application.DTOs.Support;
using UserIdentity.Application.Interfaces;
using UserIdentity.Application.Mapping;

namespace UserIdentity.Application.Features.Admin.Support.Queries;

public class GetSupportTicketByIdQueryHandler : IRequestHandler<GetSupportTicketByIdQuery, SupportTicketDto?>
{
    private readonly ISqlGenericRepository _repository;

    public GetSupportTicketByIdQueryHandler(ISqlGenericRepository repository)
    {
        _repository = repository;
    }

    public async Task<SupportTicketDto?> Handle(GetSupportTicketByIdQuery request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetSupportTicketByIdAsync(request.Id);
        return ticket?.ToDto();
    }
}
