using App.Modules.Lab.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Lab.Infrastructure.Configurations;

internal sealed class LabTypeConfiguration : IEntityTypeConfiguration<LabType> 
{
    public void Configure(EntityTypeBuilder<LabType> builder)
    {
        builder.Property(c => c.Description)
            .HasMaxLength(512);
        builder.Property(c => c.Name)
            .HasMaxLength(100).IsRequired();
    }
}