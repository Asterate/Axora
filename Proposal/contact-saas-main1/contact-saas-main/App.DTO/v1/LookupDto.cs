namespace App.DTO.v1;

public class LookupDto
{
    public Guid Id { get; set; }
    
    
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// Translations for internationalization (optional - use when API supports i18n)
    /// </summary>
    public Dictionary<string, string>? Translations { get; set; }
    
    /// <summary>
    /// Get localized name based on current culture
    /// </summary>
    public string GetLocalizedName(string? culture = null)
    {
        if (Translations == null || Translations.Count == 0)
            return Name;
        
        culture = culture?.Trim() ?? Thread.CurrentThread.CurrentUICulture.Name;
        
        if (Translations.ContainsKey(culture))
            return Translations[culture];
        
        var neutralCulture = culture.Split('-')[0];
        if (Translations.ContainsKey(neutralCulture))
            return Translations[neutralCulture];
        
        return Name; // fallback to default name
    }
}