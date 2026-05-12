using App.DAL.EF;
using App.Domain.Identity;
using App.Modules.Identity.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.Setup;

public static class IdentitySetupExtensions
{
    public static IServiceCollection AddAppIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<AppUser, AppRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddDefaultUI()
            .AddEntityFrameworkStores<IdentityModuleDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}