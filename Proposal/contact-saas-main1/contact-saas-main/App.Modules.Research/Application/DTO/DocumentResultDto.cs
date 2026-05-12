public class DocumentResultListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class DocumentResultResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateDocumentResultRequest
{
    public Guid Id { get; set; }
}

public class UpdateDocumentResultRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}