using System.Text.Json;
using App.Modules.Lab.Domain;
using App.Shared.Domain;
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
        builder.HasDefaultSchema("lab"); // keeps tables organized in DB
        builder.ApplyAppConventions();
        builder.ApplyConfigurationsFromAssembly(typeof(LabDbContext).Assembly);
        builder.Entity<Domain.Equipment>()
            .Property(e => e.EquipmentName)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => string.IsNullOrWhiteSpace(v) ? new LangStr() : JsonSerializer.Deserialize<LangStr>(v, (JsonSerializerOptions?)null) ?? new LangStr());
        
        builder.Entity<EquipmentType>()
            .Property(e => e.Description)
            .HasConversion(
                v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => string.IsNullOrWhiteSpace(v) ? null : JsonSerializer.Deserialize<LangStr>(v, (JsonSerializerOptions?)null));
    }
}