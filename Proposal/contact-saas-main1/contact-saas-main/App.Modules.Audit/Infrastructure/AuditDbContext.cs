using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Equipment.Infrastructure.Data;
internal sealed class AuditDbContext : DbContext
{
    public AuditDbContext(DbContextOptions<AuditDbContext> options) 
        : base(options) { }

    public DbSet<SystemLog> SystemLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("audit"); // keeps tables organized in DB
        builder.ApplyConfigurationsFromAssembly(typeof(AuditDbContext).Assembly);
    }
}