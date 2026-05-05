using App.Modules.Equipment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class EquipmentModule
{
    public static IServiceCollection AddEquipmentModule(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<EquipmentDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}