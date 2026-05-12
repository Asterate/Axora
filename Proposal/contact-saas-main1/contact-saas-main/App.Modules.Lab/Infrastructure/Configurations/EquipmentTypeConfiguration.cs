using App.Modules.Equipment.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Equipment.Infrastructure.Configurations;

internal sealed class EquipmentTypeConfiguration : IEntityTypeConfiguration<EquipmentType>
{
    public void Configure(EntityTypeBuilder<EquipmentType> builder)
    {
        builder.Property(c => c.Description)
            .HasMaxLength(512);
    }
}