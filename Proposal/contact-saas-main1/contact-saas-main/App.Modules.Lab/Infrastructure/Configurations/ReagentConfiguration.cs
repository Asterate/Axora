using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Reagent.Infrastructure.Configurations;

internal sealed class ReagentConfiguration : IEntityTypeConfiguration<ReagentType> 
{
    public void Configure(EntityTypeBuilder<ReagentType> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
    }
}