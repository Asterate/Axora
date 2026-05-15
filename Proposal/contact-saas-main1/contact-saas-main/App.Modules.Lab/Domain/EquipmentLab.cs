using App.Shared.Domain;

namespace App.Modules.Lab.Domain;

public class EquipmentLab : BaseEntity
{
    public int Quantity { get; set; }

    public Guid LabId { get; set; }
    public Lab Lab { get; set; } = default!;

    public Guid EquipmentId { get; set; }
    public Equipment Equipment { get; set; } = default!;
    
}