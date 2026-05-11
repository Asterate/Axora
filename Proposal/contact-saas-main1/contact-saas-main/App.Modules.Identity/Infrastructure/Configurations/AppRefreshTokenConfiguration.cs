using App.Domain.Entities;
using App.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Identity.Infrastructure.Configurations;

internal sealed class AppRefreshTokenConfiguration : IEntityTypeConfiguration<AppRefreshToken> 
{
    public void Configure(EntityTypeBuilder<AppRefreshToken> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}