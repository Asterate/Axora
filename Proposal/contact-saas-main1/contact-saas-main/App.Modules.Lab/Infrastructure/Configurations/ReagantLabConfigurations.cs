using App.Modules.Lab.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Lab.Infrastructure.Configurations;

internal sealed class ReagantLabConfiguration : IEntityTypeConfiguration<ReagentLab> 
{
    public void Configure(EntityTypeBuilder<ReagentLab> builder)
    {
        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_ReagentLab_Quantity",
                "\"Quantity\" > 0");
        });
        builder.Property(l => l.Unit).IsRequired().HasMaxLength(10);
        builder.HasOne(x => x.Reagent)
            .WithMany()
            .HasForeignKey(x => x.ReagentId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => new { x.ReagentId, x.LabId })
            .IsUnique();
    }
}