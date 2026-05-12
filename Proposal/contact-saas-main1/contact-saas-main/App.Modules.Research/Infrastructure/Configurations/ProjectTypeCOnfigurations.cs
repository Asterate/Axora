using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Project.Infrastructure.Configurations;

internal sealed class ProjectTypeConfiguration : IEntityTypeConfiguration<ProjectType> 
{
    public void Configure(EntityTypeBuilder<ProjectType> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}