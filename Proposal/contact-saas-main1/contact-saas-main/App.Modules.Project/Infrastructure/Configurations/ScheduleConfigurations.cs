using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Project.Infrastructure.Configurations;

internal sealed class ScheduleConfiguration : IEntityTypeConfiguration<Schedule> 
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}