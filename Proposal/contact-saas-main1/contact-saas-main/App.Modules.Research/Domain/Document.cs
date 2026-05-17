using App.Shared.Domain;

namespace App.Modules.Project.Domain;

public class Document : BaseEntity
{
    public LangStr DocumentName { get; set; }  = default!;
    public LangStr? Description { get; set; }
    public string? FilePath { get; set; }
    
    public ICollection<DocumentResult> DocumentResults { get; set; } = new List<DocumentResult>();
    public Guid DocumentTypeId { get; set; }
    public DocumentType DocumentType { get; set; } = default!;
    
}