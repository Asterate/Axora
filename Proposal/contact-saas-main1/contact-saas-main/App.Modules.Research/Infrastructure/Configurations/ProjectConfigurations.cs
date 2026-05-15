using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Project.Infrastructure.Configurations;

internal sealed class ProjectConfiguration : IEntityTypeConfiguration<Domain.Project> 
{
    public void Configure(EntityTypeBuilder<Domain.Project> builder)
    {
        builder.Property(x => x.ProjectName)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.Requirements)
            .HasMaxLength(4000);

        builder.Property(x => x.RequirementsFilePath)
            .HasMaxLength(500);

        builder.HasOne(x => x.ProjectType)
            .WithMany()
            .HasForeignKey(x => x.ProjectTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_Project_Funding",
                "[Funding] IS NULL OR [Funding] >= 0");
        });
        
    }
}