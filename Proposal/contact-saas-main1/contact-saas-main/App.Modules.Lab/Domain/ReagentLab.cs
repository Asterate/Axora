using App.Shared.Domain;

namespace App.Modules.Lab.Domain;

public class ReagentLab : BaseEntity
{
    public int Quantity { get; set; }
    public string Unit { get; set; }  = default!;
    public Guid LabId { get; set; }
    public Lab Lab { get; set; } = default!;
    public Guid ReagentId { get; set; }
    public Reagent Reagent { get; set; }= default!;
}