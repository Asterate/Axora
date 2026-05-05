using App.Modules.Equipment.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Equipment.Infrastructure.Configurations;

internal sealed class CertificationTypeConfiguration : IEntityTypeConfiguration<CertificationType>
{
    public void Configure(EntityTypeBuilder<CertificationType> builder)
    {
        builder.Property(c => c.Description)
            .HasMaxLength(512);
    }
}