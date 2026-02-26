using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class Reagent : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public string ReagentName { get; set; }  = default!;
    [StringLength(128, MinimumLength = 3)]
    public string ReagentDescription { get; set; }  = default!;
    [StringLength(50)]
    public string? CASNumber { get; set; }
    public string? ChemicalFormula { get; set; }
    public float? MolecularWeight { get; set; }
    public string? Concentration { get; set; }
    public string? StorageConditions { get; set; }
    public string? SafetyNotes { get; set; }
    public string? MaterialFilePath { get; set; }
    
    public Guid ReagentTypeId {get; set;}
    public ReagentType ReagentType {get; set;} = default!;
}