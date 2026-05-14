using App.Domain.Entities;
using App.Domain.Identity;
using App.Modules.Identity.Domain;
using App.Shared.Persistence;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Identity.Infrastructure;

public sealed class IdentityModuleDbContext
    : IdentityDbContext<AppUser, AppRole, Guid>, IDataProtectionKeyContext
{
    public IdentityModuleDbContext(DbContextOptions<IdentityModuleDbContext> options)
        : base(options)
    {
    }

    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = default!;
    public DbSet<InstituteUser> InstituteUsers { get; set; } = default!;
    public DbSet<AppRefreshToken> AppRefreshTokens { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("identity");
        builder.ApplyAppConventions();
        builder.ApplyConfigurationsFromAssembly(typeof(IdentityModuleDbContext).Assembly);
    }
}