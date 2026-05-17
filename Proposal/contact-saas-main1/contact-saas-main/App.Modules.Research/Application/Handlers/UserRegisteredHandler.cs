using App.Modules.Project.Application.DTO;
using App.Modules.Project.Application.Services;
using App.Shared.Contracts.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.Modules.Project.Application.Handlers;

public class UserRegisteredHandler : INotificationHandler<UserRegisteredEvent>
{
    private readonly InstituteService _instituteService;
    private readonly IMediator _mediator;  
    private readonly ILogger<UserRegisteredHandler> _logger;

    public UserRegisteredHandler(
        InstituteService instituteService, InstituteTypeService instituteTypeService,
        IMediator mediator,
        ILogger<UserRegisteredHandler> logger)
    {
        _instituteService = instituteService;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(UserRegisteredEvent e, CancellationToken ct)
    {
        Domain.Institute institute;

        if (e.IsNewInstitute)
        {
            institute = await _instituteService.CreateAndReturnAsync(new SaveInstituteRequest
            {
                InstituteName = e.NewInstituteName ?? "??",
                InstituteCountry = e.NewInstituteCountry ?? "??",
                InstituteAddress = e.NewInstituteAddress ?? "??",
                InstitutePhoneNumber = e.NewInstitutePhone ?? "??",
                InstituteTypeId = e.NewInstituteTypeId!.Value,
                CreatedAt = DateTime.UtcNow,
                Active = true
            });

            _logger.LogInformation("New institute {Name} created for {Email}", 
                institute.InstituteName, e.Email);
        }
        else
        {
            institute = await _instituteService.GetEntityByIdAsync(e.ExistingInstituteId!.Value)
                        ?? throw new InvalidOperationException($"Institute {e.ExistingInstituteId} not found");
        }

        await _mediator.Publish(new InstituteReadyEvent(
            UserId: e.UserId,
            InstituteId: institute.Id
        ), ct);

        _logger.LogInformation("InstituteUser created for {Email}", e.Email);
    }
}