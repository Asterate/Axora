using MediatR;

namespace App.Shared.Contracts.Events;

public class InstituteUserEvent
{
    public record GetInstituteIdByUserIdQuery(Guid UserId) : IRequest<Guid?>;

}