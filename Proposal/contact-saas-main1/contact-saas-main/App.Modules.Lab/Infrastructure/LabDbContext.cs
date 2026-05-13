using App.Domain.Entities;
using App.Modules.Equipment.Domain;
using App.Modules.Lab.Domain;
using App.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Lab.Infrastructure;

public sealed class LabDbContext : DbContext
{
    public LabDbContext(DbContextOptions<LabDbContext> options) 
        : base(options) { }

    public DbSet<App.Domain.Entities.Lab> Labs { get; set; }
    public DbSet<LabType> LabTypes { get; set; }
    public DbSet<ReagentLab> ReagentLabs { get; set; }
    public DbSet<EquipmentLab> EquipmentLabs { get; set; }
    public DbSet<InstituteLab> InstituteLabs { get; set; }
    public DbSet<Domain.Equipment> Equipments { get; set; }
    public DbSet<EquipmentType> EquipmentTypes { get; set; }
    public DbSet<Certification> Certifications { get; set; }
    public DbSet<CertificationType> CertificationTypes { get; set; }
    public DbSet<EquipmentCertificationType> EquipmentCertificationTypes { get; set; }
    public DbSet<App.Domain.Entities.Reagent> Reagents { get; set; }
    public DbSet<ReagentType> ReagentTypes { get; set; }
   
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("lab"); // keeps tables organized in DB
        builder.ApplyAppConventions();
        builder.ApplyConfigurationsFromAssembly(typeof(LabDbContext).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(Domain.Equipment).Assembly);
    }
}