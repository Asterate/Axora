using App.Shared.Domain;

namespace App.Modules.Lab.Domain;

public class Reagent : BaseEntity
{
    public LangStr ReagentName { get; set; }  = default!;
    public LangStr ReagentDescription { get; set; }  = default!;
    public string? CasNumber { get; set; }
    public string? ChemicalFormula { get; set; }
    public float? MolecularWeight { get; set; }
    public string? Concentration { get; set; }
    public LangStr? StorageConditions { get; set; }
    public LangStr? SafetyNotes { get; set; }
    public string? MaterialFilePath { get; set; }
    
    public Guid ReagentTypeId {get; set;}
    public ReagentType ReagentType {get; set;} = default!;
}