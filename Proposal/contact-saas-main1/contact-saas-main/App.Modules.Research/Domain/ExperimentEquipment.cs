using App.Shared.Domain;

namespace App.Modules.Project.Domain;

public class ExperimentEquipment : BaseEntity
{
    public Guid ExperimentId { get; set; }
    public Experiment Experiment { get; set; } = default!;
    public Guid EquipmentId { get; set; }
    
}