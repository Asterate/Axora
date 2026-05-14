// Modules/Institutions/Application/Handlers/UserRegisteredHandler.cs

using App.Modules.Project.Application.Interfaces;
using App.Shared.Contracts.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.Modules.Project.Application.Handlers;

public class UserRegisteredHandler : INotificationHandler<UserRegisteredEvent>
{
    private readonly InstituteService _instituteService;
    private readonly InstituteUserService _instituteUserService;
    private readonly InstituteTypeService _instituteTypeService;
    private readonly ILogger<UserRegisteredHandler> _logger;

    public UserRegisteredHandler(
        InstituteService instituteService,
        InstituteUserService instituteUserService,
        InstituteTypeService instituteTypeService,
        ILogger<UserRegisteredHandler> logger)
    {
        _instituteService = instituteService;
        _instituteUserService = instituteUserService;
        _instituteTypeService = instituteTypeService;
        _logger = logger;
    }

    public async Task Handle(UserRegisteredEvent e, CancellationToken ct)
    {
        Domain.Entities.Institute institute;

        if (e.InstituteSelection == InstituteSelectionType.CreateNew)
        {
            var dto = e.NewInstitute!;
            institute = await _instituteService.CreateAndReturnAsync(new CreateInstituteRequest
            {
                Id = Guid.NewGuid(),
                InstituteName = dto.InstituteName,
                InstituteCountry = dto.InstituteCountry,
                InstituteAddress = dto.InstituteAddress,
                InstitutePhoneNumber = dto.InstitutePhoneNumber,
                InstituteTypeId = dto.InstituteTypeId,
                CreatedAt = DateTime.UtcNow,
                Active = true
            });
            _logger.LogInformation("New institute {Name} created for {Email}", institute.InstituteName, e.Email);
        }
        else
        {
            institute = await _instituteService.GetEntityByIdAsync(e.ExistingInstituteId!.Value)
                        ?? throw new InvalidOperationException($"Institute {e.ExistingInstituteId} not found");
        }

        await _instituteUserService.CreateAsync(new CreateInstituteUserRequest
        {
            Id = Guid.NewGuid(),
            InstituteId = institute.Id,
            UserId = e.UserId,
            Role = EInstituteUserRole.Employee
        });

        _logger.LogInformation("InstituteUser created for {Email}", e.Email);
    }
}