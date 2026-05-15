using App.Modules.Lab.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Lab.Infrastructure.Configurations;

internal sealed class CertificationTypeConfiguration : IEntityTypeConfiguration<CertificationType>
{
    public void Configure(EntityTypeBuilder<CertificationType> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Description)
            .HasMaxLength(512);
    }
}