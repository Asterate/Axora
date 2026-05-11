using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Experiment.Infrastructure;
internal sealed class ExperimentDbContext : DbContext
{
    public ExperimentDbContext(DbContextOptions<ExperimentDbContext> options) 
        : base(options) { }

    public DbSet<Domain.Entities.Experiment> Experiments { get; set; }
    public DbSet<ExperimentType> ExperimentTypes { get; set; }
    public DbSet<ExperimentEquipment> ExperimentEquipments { get; set; }
    public DbSet<ExperimentTask> ExperimentTasks { get; set; }
    public DbSet<ExperimentTaskType> ExperimentTaskTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("experiment"); // keeps tables organized in DB
        builder.ApplyConfigurationsFromAssembly(typeof(ExperimentDbContext).Assembly);
    }
}