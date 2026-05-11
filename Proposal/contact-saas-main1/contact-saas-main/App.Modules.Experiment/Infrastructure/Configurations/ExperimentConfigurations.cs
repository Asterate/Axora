using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Experiment.Infrastructure.Configurations;

internal sealed class ExperimentConfiguration : IEntityTypeConfiguration<Domain.Entities.Experiment>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Experiment> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}