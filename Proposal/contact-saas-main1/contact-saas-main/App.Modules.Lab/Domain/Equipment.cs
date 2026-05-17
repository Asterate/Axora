using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Lab.Domain;

public class Equipment : BaseEntity
{
    public LangStr EquipmentName {get; set;} = "??";
    
    public string? EquipmentSerialCode {get; set;}
    
    public string? ManualFilePath { get; set; }
    
    public Guid EquipmentTypeId {get; set;}
    public EquipmentType EquipmentType { get; set; }  = default!;
    public ICollection<EquipmentCertification>? EquipmentCertificationTypes { get; set; }
}