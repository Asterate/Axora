namespace App.Modules.Project.Application.DTO;

public class DocumentTypeResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class SaveDocumentTypeRequest
{
    public string NameEn { get; set; } = "??";
    public string NameEt { get; set; } = "??";
    public string? DescriptionEn { get; set; }
    public string? DescriptionEt { get; set; }
}