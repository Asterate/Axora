namespace App.Modules.Project.Application.DTO;

public class ExperimentEquipmentResponse
{
    public Guid Id { get; set; }
    public string? ExperimentName { get; set; }
    public Guid EquipementId { get; set; }
}

public class CreateExperimentEquipmentRequest
{
    public string? ExperimentName { get; set; }
    public Guid EquipementId { get; set; }
}

public class UpdateExperimentEquipmentRequest :  CreateExperimentEquipmentRequest
{
    public Guid Id { get; set; }
    
}