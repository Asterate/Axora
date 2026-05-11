using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Institute.Infrastructure.Configurations;

internal sealed class InstituteConfiguration : IEntityTypeConfiguration<Domain.Entities.Institute> 
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Institute> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}