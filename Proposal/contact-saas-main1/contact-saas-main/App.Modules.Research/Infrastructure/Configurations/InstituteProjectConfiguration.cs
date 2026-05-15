using App.Modules.Project.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Project.Infrastructure.Configurations;

internal sealed class InstituteProjectConfiguration : IEntityTypeConfiguration<InstituteProject> 
{
    public void Configure(EntityTypeBuilder<InstituteProject> builder)
    {
        builder.HasOne(x => x.Institute)
            .WithMany()
            .HasForeignKey(x => x.InstituteId)
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}