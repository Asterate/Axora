using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Experiment.Infrastructure.Configurations;

internal sealed class ExperimentTaskTypeConfiguration : IEntityTypeConfiguration<ExperimentTaskType>
{
    public void Configure(EntityTypeBuilder<ExperimentTaskType> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}