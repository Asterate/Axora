using App.Modules.Equipment.Infrastructure.Repositories;
using App.Modules.Identity.Infrastructure;
using App.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.Modules.Identity;

public static class IdentityModule
{
    public static IServiceCollection AddIdentityModule(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<IdentityDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<InstituteUserRepository>();
        services.AddScoped<AppRefreshTokenRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<AppRefreshTokenService>();
        services.AddScoped<InstituteUserService>();
        
        return services;
    }
}