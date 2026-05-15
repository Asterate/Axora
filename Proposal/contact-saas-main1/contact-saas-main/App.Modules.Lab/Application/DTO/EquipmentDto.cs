using App.Modules.Lab.Domain;

namespace App.Modules.Lab.Application.DTO;

public class EquipmentListResponse
{
    public Guid Id { get; set; }
    public string? EquipmentName { get; set; }
    public string? EquipmentSerialCode {get; set;}
    public Guid EquipmentTypeId {get; set;}
}

public class EquipmentResponse
{
    public Guid Id { get; set; }
    public string? EquipmentName { get; set; }
    public string? EquipmentSerialCode {get; set;}
    public string? ManualFilePath { get; set; }
    public Guid EquipmentTypeId {get; set;}
    public ICollection<EquipmentCertification>? EquipmentCertificationTypes { get; set; }
}

public class CreateEquipmentRequest
{
    public string? EquipmentName { get; set; }
    public string? EquipmentSerialCode {get; set;}
    public string? ManualFilePath { get; set; }
    public Guid EquipmentTypeId {get; set;}
    public ICollection<EquipmentCertification>? EquipmentCertificationTypes { get; set; }
}

public class UpdateEquipmentRequest :  CreateEquipmentRequest
{
    public Guid Id { get; set; }
}