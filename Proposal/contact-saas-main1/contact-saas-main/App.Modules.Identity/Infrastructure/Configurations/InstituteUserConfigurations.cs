using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Identity.Infrastructure.Configurations;

internal sealed class InstituteUserConfigurations : IEntityTypeConfiguration<InstituteUser> 
{
    public void Configure(EntityTypeBuilder<InstituteUser> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}