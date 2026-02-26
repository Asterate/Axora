namespace App.Domain.Entities;

public class DocumentResult : BaseEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    public Guid DocumentId { get; set; }
    public Document Document { get; set; } = default!;
    public Guid ResultId { get; set; }
    public Result Result { get; set; } = default!;
}