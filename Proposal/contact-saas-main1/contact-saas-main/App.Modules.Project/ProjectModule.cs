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
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ProjectDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectTypeRepository, ProjectTypeRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
        services.AddScoped<IDocumentResultRepository, DocumentResultRepository>();
        services.AddScoped<IResultRepository, ResultRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<DocumentService>();
        services.AddScoped<DocumentTypeService>();
        services.AddScoped<ProjectService>();
        services.AddScoped<ScheduleService>();
        services.AddScoped<ProjectTypeService>();
        services.AddScoped<ResultService>();
        services.AddScoped<DocumentResultService>();
        
        return services;
    }
}