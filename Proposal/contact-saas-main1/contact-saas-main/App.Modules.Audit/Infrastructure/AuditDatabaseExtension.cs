using App.Modules.Audit.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App.Modules.Audit.Infrastructure;

public static class AuditDatabaseExtensions
{
    public static IServiceCollection AddAuditDatabase(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<AuditDbContext>(options =>
        {
            options.UseNpgsql(connectionString);

            if (!environment.IsProduction())
            {
                options.EnableDetailedErrors()
                    .EnableSensitiveDataLogging();
            }
        });

        return services;
    }
}