namespace App.Modules.Lab.Application.DTO;

public class EquipmentLabResponse
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Guid LabId { get; set; }
    public string LabName { get; set; } = default!;
    public Guid EquipmentId { get; set; }
    public string EquipmentName { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}

public class CreateEquipmentLabRequest
{
    public int Quantity { get; set; }
    public Guid LabId { get; set; }
    public Guid EquipmentId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}

public class UpdateEquipmentLabRequest :  CreateEquipmentLabRequest
{
    public Guid Id { get; set; }
}