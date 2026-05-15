using App.Domain.Entities;
using App.Shared.Domain;
using App.Shared.Helpers;

namespace App.Modules.Project.Domain;

public class Document : BaseEntity
{
    public string DocumentName { get; set; }  = default!;
    public string? Description { get; set; }
    public string? FilePath { get; set; }
    
    public ICollection<DocumentResult> DocumentResults { get; set; } = new List<DocumentResult>();
    public Guid DocumentTypeId { get; set; }
    public DocumentType DocumentType { get; set; } = default!;
    public string? GetName(string? culture = null)
        => DocumentName.GetLocalizedValue(culture);

    public string? GetDescription(string? culture = null)
        => DocumentName.GetLocalizedValue(culture);
    
}