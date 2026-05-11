using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Lab.Infrastructure.Configurations;

internal sealed class LabTypeConfiguration : IEntityTypeConfiguration<LabType> 
{
    public void Configure(EntityTypeBuilder<LabType> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}