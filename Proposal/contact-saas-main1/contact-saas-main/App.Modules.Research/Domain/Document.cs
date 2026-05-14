using System.Text.Json;
using App.Domain;
using App.Shared.Domain;

namespace App.Domain.Entities;

public class Document : BaseEntity
{
    public string DocumentName { get; set; }  = default!;
    public string? Description { get; set; }  = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string FilePath { get; set; } = default!; 
    
    public ICollection<DocumentResult> DocumentResults { get; set; } = new List<DocumentResult>();
    public Guid DocumentTypeId { get; set; }
    public DocumentType DocumentType { get; set; } = default!;
    public string? GetName(string? culture = null)
        => GetFromJson(DocumentName, culture);

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