using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Project.Domain;

public class ProjectType : BaseEntity
{
    public string Name { get; set; } = "{}";

    public string? Description { get; set; }


    // Helper to read a translation by key from the JSON stored in Name
    public string? GetName(string? culture = null)
        => Name.GetLocalizedValue(culture);

    public string? GetDescription(string? culture = null)
        => Description.GetLocalizedValue(culture);
    
}