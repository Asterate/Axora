using App.Domain;

namespace App.Domain.Entities;

public class Document : BaseEntity
{
    public string DocumentName { get; set; }  = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string FilePath { get; set; } = default!; 
    
    public ICollection<DocumentResult> DocumentResults { get; set; } = new List<DocumentResult>();
    public Guid DocumentTypeId { get; set; }
    public DocumentType DocumentType { get; set; } = default!;
}