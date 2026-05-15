using App.Domain.Entities;
using App.Modules.Identity.Application.Services;
using App.Shared.Contracts.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.Modules.Identity.Application.Handlers;

// Modules/Identity/Application/Handlers/InstituteUserHandler.cs
public class InstituteUserHandler : INotificationHandler<InstituteReadyEvent>
{
    private readonly InstituteUserService _instituteUserService;
    private readonly ILogger<InstituteUserHandler> _logger;

    public InstituteUserHandler(
        InstituteUserService instituteUserService,
        ILogger<InstituteUserHandler> logger)
    {
        _instituteUserService = instituteUserService;
        _logger = logger;
    }

    public async Task Handle(InstituteReadyEvent e, CancellationToken ct)
    {
        await _instituteUserService.CreateAsync(new CreateInstituteUserRequest
        {
            Id = Guid.NewGuid(),
            InstituteId = e.InstituteId,
            UserId = e.UserId,
            Role = EInstituteUserRole.Employee
        });

        _logger.LogInformation("InstituteUser created for user {UserId} in institute {InstituteId}",
            e.UserId, e.InstituteId);
    }
}