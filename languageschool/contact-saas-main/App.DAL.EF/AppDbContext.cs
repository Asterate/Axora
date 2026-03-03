using App.Domain.Entities;
using App.Domain.Identity;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.DAL.EF;


public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=webapp2526s;Username=postgres;Password=postgres");

        return new AppDbContext(optionsBuilder.Options);
    }
}
public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser, AppRole, Guid>(options), IDataProtectionKeyContext
{

    public DbSet<AppRefreshToken> RefreshTokens { get; set; }
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    
    public DbSet<AttendanceRecord>  AttendanceRecords { get; set; }
    public DbSet<Availability>  Availabilities { get; set; }
    public DbSet<Certificate>  Certificates { get; set; }
    public DbSet<Company>  Companies { get; set; }
    public DbSet<CompanyConfig>  CompanyConfigs { get; set; }
    public DbSet<CompanyUser>  CompanyUsers { get; set; }
    public DbSet<Consultation>  Consultations { get; set; }
    public DbSet<Course>  Courses { get; set; }
    public DbSet<Enrollment>  Enrollments { get; set; }
    public DbSet<Language>  Languages { get; set; }
    public DbSet<Level>  Levels { get; set; }
    public DbSet<Material>  Materials { get; set; }
    public DbSet<MaterialDistribution>  MaterialDistributions { get; set; }
    public DbSet<PlacementTest>  PlacementTests { get; set; }
    public DbSet<Schedule>  Schedules { get; set; }
    public DbSet<Session>  Sessions { get; set; }
    public DbSet<Student>  Students { get; set; }
    public DbSet<Subs>  Subs { get; set; }
    public DbSet<Teacher>  Teachers { get; set; }
    public DbSet<TeacherCertificate>  TeacherCertificates { get; set; }
    public DbSet<StudentPlacementTest>  StudentPlacementTests { get; set; }
    public DbSet<TeacherLanguage>  TeacherLanguages { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure all DateTime properties to use UTC
        ConfigureDateTimeAsUtc(builder);

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

}