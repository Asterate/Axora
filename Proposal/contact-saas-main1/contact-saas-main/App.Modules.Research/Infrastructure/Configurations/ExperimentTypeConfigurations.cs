using App.Modules.Project.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Project.Infrastructure.Configurations;

internal sealed class ExperimentTypeConfiguration : IEntityTypeConfiguration<ExperimentType>
{
    public void Configure(EntityTypeBuilder<ExperimentType> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(512);
        
    }
}