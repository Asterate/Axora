using App.Modules.Equipment.Application.Interfaces;
using App.Modules.Equipment.Application.Service;
using App.Modules.Equipment.Infrastructure;
using App.Modules.Equipment.Infrastructure.Data;
using App.Modules.Equipment.Infrastructure.Repositories;
using App.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class EquipmentModule
{
    public static IServiceCollection AddEquipmentModule(
        this IServiceCollection services,
        string connectionString)
    {
        // DbContext
        services.AddDbContext<EquipmentDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Repositories
        services.AddScoped<IEquipmentRepository, EquipmentRepository>();
        services.AddScoped<IEquipmentTypeRepository, EquipmentTypeRepository>();
        services.AddScoped<ICertificationRepository, CertificationRepository>();
        services.AddScoped<ICertificationTypeRepository, CertificationTypeRepository>();
        services.AddScoped<IEquipmentCertificationTypeRepository, EquipmentCertificationTypeRepository>();

        // UoW
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddScoped<EquipmentService>();
        services.AddScoped<EquipmentTypeService>();
        services.AddScoped<CertificationService>();
        services.AddScoped<CertificationTypeService>();
        services.AddScoped<EquipmentCertificationTypeService>();

        return services;
    }
}