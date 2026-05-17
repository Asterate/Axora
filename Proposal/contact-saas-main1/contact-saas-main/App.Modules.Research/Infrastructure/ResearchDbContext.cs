using App.Domain.Entities;
using App.Modules.Project.Domain;
using App.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Project.Infrastructure;

public sealed class ResearchDbContext : DbContext
{
    public ResearchDbContext(DbContextOptions<ResearchDbContext> options) 
        : base(options) { }

    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentResult> DocumentResults { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<Domain.Project> Projects { get; set; }
    public DbSet<ProjectType> ProjectTypes { get; set; }
    public DbSet<Result> Results { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    
    public DbSet<Domain.Institute> Institutes { get; set; }
    public DbSet<InstituteProject> InstituteProjects { get; set; }
    public DbSet<InstituteType> InstituteTypes { get; set; }
    
    public DbSet<Domain.Experiment> Experiments { get; set; }
    public DbSet<ExperimentType> ExperimentTypes { get; set; }
    public DbSet<ExperimentEquipment> ExperimentEquipments { get; set; }
    public DbSet<ExperimentTask> ExperimentTasks { get; set; }
    public DbSet<ExperimentTaskType> ExperimentTaskTypes { get; set; }
   
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("project"); // keeps tables organized in DB
        builder.ApplyAppConventions();
        builder.ApplyConfigurationsFromAssembly(typeof(ResearchDbContext).Assembly);
        builder.ConfigureLangStrAsJson();
    }
}