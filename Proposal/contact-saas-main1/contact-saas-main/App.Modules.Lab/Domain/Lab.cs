using App.Shared.Domain;

namespace App.Modules.Lab.Domain;

public class Lab : BaseEntity
{
    public string LabName { get; set; }  = default!;
    public string LabAddress { get; set; }  = default!;
    
    public int LabCapacity { get; set; }
    
    public bool LabIsActive { get; set; } = true;
    
    public Guid LabTypeId { get; set; }
    public LabType LabType { get; set; } = default!;
}