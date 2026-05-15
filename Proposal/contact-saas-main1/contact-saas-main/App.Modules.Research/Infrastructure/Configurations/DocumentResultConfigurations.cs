using App.Modules.Project.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Project.Infrastructure.Configurations;

internal sealed class DocumentResultConfiguration : IEntityTypeConfiguration<DocumentResult> 
{
    public void Configure(EntityTypeBuilder<DocumentResult> builder)
    {
        builder.HasKey(x => new { x.DocumentId, x.ResultId });
        builder.HasOne(x => x.Document)
            .WithMany()
            .HasForeignKey(x => x.DocumentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Result)
            .WithMany()
            .HasForeignKey(x => x.ResultId)
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}