using App.Modules.Audit.Application.Interface;
using App.Modules.Audit.Infrastructure;
using App.Modules.Equipment.Infrastructure.Data;
using App.Modules.Equipment.Infrastructure.Repositories;
using App.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class AuditModule
{
    public static IServiceCollection AddAuditModule(
        this IServiceCollection services,
        string connectionString)
    {
        // DbContext
        services.AddDbContext<AuditDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Repositories
        services.AddScoped<ISystemLogRepository, SystemLogRepository>();

        // UoW
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddScoped<SystemLogService>();
      
        return services;
    }
}