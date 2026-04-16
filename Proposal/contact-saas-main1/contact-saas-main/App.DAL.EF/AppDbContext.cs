using App.Domain.Entities;
using App.Domain.Identity;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using App.Domain;

namespace App.DAL.EF;
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Replace with your PostgreSQL connection string
        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=webapp2526s;Username=postgres;Password=postgres");

        return new AppDbContext(optionsBuilder.Options);
    }
}
public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser, AppRole, Guid>(options), IDataProtectionKeyContext
{

    public DbSet<AppRefreshToken> RefreshTokens { get; set; }
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    
    public DbSet<InstituteUser> InstituteUsers { get; set; }
    public DbSet<Institute> Institutes { get; set; }
    public DbSet<InstituteLab> InstituteLabs { get; set; }
    public DbSet<InstituteProject> InstituteProjects { get; set; }
    public DbSet<InstituteType> InstituteTypes { get; set; }
    public DbSet<Experiment> Experiments { get; set; }
    public DbSet<ExperimentEquipment> ExperimentEquipments { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ExperimentTask> ExperimentTasks { get; set; }
    public DbSet<Result> Results { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentResult> DocumentResults { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<EquipmentType> EquipmentTypes { get; set; }
    public DbSet<EquipmentCertificationType> EquipmentCertificationTypes { get; set; }
    public DbSet<Certification> Certifications { get; set; }
    public DbSet<CertificationType> CertificationTypes { get; set; }
    public DbSet<Reagent> Reagents { get; set; }
    public DbSet<ReagentType> ReagentTypes { get; set; }
    public DbSet<ReagentLab> ReagentLabs { get; set; }
    public DbSet<ExperimentType> ExperimentTypes { get; set; }
    public DbSet<TaskType> TaskTypes { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<ProjectType> ProjectTypes { get; set; }
    public DbSet<EquipmentLab> EquipmentLabs { get; set; }
    public DbSet<Lab> Labs { get; set; }
    public DbSet<LabType> LabTypes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure all DateTime properties to use UTC
        ConfigureDateTimeAsUtc(builder);

        // Configure LangStr properties to use JSON conversion
        ConfigureLangStrAsJson(builder);

        // disable cascade delete
        foreach (var relationship in builder.Model
                     .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
    
    /// <summary>
    /// Configures all DateTime and DateTime? properties to convert to UTC when saving to PostgreSQL.
    /// PostgreSQL's 'timestamp with time zone' type requires UTC values.
    /// </summary>
    private static void ConfigureDateTimeAsUtc(ModelBuilder builder)
    {
        // Value converter for DateTime
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(v, DateTimeKind.Utc)
                : v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        // Value converter for DateTime?
        var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
            v => v.HasValue
                ? (v.Value.Kind == DateTimeKind.Unspecified
                    ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc)
                    : v.Value.ToUniversalTime())
                : v,
            v => v.HasValue
                ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc)
                : v);

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(dateTimeConverter);
                }
                else if (property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(nullableDateTimeConverter);
                }
            }
        }
    }

    /// <summary>
    /// Configures all LangStr properties to convert to/from JSON for database storage.
    /// LangStr is serialized as a JSON object with culture keys (e.g., {"en": "Name", "et": "Nimi"}).
    /// </summary>
    private static void ConfigureLangStrAsJson(ModelBuilder builder)
    {
        var langStrConverter = new ValueConverter<LangStr, string>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
            v => string.IsNullOrEmpty(v) 
                ? new LangStr() 
                : JsonSerializer.Deserialize<LangStr>(v, (JsonSerializerOptions?)null) ?? new LangStr());

        // ValueComparer for LangStr (Dictionary-based type) to properly compare collection elements
        var langStrComparer = new ValueComparer<LangStr>(
            (left, right) => LangStrComparerEquals(left, right),
            obj => LangStrComparerHash(obj),
            obj => LangStrComparerClone(obj));

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(LangStr))
                {
                    property.SetValueConverter(langStrConverter);
                    property.SetValueComparer(langStrComparer);
                }
            }
        }
    }

    // Helper methods for LangStr ValueComparer
    private static bool LangStrComparerEquals(LangStr? left, LangStr? right)
    {
        if (left == null && right == null) return true;
        if (left == null || right == null) return false;
        return left.SequenceEqual(right);
    }

    private static int LangStrComparerHash(LangStr obj)
    {
        var hash = 0;
        foreach (var item in obj)
        {
            hash = HashCode.Combine(hash, item.Key.GetHashCode(), item.Value.GetHashCode());
        }
        return hash;
    }

    private static LangStr LangStrComparerClone(LangStr obj)
    {
        var copy = new LangStr();
        foreach (var item in obj)
        {
            copy[item.Key] = item.Value;
        }
        return copy;
    }

}