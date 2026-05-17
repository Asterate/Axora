using App.Modules.Project.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Project.Infrastructure.Configurations;

internal sealed class ExperimentTaskConfiguration : IEntityTypeConfiguration<ExperimentTask>
{
    public void Configure(EntityTypeBuilder<ExperimentTask> builder)
    {
        builder.Property(x => x.TaskName)
            .IsRequired().HasMaxLength(128); 
        builder.Property(x => x.TaskDescription)
            .HasMaxLength(128);
        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_ExperimentTask_Priority",
                "\"Priority\" IS NULL OR \"Priority\" BETWEEN 0 AND 5");
        });
        builder.HasOne(x => x.Experiment)
            .WithMany()
            .HasForeignKey(x => x.ExperimentId)
            .OnDelete(DeleteBehavior.Restrict); 
        builder.HasOne(x => x.ExperimentTaskType)
            .WithMany()
            .HasForeignKey(x => x.TaskTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}