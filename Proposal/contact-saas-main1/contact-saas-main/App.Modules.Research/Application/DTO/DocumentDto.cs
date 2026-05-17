namespace App.Modules.Project.Application.DTO;

public class DocumentListResponse
{
    public Guid Id { get; set; }
    public string DocumentName { get; set; } = default!;
    public string? DocumentTypeName { get; set; }
    public DateTime CreatedAt { get; set; }
    
}
public class DocumentResponse
{
    public Guid Id { get; set; }
    public string DocumentName { get; set; } = default!;
    public string? Description { get; set; }
    public string? DocumentTypeName { get; set; }
    public string? FilePath { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid DocumentTypeId { get; set; }
}
public class SaveDocumentRequest
{
    public string DocumentName { get; set; } = default!;
    public string? Description { get; set; } 
    public string? FilePath { get; set; }
    public Guid DocumentTypeId { get; set; }
}