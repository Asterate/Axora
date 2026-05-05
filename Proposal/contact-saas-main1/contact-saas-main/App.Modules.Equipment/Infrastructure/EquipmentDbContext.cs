// App.Modules.Equipment/Infrastructure/EquipmentDbContext.cs

using App.Modules.Equipment.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Equipment.Infrastructure.Data;
internal sealed class EquipmentDbContext : DbContext
{
    public EquipmentDbContext(DbContextOptions<EquipmentDbContext> options) 
        : base(options) { }

    public DbSet<Domain.Equipment> Equipments { get; set; }
    public DbSet<EquipmentType> EquipmentTypes { get; set; }
    public DbSet<Certification> Certifications { get; set; }
    public DbSet<CertificationType> CertificationTypes { get; set; }
    public DbSet<EquipmentCertificationType> EquipmentCertificationTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("equipment"); // keeps tables organized in DB
        builder.ApplyConfigurationsFromAssembly(typeof(EquipmentDbContext).Assembly);
    }
}