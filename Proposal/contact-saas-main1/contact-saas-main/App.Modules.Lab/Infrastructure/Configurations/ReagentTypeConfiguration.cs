using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Reagent.Infrastructure.Configurations;

internal sealed class ReagentConfigurationType : IEntityTypeConfiguration<Domain.Entities.Reagent> 
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Reagent> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
    }   
}