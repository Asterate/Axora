using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Project.Infrastructure.Configurations;

internal sealed class InstituteConfiguration : IEntityTypeConfiguration<Project.Domain.Institute> 
{
    public void Configure(EntityTypeBuilder<Project.Domain.Institute> builder)
    {
        builder.Property(x => x.InstituteName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.InstituteCountry).IsRequired().HasMaxLength(100);
        builder.Property(x => x.InstitutePhoneNumber).IsRequired().HasMaxLength(100);
        builder.Property(x => x.InstituteAddress).IsRequired().HasMaxLength(250);
        builder.HasOne(x => x.InstituteType)
            .WithMany()
            .HasForeignKey(x => x.InstituteTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}