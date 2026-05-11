using App.Domain.Entities;

public class DocumentListResponse
{
    public Guid Id { get; set; }
    public string? DocumentName { get; set; }
    public string? DocumentType { get; set; }
}

public class DocumentResponse
{
    public Guid Id { get; set; }
    public string? DocumentName { get; set; }
    public string? DocumentType { get; set; }
}

public class CreateDocumentRequest
{
    public Guid Id { get; set; }
    public string? DocumentName { get; set; }
    public string? FilePath { get; set; }
    public Guid DocumentTypeId { get; set; }
}

public class UpdateDocumentRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? DocumentName { get; set; }
    public string? FilePath { get; set; }
    public ICollection<DocumentResult> DocumentResults { get; set; } = new List<DocumentResult>();
    public Guid DocumentTypeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    public UpdateDocumentRequest(Document document)
    {
        Id = document.Id;
        DocumentName = document.DocumentName;
        FilePath = document.FilePath;
        DocumentTypeId = document.DocumentTypeId;
    }
}