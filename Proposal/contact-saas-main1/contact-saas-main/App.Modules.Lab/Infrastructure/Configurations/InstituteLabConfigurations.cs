using App.Modules.Lab.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Lab.Infrastructure.Configurations;

internal sealed class InstituteLabConfiguration : IEntityTypeConfiguration<InstituteLab> 
{
    public void Configure(EntityTypeBuilder<InstituteLab> builder)
    {
        builder.HasIndex(x => new { x.InstituteId, x.LabId })
            .IsUnique();
        builder.HasOne(x => x.Lab)
            .WithMany()
            .HasForeignKey(x => x.LabId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.InstituteId, x.LabId })
            .IsUnique();
    }
}