using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Institute.Infrastructure.Configurations;

internal sealed class InstituteTypeConfiguration : IEntityTypeConfiguration<InstituteType> 
{
    public void Configure(EntityTypeBuilder<InstituteType> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}