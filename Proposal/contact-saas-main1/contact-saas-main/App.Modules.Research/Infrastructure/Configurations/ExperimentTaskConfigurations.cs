using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Experiment.Infrastructure.Configurations;

internal sealed class ExperimentTaskConfiguration : IEntityTypeConfiguration<ExperimentTask>
{
    public void Configure(EntityTypeBuilder<ExperimentTask> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}