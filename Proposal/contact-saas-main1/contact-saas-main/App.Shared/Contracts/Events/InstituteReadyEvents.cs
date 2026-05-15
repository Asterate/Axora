using MediatR;

namespace App.Shared.Contracts.Events;

public record InstituteReadyEvent(
    Guid UserId,
    Guid InstituteId
) : INotification;