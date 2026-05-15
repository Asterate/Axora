using App.Modules.Project.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Project.Infrastructure.Configurations;

internal sealed class ResultConfiguration : IEntityTypeConfiguration<Result> 
{
    public void Configure(EntityTypeBuilder<Result> builder)
    {
        builder.Property(x => x.ResultName)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.ResultDescription)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.MeasurementName)
            .HasMaxLength(128);

        builder.Property(x => x.MeasurementValue)
            .HasMaxLength(128);

        builder.Property(x => x.Unit)
            .HasMaxLength(64);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.Property(x => x.FilePath)
            .HasMaxLength(500);

        builder.HasMany(x => x.DocumentResults)
            .WithOne(x => x.Result)
            .HasForeignKey(x => x.ResultId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Domain.Experiment>()
            .WithMany()
            .HasForeignKey(x => x.ExperimentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ExperimentTask>()
            .WithMany()
            .HasForeignKey(x => x.ExperimentTaskId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}