using App.Modules.Project.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Project.Infrastructure.Configurations;

internal sealed class ScheduleConfiguration : IEntityTypeConfiguration<Schedule> 
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.Property(x => x.ScheduleName)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.ScheduleDescription)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.ColorCode)
            .HasMaxLength(32);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_Schedule_TimeRange",
                "\"ScheduleEndTime\" > \"ScheduleStartTime\"");
        });

        builder.HasOne<ExperimentTask>()
            .WithMany()
            .HasForeignKey(x => x.ExperimentId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.ToTable(t => t.HasCheckConstraint("CK_Schedule_EndAfterStart", "\"ScheduleEndTime\" > \"ScheduleStartTime\""));
    }
}