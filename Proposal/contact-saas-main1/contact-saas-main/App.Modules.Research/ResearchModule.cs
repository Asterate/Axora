using App.Modules.Experiment.Application.Interfaces;
using App.Modules.Experiment.Infrastructure.Repositories;
using App.Modules.Institute.Application.Interfaces;
using App.Modules.Institute.Infrastructure.Repositories;
using App.Modules.Lab.Infrastructure;
using App.Modules.Lab.Infrastructure.Repositories;
using App.Modules.Project.Application.Interfaces;
using App.Modules.Project.Application.Interfaces.Service;
using App.Modules.Project.Application.Services;
using App.Modules.Project.Infrastructure.Repositories;
using App.Shared.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace App.Modules.Project;

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
        services.AddScoped<IInstituteRepository, InstituteRepository>();
        services.AddScoped<IInstituteProjectRepository, InstituteProjectRepository>();
        services.AddScoped<IInstituteTypeRepository, InstituteTypeRepository>();
        services.AddScoped<IExperimentRepository, ExperimentRepository>();
        services.AddScoped<IExperimentTypeRepository, ExperimentTypeRepository>();
        services.AddScoped<IExperimentTaskRepository, ExperimentTaskRepository>();
        services.AddScoped<IExperimentTaskTypeRepository, ExperimentTaskTypeRepository>();
        services.AddScoped<IExperimentEquipmentRepository, ExperimentEquipmentRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IDocumentService,DocumentService>();
        services.AddScoped<IDocumentTypeService, DocumentTypeService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IScheduleService, ScheduleService>();
        services.AddScoped<IProjectTypeService, ProjectTypeService>();
        services.AddScoped<IResultService, ResultService>();
        services.AddScoped<IDocumentResultService, DocumentResultService>();
        services.AddScoped<IInstituteService, InstituteService>();
        services.AddScoped<IInstituteProjectService, InstituteProjectService>();
        services.AddScoped<IInstituteTypeService,InstituteTypeService>();
        services.AddScoped<IExperimentService,ExperimentService>();
        services.AddScoped<IExperimentTypeService,ExperimentTypeService>();
        services.AddScoped<IExperimentTaskService, ExperimentTaskService>();
        services.AddScoped<IExperimentTaskTypeService, ExperimentTaskTypeService>();
        services.AddScoped<IExperimentEquipmentService, ExperimentEquipmentService>();

        
        return services;
    }
}