using App.Modules.Equipment.Infrastructure;
using App.Modules.Experiment.Application.Interfaces;
using App.Modules.Experiment.Infrastructure;
using App.Modules.Experiment.Infrastructure.Repositories;
using App.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.Modules.Experiment;

public static class ExperimentModule
{
    public static IServiceCollection AddExperimentModule(
        this IServiceCollection services,
        string connectionString)
    {
        // DbContext
        services.AddDbContext<ExperimentDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Repositories
        services.AddScoped<IExperimentRepository, ExperimentRepository>();
        services.AddScoped<IExperimentTypeRepository, ExperimentTypeRepository>();
        services.AddScoped<IExperimentTaskRepository, ExperimentTaskRepository>();
        services.AddScoped<IExperimentTaskTypeRepository, ExperimentTaskTypeRepository>();
        services.AddScoped<IExperimentEquipmentRepository, ExperimentEquipmentRepository>();

        // UoW
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddScoped<ExperimentService>();
        services.AddScoped<ExperimentTypeService>();
        services.AddScoped<ExperimentTaskService>();
        services.AddScoped<ExperimentTaskTypeService>();
        services.AddScoped<ExperimentEquipmentService>();

        return services;
    }
}