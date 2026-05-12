using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Project.Infrastructure.Configurations;

internal sealed class DocumentResultConfiguration : IEntityTypeConfiguration<DocumentResult> 
{
    public void Configure(EntityTypeBuilder<DocumentResult> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}