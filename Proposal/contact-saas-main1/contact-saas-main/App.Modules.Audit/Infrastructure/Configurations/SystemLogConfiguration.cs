using App.Modules.Audit.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Audit.Infrastructure.Configurations;

internal sealed class SystemLogConfiguration : IEntityTypeConfiguration<SystemLog>
{
    public void Configure(EntityTypeBuilder<SystemLog> builder)
    {
        builder.Property(x => x.Id).IsRequired();
        
        builder.Property(x => x.Timestamp).IsRequired();
        
        builder.Property(x => x.Type)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.Message)
            .IsRequired()
            .HasMaxLength(512);
            
        builder.Property(x => x.UserName)
            .HasMaxLength(256);
            
        builder.Property(x => x.StatusCode)
            .HasDefaultValue(0);

        builder.HasIndex(x => x.Timestamp);
        
        builder.HasIndex(x => x.Type);
    }
}