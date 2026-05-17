using App.Modules.Lab.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Lab.Infrastructure.Configurations;

internal sealed class ReagentConfigurationType : IEntityTypeConfiguration<ReagentType> 
{
    public void Configure(EntityTypeBuilder<ReagentType> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(512);
        builder.Property(x => x.Category).HasMaxLength(64);
        builder.Property(x => x.HazardLevel).HasMaxLength(64);
        builder.Property(x => x.StandardConcentration).HasMaxLength(64);
        builder.Property(x => x.MaterialFilePath).HasMaxLength(64);
        builder.Property(x => x.ColorCode).HasMaxLength(64);
        builder.Property(x => x.IsHazardous)
            .IsRequired();
        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_ReagentType_DefaultStorage",
                "\"DefaultStorage\" > 0");
        });
    }   
}