using App.Domain.Entities;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Identity.Infrastructure;

internal sealed class IdentityDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options, DbSet<InstituteUser> instituteUser, DbSet<AppRefreshToken> appRefreshToken)
        : base(options)
    {
        InstituteUser = instituteUser;
        AppRefreshToken = appRefreshToken;
    }

    public DbSet<InstituteUser> InstituteUser { get; set; }
    public DbSet<AppRefreshToken> AppRefreshToken { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("identity");
        builder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }
}