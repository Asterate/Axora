using MediatR;

namespace App.Shared.Contracts.Events;

public class ScheduleEvent
{
    public record GetLabByIdQuery(Guid Id) : IRequest<LookupItem?>;
    public record GetEquipmentByIdQuery(Guid Id) : IRequest<LookupItem?>;
}