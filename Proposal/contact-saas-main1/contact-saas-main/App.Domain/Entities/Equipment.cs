using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Resources;
using App.Domain;

namespace App.Domain.Entities;

public class Equipment : BaseEntity
{
    [Display(Name = "Name", ResourceType = typeof(Base.Resources.Common))]
    [StringLength(128, MinimumLength = 3)]
    [Column(TypeName = "jsonb")]
    public LangStr EquipmentName {get; set;} = new LangStr();
    
    [Display(Name = "SerialCode", ResourceType = typeof(Base.Resources.Common))]
    [StringLength(128, MinimumLength = 10)]
    public string? EquipmentSerialCode {get; set;}
    
    [Display(Name = "Manual", ResourceType = typeof(Base.Resources.Common))]
    public string? ManualFilePath { get; set; }
    
    [Display(Name = "CreatedAt", ResourceType = typeof(Base.Resources.Common))]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Display(Name = "UpdatedAt", ResourceType = typeof(Base.Resources.Common))]
    public DateTime? UpdatedAt { get; set; }
    
    [Display(Name = "DeletedAt", ResourceType = typeof(Base.Resources.Common))]
    public DateTime? DeletedAt { get; set; }
    
    public Guid EquipmentTypeId {get; set;}
    public EquipmentType EquipmentType { get; set; }  = default!;
    public ICollection<EquipmentCertificationType>? EquipmentCertificationTypes { get; set; }
}