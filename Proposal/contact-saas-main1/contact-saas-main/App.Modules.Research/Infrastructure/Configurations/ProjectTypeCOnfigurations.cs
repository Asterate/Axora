using App.Modules.Project.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Project.Infrastructure.Configurations;

internal sealed class ProjectTypeConfiguration : IEntityTypeConfiguration<ProjectType> 
{
    public void Configure(EntityTypeBuilder<ProjectType> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(512);
        
    }
}