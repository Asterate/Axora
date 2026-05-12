using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Project.Infrastructure.Configurations;

internal sealed class DocumentConfiguration : IEntityTypeConfiguration<Document> 
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();
        
    }
}