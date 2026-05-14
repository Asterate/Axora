using App.Shared.Contracts.Dtos;
using MediatR;

namespace App.Shared.Contracts.Events;

public record UserRegisteredEvent(
    Guid UserId,
    string Email,
    InstituteSelectionType InstituteSelection,
    Guid? ExistingInstituteId,
    NewInstituteDto? NewInstitute
) : INotification;