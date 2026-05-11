using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Project.Infrastructure;

internal sealed class ProjectDbContext : DbContext
{
    public ProjectDbContext(DbContextOptions<ProjectDbContext> options) 
        : base(options) { }

    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentResult> DocumentResults { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<Domain.Entities.Project> Projects { get; set; }
    public DbSet<ProjectType> ProjectTypes { get; set; }
    public DbSet<Result> Results { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
   
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("project"); // keeps tables organized in DB
        builder.ApplyConfigurationsFromAssembly(typeof(ProjectDbContext).Assembly);
    }
}