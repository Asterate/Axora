using App.Domain.Entities;
using App.Modules.Equipment.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Lab.Infrastructure;

internal sealed class LabDbContext : DbContext
{
    public LabDbContext(DbContextOptions<LabDbContext> options) 
        : base(options) { }

    public DbSet<Domain.Entities.Lab> Labs { get; set; }
    public DbSet<LabType> LabTypes { get; set; }
    public DbSet<ReagentLab> ReagentLabs { get; set; }
    public DbSet<EquipmentLab> EquipmentLabs { get; set; }
    public DbSet<InstituteLab> InstituteLabs { get; set; }
   
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("lab"); // keeps tables organized in DB
        builder.ApplyConfigurationsFromAssembly(typeof(LabDbContext).Assembly);
    }
}