using App.Domain.Entities;
using App.Shared.Domain;

namespace App.Modules.Project.Domain;

public class DocumentResult : BaseEntity
{
    public Guid DocumentId { get; set; }
    public Document Document { get; set; } = default!;
    public Guid ResultId { get; set; }
    public Result Result { get; set; } = default!;
}