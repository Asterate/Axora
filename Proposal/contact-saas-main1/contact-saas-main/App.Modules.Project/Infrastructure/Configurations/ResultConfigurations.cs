using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Project.Infrastructure.Configurations;

internal sealed class ResultConfiguration : IEntityTypeConfiguration<Result> 
{
    public void Configure(EntityTypeBuilder<Result> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}