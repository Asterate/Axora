using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Lab.Infrastructure.Configurations;

internal sealed class LabConfiguration : IEntityTypeConfiguration<Domain.Lab> 
{
    public void Configure(EntityTypeBuilder<Domain.Lab> builder)
    {
        builder.Property(l => l.LabName).IsRequired().HasMaxLength(100);
        builder.Property(l => l.LabAddress).IsRequired().HasMaxLength(200);
        builder.HasOne(x => x.LabType)
            .WithMany()
            .HasForeignKey(x => x.LabType)
            .OnDelete(DeleteBehavior.Restrict);
        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_Lab_LabCapacity",
                "[LabCapacity] > 0");
        });
        
    }
}