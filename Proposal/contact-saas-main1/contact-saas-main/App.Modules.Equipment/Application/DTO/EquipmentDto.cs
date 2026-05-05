namespace Modules.Equipment.Application.DTO;

public class EquipmentDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? EquipmentSerialCode {get; set;}
    public string? ManualFilePath { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    
    public Guid EquipmentTypeId { get; set; }
    public string? EquipmentTypeName { get; set; }
    public string? Description { get; set; }
    public ICollection<EquipmentCertificationTypeDto>? EquipmentCertificationTypes { get; set; }

}