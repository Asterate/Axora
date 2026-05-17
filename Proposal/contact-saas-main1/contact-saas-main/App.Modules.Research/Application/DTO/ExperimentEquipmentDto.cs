namespace App.Modules.Project.Application.DTO;

public class ExperimentEquipmentResponse
{
    public Guid Id { get; set; }
    public string? ExperimentName { get; set; }
    public Guid EquipementId { get; set; }
}

public class SaveExperimentEquipmentRequest
{
    public string? ExperimentName { get; set; }
    public Guid EquipementId { get; set; }
}