using App.Modules.Equipment.Infrastructure.Repositories;
using App.Modules.Identity.Application.Handlers;
using App.Modules.Identity.Application.Interfaces;
using App.Modules.Identity.Application.Services;
using App.Modules.Identity.Infrastructure;
using App.Modules.Identity.Infrastructure.Repositories;
using App.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.Modules.Identity;

public static class IdentityModule
{
    public static IServiceCollection AddIdentityModule(
        this IServiceCollection services)
    {
        services.AddScoped<IAppRefreshTokenRepository, AppRefreshTokenRepository>();
        services.AddScoped<IInstituteUserRepository, InstituteUserRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    
        services.AddScoped<IAppRefreshTokenService,AppRefreshTokenService>();
        services.AddScoped<IInstituteUserService, InstituteUserService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(GetInstituteUserIdHandler.GetInstituteIdByUserIdHandler).Assembly));
    
        return services;
    }
}