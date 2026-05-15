using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Lab.Infrastructure.Configurations;

internal sealed class EquipmentConfiguration : IEntityTypeConfiguration<Lab.Domain.Equipment>
{
    public void Configure(EntityTypeBuilder<Lab.Domain.Equipment> builder)
    {
        builder.Property(c => c.EquipmentName)
            .HasMaxLength(128).IsRequired();
        builder.Property(c => c.EquipmentSerialCode)
            .HasMaxLength(128);
        builder.Property(c => c.ManualFilePath)
            .HasMaxLength(128);
        builder.HasOne(c => c.EquipmentType)
            .WithMany()
            .HasForeignKey(c => c.EquipmentTypeId)
            .IsRequired();
        
    }
}