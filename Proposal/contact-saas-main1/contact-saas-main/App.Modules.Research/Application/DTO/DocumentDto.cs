// CreateDocumentRequest — user submits these

using System.ComponentModel.DataAnnotations;
// DocumentResponse — what the view receives
public class DocumentListResponse
{
    public Guid Id { get; set; }
    public string DocumentName { get; set; } = default!;
    public string? DocumentType { get; set; }
    public DateTime CreatedAt { get; set; }
}
public class DocumentResponse
{
    public Guid Id { get; set; }
    public string DocumentName { get; set; } = default!;
    public string? Description { get; set; }
    public string? DocumentType { get; set; }
    public string? FilePath { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    [Required]
    public Guid DocumentTypeId { get; set; }
}
public class CreateDocumentRequest
{
    [Required]
    public Guid Id { get; set; }
    public string DocumentName { get; set; } = default!;
    public string? Description { get; set; }  // add this, it's on the entity
    public string? FilePath { get; set; }
    [Required]
    public Guid DocumentTypeId { get; set; }
}

// UpdateDocumentRequest — user can change these
public class UpdateDocumentRequest :  CreateDocumentRequest
{
   
}
