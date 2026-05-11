using App.Modules.Reagent.Application.Interfaces;
using App.Modules.Reagent.Infrastructure;
using App.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class ReagentDbModule
{
    public static IServiceCollection AddReagentModule(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ReagentDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IReagentRepository, ReagentRepository>();
        services.AddScoped<IReagentTypeRepository, ReagentTypeRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<ReagentService>();
        services.AddScoped<ReagentTypeService>();
    
        return services;
    }
}