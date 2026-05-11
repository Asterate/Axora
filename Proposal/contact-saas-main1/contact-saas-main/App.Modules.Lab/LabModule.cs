using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Infrastructure.Repositories;
using App.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.Modules.Lab.Infrastructure;

public static class LabDbModule
{
    public static IServiceCollection AddLabModule(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<LabDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<ILabRepository, LabRepository>();
        services.AddScoped<ILabTypeRepository, LabTypeRepository>();
        services.AddScoped<IEquipmentLabRepository, EquipmentLabRepository>();
        services.AddScoped<IReagentLabRepository, ReagentLabRepository>();
        services.AddScoped<IInstituteLabRepository, InstituteLabRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<LabService>();
        services.AddScoped<LabTypeService>();
        services.AddScoped<InstituteLabService>();
        services.AddScoped<EquipmentLabService>();
        services.AddScoped<ReagentLabService>();
        
        return services;
    }
}