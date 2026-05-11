using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Lab.Infrastructure.Configurations;

internal sealed class InstituteLabConfiguration : IEntityTypeConfiguration<InstituteLab> 
{
    public void Configure(EntityTypeBuilder<InstituteLab> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}