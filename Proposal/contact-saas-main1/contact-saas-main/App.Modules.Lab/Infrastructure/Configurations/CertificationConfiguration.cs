using App.Modules.Lab.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Lab.Infrastructure.Configurations;

internal sealed class CertificationConfiguration : IEntityTypeConfiguration<Certification>
{
    public void Configure(EntityTypeBuilder<Certification> builder)
    {
        builder.Property(x => x.InstituteUserId)
            .IsRequired();
        builder.Property(x => x.CertificationName).HasMaxLength(256);
        builder.Property(x => x.CertificationTypeId).IsRequired();
        builder.Property(x => x.InstituteUserId).IsRequired();
            
        builder.HasOne(x => x.CertificationType)
            .WithMany()
            .HasForeignKey(x => x.CertificationTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_Certification_Dates",
                "\"Expired\" IS NULL OR \"HandedOver\" < \"Expired\"");
        });
    }
}