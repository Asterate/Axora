using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App.Modules.Lab.Infrastructure;

public static class LabDatabaseExtension
{
    public static IServiceCollection AddLabDatabase(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<LabDbContext>(options =>
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