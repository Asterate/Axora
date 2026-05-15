using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Lab.Domain;

public class ReagentType : BaseEntity
{
    public string Name { get; set; } = "??";
    public string? Description { get; set; }
    
    public string? Category { get; set; }
    
    public int? DefaultStorage { get; set; }

    public string? HazardLevel { get; set; }

    public string? StandardConcentration { get; set; }

    public string? MaterialFilePath { get; set; }

    public bool IsHazardous { get; set; } = false;

    public string? ColorCode { get; set; }
    
    public string? GetName(string? culture = null)
        => Name.GetLocalizedValue(culture);

    public string? GetDescription(string? culture = null)
        => Description.GetLocalizedValue(culture); 
    
    public string? GetCategory(string? culture = null)
        => Category.GetLocalizedValue(culture);
    
    public string? GetHazardLevel(string? culture = null)
        => HazardLevel.GetLocalizedValue(culture);
    
    public string? GetColorCode(string? culture = null)
        => ColorCode.GetLocalizedValue(culture);
    
}