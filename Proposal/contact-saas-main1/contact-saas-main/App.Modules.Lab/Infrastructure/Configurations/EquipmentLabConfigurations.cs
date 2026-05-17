using App.Modules.Lab.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Lab.Infrastructure.Configurations;

internal sealed class EquipmentLabConfiguration : IEntityTypeConfiguration<EquipmentLab>
{
    public void Configure(EntityTypeBuilder<EquipmentLab> builder)
    {
        builder.HasIndex(x => new { x.EquipmentId, x.LabId })
            .IsUnique();

        builder.HasOne(x => x.Lab)
            .WithMany()
            .HasForeignKey(x => x.LabId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Equipment)
            .WithMany()
            .HasForeignKey(x => x.EquipmentId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_EquipmentLab_Quantity",
                "\"Quantity\" >= 0");
        });
    }
}