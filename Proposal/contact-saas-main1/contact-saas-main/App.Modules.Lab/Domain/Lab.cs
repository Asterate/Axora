using App.Shared.Domain;

namespace App.Modules.Lab.Domain;

public class Lab : BaseEntity
{
    public LangStr LabName { get; set; }  = String.Empty;
    public string LabAddress { get; set; }  = String.Empty;
    
    public int LabCapacity { get; set; }
    
    public bool LabIsActive { get; set; } = true;
    
    public Guid LabTypeId { get; set; }
    public LabType LabType { get; set; } = default!;
}