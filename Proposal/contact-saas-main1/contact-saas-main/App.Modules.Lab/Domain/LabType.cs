using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Lab.Domain;

public class LabType : BaseEntity
{
    public string Name { get; set; } = "??";

    public string? Description { get; set; }
    
    public string? GetName(string? culture = null)
        => Name.GetLocalizedValue(culture);

    public string? GetDescription(string? culture = null)
        => Description.GetLocalizedValue(culture);
    
}