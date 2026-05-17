using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Equipment.Infrastructure.Repositories;
using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Application.Interfaces.Service;
using App.Modules.Lab.Application.Services;
using App.Modules.Lab.Infrastructure;
using App.Modules.Lab.Infrastructure.Repositories;
using App.Modules.Reagent.Application.Interfaces;
using App.Shared.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace App.Modules.Lab;

public static class LabDbModule
{
    public static IServiceCollection AddLabModule(
        this IServiceCollection services)
    {

        services.AddScoped<ILabRepository, LabRepository>();
        services.AddScoped<ILabTypeRepository, LabTypeRepository>();
        services.AddScoped<IEquipmentLabRepository, EquipmentLabRepository>();
        services.AddScoped<IReagentLabRepository, ReagentLabRepository>();
        services.AddScoped<IInstituteLabRepository, InstituteLabRepository>();
        services.AddScoped<IEquipmentRepository, EquipmentRepository>();
        services.AddScoped<IEquipmentTypeRepository, EquipmentTypeRepository>();
        services.AddScoped<ICertificationRepository, CertificationRepository>();
        services.AddScoped<ICertificationTypeRepository, CertificationTypeRepository>();
        services.AddScoped<IEquipmentCertificationTypeRepository, EquipmentCertificationTypeRepository>();
        services.AddScoped<IReagentRepository, ReagentRepository>();
        services.AddScoped<IReagentTypeRepository, ReagentTypeRepository>();


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<ILabService, LabService>();
        services.AddScoped<ILabTypeService, LabTypeService>();
        services.AddScoped<IInstituteLabService, InstituteLabService>();
        services.AddScoped<IEquipmentLabService, EquipmentLabService>();
        services.AddScoped<IReagentLabService, ReagentLabService>();
        services.AddScoped<IEquipmentService, EquipmentService>();
        services.AddScoped<IEquipmentTypeService, EquipmentTypeService>();
        services.AddScoped<ICertificationService, CertificationService>();
        services.AddScoped<ICertificationTypeService, CertificationTypeService>();
        services.AddScoped<IEquipmentCertificationService, EquipmentCertificationService>();
        services.AddScoped<IReagentService, ReagentService>();
        services.AddScoped<IReagentTypeService, ReagentTypeService>();
        
        return services;
    }
}