using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Equipment.Infrastructure.Repositories;
using App.Modules.Lab.Application.Interfaces;
using App.Modules.Lab.Infrastructure.Repositories;
using App.Modules.Reagent.Application.Interfaces;
using App.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.Modules.Lab.Infrastructure;

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
        
        services.AddScoped<LabService>();
        services.AddScoped<LabTypeService>();
        services.AddScoped<InstituteLabService>();
        services.AddScoped<EquipmentLabService>();
        services.AddScoped<ReagentLabService>();
        services.AddScoped<EquipmentService>();
        services.AddScoped<EquipmentTypeService>();
        services.AddScoped<CertificationService>();
        services.AddScoped<CertificationTypeService>();
        services.AddScoped<EquipmentCertificationTypeService>();
        services.AddScoped<ReagentService>();
        services.AddScoped<ReagentTypeService>();
        
        return services;
    }
}