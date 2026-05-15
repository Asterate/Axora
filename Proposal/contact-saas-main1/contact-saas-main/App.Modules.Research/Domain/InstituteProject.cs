using App.Shared.Domain;

namespace App.Modules.Project.Domain;

public class InstituteProject : BaseEntity
{
    public Guid InstituteId { get; set; }
    public Institute Institute { get; set; } = default!;
    public Guid ProjectId  { get; set; }
}