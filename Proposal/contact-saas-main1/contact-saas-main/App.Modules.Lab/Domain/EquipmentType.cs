using System.Text.Json;
using App.Shared.Domain;

namespace App.Modules.Equipment.Domain;

public class EquipmentType : BaseEntity
{
    public string Name { get; set; } = "{}";

    public string? Description { get; set; }

    public DateTime? DeletedAt { get; set; }

    // Helper to read a translation by key from the JSON stored in Name
    public string? GetName(string? culture = null)
        => GetFromJson(Name, culture);

    public string? GetDescription(string? culture = null)
        => GetFromJson(Description, culture);

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