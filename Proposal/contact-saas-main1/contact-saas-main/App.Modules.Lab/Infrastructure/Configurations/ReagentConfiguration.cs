using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Lab.Infrastructure.Configurations;

internal sealed class ReagentConfiguration : IEntityTypeConfiguration<Domain.Reagent> 
{
    public void Configure(EntityTypeBuilder<Domain.Reagent> builder)
    {
        builder.Property(x => x.ReagentName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.ReagentDescription).IsRequired().HasMaxLength(512);
        builder.Property(x => x.CasNumber).HasMaxLength(10);
        builder.Property(x => x.ChemicalFormula).HasMaxLength(512);
        builder.Property(x => x.Concentration)
            .HasMaxLength(128);
        builder.Property(x => x.StorageConditions)
            .HasMaxLength(512);
        builder.Property(x => x.SafetyNotes)
            .HasMaxLength(512);
        builder.Property(x => x.MaterialFilePath).HasMaxLength(512);
    }
}