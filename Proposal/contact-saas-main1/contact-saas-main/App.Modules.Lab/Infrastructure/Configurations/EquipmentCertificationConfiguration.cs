using App.Modules.Lab.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Lab.Infrastructure.Configurations;

public class EquipmentCertificationConfiguration :  IEntityTypeConfiguration<EquipmentCertification>
{
    public void Configure(EntityTypeBuilder<EquipmentCertification> builder)
    {
        builder.HasKey(x => new { x.EquipmentId, x.CertificationTypeId });
        builder.HasOne(x => x.CertificationType)
            .WithMany()
            .HasForeignKey(x => x.CertificationTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Equipment)
            .WithMany()
            .HasForeignKey(x => x.EquipmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}