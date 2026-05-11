using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Reagent.Infrastructure;

internal sealed class ReagentDbContext : DbContext
{
    public ReagentDbContext(DbContextOptions<ReagentDbContext> options) 
        : base(options) { }

    public DbSet<Domain.Entities.Reagent> Reagents { get; set; }
    public DbSet<ReagentType> ReagentTypes { get; set; }
    
   
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("reagent"); // keeps tables organized in DB
        builder.ApplyConfigurationsFromAssembly(typeof(ReagentDbContext).Assembly);
    }
}