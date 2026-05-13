using App.Domain.Entities;

public class DocumentTypeListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class DocumentTypeResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateDocumentTypeRequest
{
    public Guid Id { get; set; }
}

public class UpdateDocumentTypeRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public UpdateDocumentTypeRequest(DocumentType documentType)
    {
        Id = documentType.Id;
        Name = documentType.Name;
    }
}