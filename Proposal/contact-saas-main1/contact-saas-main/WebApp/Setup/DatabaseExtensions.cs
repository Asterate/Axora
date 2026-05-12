using App.DAL.EF;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace WebApp.Setup;

public static class DatabaseExtensions
{
    public static IServiceCollection AddModuleDatabase<TContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
        where TContext : DbContext
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string not found.");

        services.AddDbContext<TContext>(options =>
        {
            options.UseNpgsql(connectionString);

            if (!environment.IsProduction())
            {
                options.EnableDetailedErrors()
                    .EnableSensitiveDataLogging();
            }
        });

        return services;
    }
}