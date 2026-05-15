using App.Shared.Domain;

namespace App.Modules.Lab.Domain;

public class InstituteLab :  BaseEntity
{
    public Guid InstituteId { get; set; }
    public Guid LabId { get; set; }
    public Lab Lab { get; set; } = default!;
}