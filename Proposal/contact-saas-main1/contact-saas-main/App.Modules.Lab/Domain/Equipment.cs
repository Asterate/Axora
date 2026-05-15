using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Lab.Domain;

public class Equipment : BaseEntity
{
    public string EquipmentName {get; set;} = string.Empty;
    
    public string? EquipmentSerialCode {get; set;}
    
    public string? ManualFilePath { get; set; }
    
    public Guid EquipmentTypeId {get; set;}
    public EquipmentType EquipmentType { get; set; }  = default!;
    public ICollection<EquipmentCertification>? EquipmentCertificationTypes { get; set; }
    
    public string? GetName(string? culture = null)
        => EquipmentName.GetLocalizedValue(culture);
}