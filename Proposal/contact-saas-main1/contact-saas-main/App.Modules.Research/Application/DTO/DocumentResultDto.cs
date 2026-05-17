namespace App.Modules.Project.Application.DTO;

public class DocumentResultResponse
{
    public Guid Id { get; set; }
    public string? DocumentName { get; set; }
    public string? ResultName { get; set; }
}

public class SaveDocumentResultRequest
{
    public Guid ResultId { get; set; }
    public Guid DocumentId { get; set; }
}