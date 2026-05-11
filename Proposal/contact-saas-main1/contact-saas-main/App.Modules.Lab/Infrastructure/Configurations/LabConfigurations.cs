using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Lab.Infrastructure.Configurations;

internal sealed class LabConfiguration : IEntityTypeConfiguration<Domain.Entities.Lab> 
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Lab> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}