using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App.Modules.Identity.Infrastructure;

public static class IdentityDatabaseExtension
{
    public static IServiceCollection AddIdentityDatabase(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<IdentityModuleDbContext>(options =>
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