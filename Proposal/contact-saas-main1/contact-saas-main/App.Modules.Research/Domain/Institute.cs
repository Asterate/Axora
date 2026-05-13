using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using App.Shared.Domain;

namespace App.Domain.Entities;

public class Institute : BaseEntity
{
    [StringLength(128, MinimumLength = 2)]
    public string InstituteName { get; set; } = "{}";

    [StringLength(128, MinimumLength = 2)]
    public string InstituteCountry { get; set; } = default!;

    [StringLength(128, MinimumLength = 5)]
    public string InstituteAddress { get; set; } = "{}";

    [StringLength(128, MinimumLength = 5)]
    public string InstitutePhoneNumber { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Boolean Active { get; set; } = true;

    public Guid InstituteTypeId { get; set; }
    public InstituteType InstituteType { get; set; } = default!;
    public ICollection<InstituteProject> InstituteProjects { get; set; } = new List<InstituteProject>();

    public string? GetInstituteName(string? culture = null)
        => GetFromJson(InstituteName, culture);

    public string? GetInstituteAddress(string? culture = null)
        => GetFromJson(InstituteAddress, culture);

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