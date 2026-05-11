using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Experiment.Infrastructure.Configurations;

internal sealed class ExperimentTypeConfiguration : IEntityTypeConfiguration<ExperimentType>
{
    public void Configure(EntityTypeBuilder<ExperimentType> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}