using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Lab.Infrastructure.Configurations;

internal sealed class ReagantLabConfiguration : IEntityTypeConfiguration<ReagentLab> 
{
    public void Configure(EntityTypeBuilder<ReagentLab> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}