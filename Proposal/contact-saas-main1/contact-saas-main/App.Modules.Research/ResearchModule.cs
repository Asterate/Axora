using App.Modules.Experiment.Application.Interfaces;
using App.Modules.Experiment.Infrastructure.Repositories;
using App.Modules.Institute.Infrastructure.Repositories;
using App.Modules.Lab.Infrastructure;
using App.Modules.Lab.Infrastructure.Repositories;
using App.Modules.Project.Application.Interfaces;
using App.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.Modules.Project.Infrastructure;

public static class ProjectDbModule
{
    public static IServiceCollection AddProjectModule(
        this IServiceCollection services)
    {

        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectTypeRepository, ProjectTypeRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
        services.AddScoped<IDocumentResultRepository, DocumentResultRepository>();
        services.AddScoped<IResultRepository, ResultRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();
        services.AddScoped<InstituteRepository>();
        services.AddScoped<InstituteProjectRepository>();
        services.AddScoped<InstituteTypeRepository>();
        services.AddScoped<IExperimentRepository, ExperimentRepository>();
        services.AddScoped<IExperimentTypeRepository, ExperimentTypeRepository>();
        services.AddScoped<IExperimentTaskRepository, ExperimentTaskRepository>();
        services.AddScoped<IExperimentTaskTypeRepository, ExperimentTaskTypeRepository>();
        services.AddScoped<IExperimentEquipmentRepository, ExperimentEquipmentRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<DocumentService>();
        services.AddScoped<DocumentTypeService>();
        services.AddScoped<ProjectService>();
        services.AddScoped<ScheduleService>();
        services.AddScoped<ProjectTypeService>();
        services.AddScoped<ResultService>();
        services.AddScoped<DocumentResultService>();
        services.AddScoped<InstituteService>();
        services.AddScoped<InstituteProjectService>();
        services.AddScoped<InstituteTypeService>();
        services.AddScoped<ExperimentService>();
        services.AddScoped<ExperimentTypeService>();
        services.AddScoped<ExperimentTaskService>();
        services.AddScoped<ExperimentTaskTypeService>();
        services.AddScoped<ExperimentEquipmentService>();

        
        return services;
    }
}