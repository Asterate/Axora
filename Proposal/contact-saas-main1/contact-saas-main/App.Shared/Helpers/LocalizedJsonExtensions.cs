using System.Text.Json;

namespace App.Shared.Helpers;

public static class LocalizedJsonExtensions
{
    public static string? GetLocalizedValue(this string? json, string? culture = null)
    {
        if (string.IsNullOrWhiteSpace(json))
            return null;

        culture = culture?.Trim() ?? Thread.CurrentThread.CurrentUICulture.Name;
        var neutral = culture.Split('-')[0];

        try
        {
            var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            if (dict is null || dict.Count == 0)
                return null;

            if (dict.TryGetValue(culture, out var value))
                return value;

            if (dict.TryGetValue(neutral, out value))
                return value;

            if (dict.TryGetValue("en", out value))
                return value;

            return dict.Values.FirstOrDefault();
        }
        catch (JsonException)
        {
            return null;
        }
    }
}