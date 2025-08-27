using MediatR;
using UserIdentity.Application.DTOs.Support;

namespace UserIdentity.Application.Features.Admin.Support.Queries;

public class GetSupportTicketByIdQuery : IRequest<SupportTicketDto?>
{
    public Guid Id { get; init; }
}
