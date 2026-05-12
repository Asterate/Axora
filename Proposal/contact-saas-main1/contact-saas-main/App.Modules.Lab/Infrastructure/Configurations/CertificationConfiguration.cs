// Infrastructure/Configurations/CertificationConfiguration.cs

using App.Modules.Equipment.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class CertificationConfiguration : IEntityTypeConfiguration<Certification>
{
    public void Configure(EntityTypeBuilder<Certification> builder)
    {
        builder.Property(x => x.InstituteUserId)
            .IsRequired();
            
        builder.HasOne(x => x.CertificationType)
            .WithMany()
            .HasForeignKey(x => x.CertificationTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}