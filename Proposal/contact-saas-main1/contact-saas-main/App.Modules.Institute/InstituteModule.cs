using App.Modules.Institute.Infrastructure;
using App.Modules.Institute.Infrastructure.Repositories;
using App.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.Modules.Institute;

public static class InstituteModule
{
    public static IServiceCollection AddInstituteModule(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<InstituteDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<InstituteRepository>();
        services.AddScoped<InstituteProjectRepository>();
        services.AddScoped<InstituteTypeRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<InstituteService>();
        services.AddScoped<InstituteProjectService>();
        services.AddScoped<InstituteTypeService>();
        
        return services;
    }
}