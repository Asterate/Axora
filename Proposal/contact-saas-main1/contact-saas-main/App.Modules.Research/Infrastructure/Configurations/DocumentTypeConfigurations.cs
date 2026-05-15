using App.Modules.Project.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Project.Infrastructure.Configurations;

internal sealed class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType> 
{
    public void Configure(EntityTypeBuilder<DocumentType> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description)
            .HasMaxLength(512);
        
    }
}