using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Project.Infrastructure.Configurations;

internal sealed class ProjectConfiguration : IEntityTypeConfiguration<Domain.Entities.Project> 
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Project> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}