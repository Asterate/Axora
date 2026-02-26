using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities;

public class ExperimentEquipment : BaseEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    public Guid ExperimentId { get; set; }
    public Experiment Experiment { get; set; } = default!;
    public Guid EquipementId { get; set; }
    public Equipment Equipment { get; set; } = default!;
}