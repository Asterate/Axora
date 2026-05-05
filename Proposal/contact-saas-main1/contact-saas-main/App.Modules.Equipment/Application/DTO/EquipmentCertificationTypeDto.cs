namespace Modules.Equipment.Application.DTO;

public class EquipmentCertificationTypeDto
{
    public Guid EquipmentId { get; set; }
    public Guid CertificationTypeId { get; set; }
    public string? CertificationTypeName { get; set; }
}