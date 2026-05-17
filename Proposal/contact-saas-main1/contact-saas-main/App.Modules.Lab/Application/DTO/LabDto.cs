namespace App.Modules.Lab.Application.DTO;

public class LabResponse
{
    public Guid Id { get; set; }
    public string LabName { get; set; }  = default!;
    public string LabAddress { get; set; }  = default!;
    
    public int LabCapacity { get; set; }
    
    public bool LabIsActive { get; set; } = true;
    
    public string LabTypeName { get; set; } = "??";
}

public class SaveLabRequest
{
    public string LabNameEn { get; set; }  = default!;
    public string LabNameEt { get; set; }  = default!;
    public string LabAddress { get; set; }  = default!;
    
    public int LabCapacity { get; set; }
    
    public bool LabIsActive { get; set; } = true;
    
    public Guid LabTypeId { get; set; }
}