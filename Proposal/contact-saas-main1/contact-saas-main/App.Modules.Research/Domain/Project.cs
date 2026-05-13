using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using App.Shared.Domain;

namespace App.Domain.Entities;

public class Project : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public string ProjectName { get; set; } = "{}";

    public float? Funding { get; set; }

    public string? Requirements { get; set; }

    public string? RequirementsFilePath { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public Guid ProjectTypeId { get; set; }
    public ProjectType ProjectType { get; set; } = default!;

    public string? GetProjectName(string? culture = null)
        => GetFromJson(ProjectName, culture);

    public string? GetRequirements(string? culture = null)
        => GetFromJson(Requirements, culture);

    private static string? GetFromJson(string? json, string? culture)
    {
        if (string.IsNullOrWhiteSpace(json)) return null;
        culture = culture?.Trim() ?? Thread.CurrentThread.CurrentUICulture.Name;
        var neutral = culture.Split('-')[0];
        try
        {
            var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            if (dict == null) return null;
            if (dict.TryGetValue(culture, out var val)) return val;
            if (dict.TryGetValue(neutral, out val)) return val;
            if (dict.TryGetValue("en", out val)) return val;
            return dict.Values.FirstOrDefault();
        }
        catch
        {
            return null;
        }
    }
}