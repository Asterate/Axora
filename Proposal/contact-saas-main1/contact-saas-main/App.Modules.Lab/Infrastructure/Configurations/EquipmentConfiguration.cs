using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Equipment.Infrastructure.Configurations;

internal sealed class EquipmentConfiguration : IEntityTypeConfiguration<Lab.Domain.Equipment>
{
    public void Configure(EntityTypeBuilder<Lab.Domain.Equipment> builder)
    {
        builder.Property(c => c.EquipmentName)
            .HasMaxLength(128);
        
        builder.Property(c => c.EquipmentSerialCode)
            .HasMaxLength(128);
    }
}