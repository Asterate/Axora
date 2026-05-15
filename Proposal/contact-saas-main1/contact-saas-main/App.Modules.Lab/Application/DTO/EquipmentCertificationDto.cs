namespace App.Modules.Lab.Application.DTO;

public class EquipmentCertificationListResponse
{
    public Guid Id { get; set; }
    public Guid EquipmentId { get; set; }
    public Guid CertificationTypeId { get; set; }
}

public class EquipmentCertificationResponse
{
    public Guid Id { get; set; }
    public Guid EquipmentId { get; set; }
    public string EquipmentName { get; set; } = default!;

    public Guid CertificationTypeId { get; set; }
    public string CertificationTypeName { get; set; } = default!;
}

public class CreateEquipmentCertificationRequest
{
    public Guid EquipmentId { get; set; }
    public Guid CertificationTypeId { get; set; }
}