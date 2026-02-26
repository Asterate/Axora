using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class Equipment : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public string EquipmentName {get; set;} = default!;
    [StringLength(128, MinimumLength = 10)]
    public string? EquipmentSerialCode {get; set;}
    public string? ManualFilePath { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    public Guid EquipmentTypeId {get; set;}
    public EquipmentType EquipmentType { get; set; }  = default!;
    public ICollection<EquipmentCertificationType>? EquipmentCertificationTypes { get; set; }
}