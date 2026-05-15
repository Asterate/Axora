namespace App.Modules.Lab.Application.DTO;

public class ReagentListResponse
{
    public Guid Id { get; set; }
    public string ReagentName { get; set; }  = default!;
    public string ReagentDescription { get; set; }  = default!;
    public string? CasNumber { get; set; }
    public float? MolecularWeight { get; set; }
    
    public Guid ReagentTypeId {get; set;}
    public string ReagentTypeName { get; set; } = "??";
}

public class ReagentResponse
{
    public Guid Id { get; set; }
    public string ReagentName { get; set; }  = default!;
    public string ReagentDescription { get; set; }  = default!;
    public string? CasNumber { get; set; }
    public string? ChemicalFormula { get; set; }
    public float? MolecularWeight { get; set; }
    public string? Concentration { get; set; }
    public string? StorageConditions { get; set; }
    public string? SafetyNotes { get; set; }
    public string? MaterialFilePath { get; set; }
    
    public Guid ReagentTypeId {get; set;}
}

public class CreateReagentRequest
{
    public string ReagentName { get; set; }  = default!;
    public string ReagentDescription { get; set; }  = default!;
    public string? CasNumber { get; set; }
    public string? ChemicalFormula { get; set; }
    public float? MolecularWeight { get; set; }
    public string? Concentration { get; set; }
    public string? StorageConditions { get; set; }
    public string? SafetyNotes { get; set; }
    public string? MaterialFilePath { get; set; }
    
    public Guid ReagentTypeId {get; set;}
}

public class UpdateReagentRequest :  CreateReagentRequest
{
    public Guid Id { get; set; }
}