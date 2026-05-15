using App.Shared.Contracts.Dtos;
using MediatR;

namespace App.Shared.Contracts.Events;

// Shared/Contracts/Events/UserRegisteredEvent.cs
public record UserRegisteredEvent(
    Guid UserId,
    string Email,
    bool IsNewInstitute,
    Guid? ExistingInstituteId,
    string? NewInstituteName,
    string? NewInstituteCountry,
    string? NewInstituteAddress,
    string? NewInstitutePhone,
    Guid? NewInstituteTypeId 
) : INotification;