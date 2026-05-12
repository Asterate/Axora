using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Institute.Infrastructure.Configurations;

internal sealed class InstituteProjectConfiguration : IEntityTypeConfiguration<InstituteProject> 
{
    public void Configure(EntityTypeBuilder<InstituteProject> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}