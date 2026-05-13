using App.Modules.Equipment.Domain;
using App.Shared.Domain;

namespace App.Modules.Lab.Domain;

public class Equipment : BaseEntity
{
    public LangStr EquipmentName {get; set;} = new LangStr();
    
    public string? EquipmentSerialCode {get; set;}
    
    public string? ManualFilePath { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    public DateTime? DeletedAt { get; set; }
    
    public Guid EquipmentTypeId {get; set;}
    public EquipmentType EquipmentType { get; set; }  = default!;
    public ICollection<EquipmentCertificationType>? EquipmentCertificationTypes { get; set; }
}