using App.Shared.Domain;

namespace App.Modules.Lab.Domain;

public class Reagent : BaseEntity
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
    public ReagentType ReagentType {get; set;} = default!;
}