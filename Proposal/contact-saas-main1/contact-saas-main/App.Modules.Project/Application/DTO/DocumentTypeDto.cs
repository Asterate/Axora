public class DocumentTypeListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
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
}