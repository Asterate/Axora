namespace App.DTO.v1;

public class LangStrDto
{
    /// <summary>
    /// Translations dictionary with culture code as key (e.g., "en", "et")
    /// </summary>
    public Dictionary<string, string> Translations { get; set; } = new();

    /// <summary>
    /// Get translation for a specific culture
    /// </summary>
    public string? GetTranslation(string? culture = null)
    {
        if (Translations.Count == 0) return null;
        
        culture = culture?.Trim() ?? Thread.CurrentThread.CurrentUICulture.Name;
        
        if (Translations.ContainsKey(culture))
        {
            return Translations[culture];
        }

        var neutralCulture = culture.Split('-')[0];
        if (Translations.ContainsKey(neutralCulture))
        {
            return Translations[neutralCulture];
        }

        // Return default culture ("en") if available
        if (Translations.ContainsKey("en"))
        {
            return Translations["en"];
        }

        return Translations.Values.FirstOrDefault();
    }

    /// <summary>
    /// Set translation for a specific culture
    /// </summary>
    public void SetTranslation(string value, string? culture = null)
    {
        culture = culture?.Trim() ?? Thread.CurrentThread.CurrentUICulture.Name;
        var neutralCulture = culture.Split('-')[0];
        Translations[neutralCulture] = value;
    }

    public override string ToString()
    {
        return GetTranslation() ?? "????";
    }
}