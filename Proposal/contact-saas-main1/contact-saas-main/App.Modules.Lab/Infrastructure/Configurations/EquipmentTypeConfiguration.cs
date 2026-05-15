using App.Modules.Lab.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Lab.Infrastructure.Configurations;

internal sealed class EquipmentTypeConfiguration : IEntityTypeConfiguration<EquipmentType>
{
    public void Configure(EntityTypeBuilder<EquipmentType> builder)
    {
        builder.Property(c => c.Description)
            .HasMaxLength(512);
        builder.Property(c => c.Name)
            .HasMaxLength(100).IsRequired();
    }
}