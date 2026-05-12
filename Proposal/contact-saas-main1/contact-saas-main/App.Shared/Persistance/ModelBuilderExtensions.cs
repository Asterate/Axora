using System.Text.Json;
using App.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.Shared.Persistence;

public static class ModelBuilderExtensions
{
    public static void ApplyAppConventions(this ModelBuilder builder)
    {
        builder.ConfigureUtcDateTimes();
        builder.ConfigureLangStrAsJson();
        builder.RestrictCascadeDeletes();
    }

    public static void ConfigureUtcDateTimes(this ModelBuilder builder)
    {
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(v, DateTimeKind.Utc)
                : v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

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

    public static void ConfigureLangStrAsJson(this ModelBuilder builder)
    {
        var langStrConverter = new ValueConverter<LangStr, string>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
            v => string.IsNullOrWhiteSpace(v)
                ? new LangStr()
                : JsonSerializer.Deserialize<LangStr>(v, (JsonSerializerOptions?)null) ?? new LangStr());

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

    public static void RestrictCascadeDeletes(this ModelBuilder builder)
    {
        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

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