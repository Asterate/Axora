using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Institute.Infrastructure;

internal sealed class InstituteDbContext : DbContext
{
    public InstituteDbContext(DbContextOptions<InstituteDbContext> options) 
        : base(options) { }

    public DbSet<Domain.Entities.Institute> Institutes { get; set; }
    public DbSet<InstituteProject> InstituteProjects { get; set; }
    public DbSet<InstituteType> InstituteTypes { get; set; }
   
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("institute"); // keeps tables organized in DB
        builder.ApplyConfigurationsFromAssembly(typeof(InstituteDbContext).Assembly);
    }
}