using App.Modules.Project.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Project.Infrastructure.Configurations;

internal sealed class DocumentConfiguration : IEntityTypeConfiguration<Document> 
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.Property(x => x.DocumentName)
            .IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(1000);
        builder.Property(x => x.FilePath).HasMaxLength(500);
        builder.HasMany(x => x.DocumentResults)
            .WithOne(x => x.Document)
            .HasForeignKey(x => x.DocumentId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(x => x.DocumentTypeId)
            .IsRequired();
    }
}