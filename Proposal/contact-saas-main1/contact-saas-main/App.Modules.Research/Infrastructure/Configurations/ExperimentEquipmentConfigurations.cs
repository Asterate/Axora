using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Experiment.Infrastructure.Configurations;

internal sealed class ExperimentEquipmentConfiguration : IEntityTypeConfiguration<ExperimentEquipment>
{
    public void Configure(EntityTypeBuilder<ExperimentEquipment> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}