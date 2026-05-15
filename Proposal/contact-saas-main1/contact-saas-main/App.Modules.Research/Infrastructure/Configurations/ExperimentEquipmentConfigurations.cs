using App.Modules.Project.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Project.Infrastructure.Configurations;

    internal sealed class ExperimentEquipmentConfiguration : IEntityTypeConfiguration<ExperimentEquipment>
    {
        public void Configure(EntityTypeBuilder<ExperimentEquipment> builder)
        {
            builder.HasOne(x => x.Experiment)
                .WithMany()
                .HasForeignKey(x => x.ExperimentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }