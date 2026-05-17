using App.Modules.Audit.Application.Interface;
using App.Modules.Audit.Application.Services;
using App.Modules.Audit.Infrastructure;
using App.Modules.Audit.Infrastructure.Repositories;
using App.Shared.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace App.Modules.Audit;

public static class AuditModule
{
    public static IServiceCollection AddAuditModule(
        this IServiceCollection services)
    {

        // Repositories
        services.AddScoped<ISystemLogRepository, SystemLogRepository>();

        // UoW
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddScoped<ISystemLogService, SystemLogService>();
      
        return services;
    }
}