namespace App.Modules.Lab.Application.DTO;

public class ReagentTypeListResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "??";
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? HazardLevel { get; set; }
    public bool IsHazardous { get; set; } = false;
    public string? ColorCode { get; set; }
}

public class ReagentTypeResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "??";
    public string? Description { get; set; }
    
    public string? Category { get; set; }
    
    public int? DefaultStorage { get; set; }

    public string? HazardLevel { get; set; }

    public string? StandardConcentration { get; set; }

    public string? MaterialFilePath { get; set; }

    public bool IsHazardous { get; set; } = false;

    public string? ColorCode { get; set; }
}

public class SaveReagentTypeRequest
{
    public string NameEn { get; set; } = "??";
    public string NameEt { get; set; } = "??";
    public string? DescriptionEn { get; set; }
    public string? DescriptionEt { get; set; }
    
    public string? CategoryEn { get; set; }
    public string? CategoryEt { get; set; }
    
    public int? DefaultStorage { get; set; }

    public string? HazardLevelEn { get; set; }
    public string? HazardLevelEt { get; set; }

    public string? StandardConcentration { get; set; }

    public string? MaterialFilePath { get; set; }

    public bool IsHazardous { get; set; } = false;

    public string? ColorCodeEn { get; set; }
    public string? ColorCodeEt { get; set; }
}