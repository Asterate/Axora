using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Project.Infrastructure.Configurations;

internal sealed class ExperimentConfiguration : IEntityTypeConfiguration<Project.Domain.Experiment>
{
    public void Configure(EntityTypeBuilder<Project.Domain.Experiment> builder)
    {
        builder.Property(x => x.ExperimentName).HasMaxLength(128).IsRequired();
        builder.Property(x => x.ExperimentNotes).HasMaxLength(512).IsRequired();
        builder.HasOne(x => x.ExperimentType)
            .WithMany()
            .HasForeignKey(x => x.ExperimentTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(x => x.ProjectId).IsRequired();
        builder.HasOne(x => x.Projects)
            .WithMany()
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(x => x.InstituteUserId).IsRequired();
        builder.HasMany(x => x.ExperimentTasks)
            .WithOne(x => x.Experiment)
            .HasForeignKey(x => x.ExperimentId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}