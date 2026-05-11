using App.Modules.Equipment.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Lab.Infrastructure.Configurations;

internal sealed class EquipmentLabConfiguration : IEntityTypeConfiguration<EquipmentLab> 
{
    public void Configure(EntityTypeBuilder<EquipmentLab> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}