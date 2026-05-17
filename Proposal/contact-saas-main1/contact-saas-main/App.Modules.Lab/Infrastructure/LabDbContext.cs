using App.Modules.Lab.Domain;
using App.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Lab.Infrastructure;

public sealed class LabDbContext : DbContext
{
    public LabDbContext(DbContextOptions<LabDbContext> options) 
        : base(options) { }

    public DbSet<Domain.Lab> Labs { get; set; }
    public DbSet<LabType> LabTypes { get; set; }
    public DbSet<ReagentLab> ReagentLabs { get; set; }
    public DbSet<EquipmentLab> EquipmentLabs { get; set; }
    public DbSet<InstituteLab> InstituteLabs { get; set; }
    public DbSet<Domain.Equipment> Equipments { get; set; }
    public DbSet<EquipmentType> EquipmentTypes { get; set; }
    public DbSet<Certification> Certifications { get; set; }
    public DbSet<CertificationType> CertificationTypes { get; set; }
    public DbSet<EquipmentCertification> EquipmentCertificationTypes { get; set; }
    public DbSet<Domain.Reagent> Reagents { get; set; }
    public DbSet<ReagentType> ReagentTypes { get; set; }
   
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("lab");
        builder.ApplyAppConventions();
        builder.ApplyConfigurationsFromAssembly(typeof(LabDbContext).Assembly);
        builder.ConfigureLangStrAsJson();
    }
}