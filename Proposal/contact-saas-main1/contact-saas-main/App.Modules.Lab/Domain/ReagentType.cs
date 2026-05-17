using App.Shared.Domain;
namespace App.Modules.Lab.Domain;

public class ReagentType : BaseEntity
{
    public LangStr Name { get; set; } = "??";
    public LangStr? Description { get; set; }
    
    public LangStr? Category { get; set; }
    
    public int? DefaultStorage { get; set; }

    public LangStr? HazardLevel { get; set; }

    public string? StandardConcentration { get; set; }

    public string? MaterialFilePath { get; set; }

    public bool IsHazardous { get; set; } = false;

    public LangStr? ColorCode { get; set; }
    
}