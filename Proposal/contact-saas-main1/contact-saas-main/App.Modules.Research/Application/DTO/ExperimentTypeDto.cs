namespace App.Modules.Project.Application.DTO;

public class ExperimentTypeResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class SaveExperimentTypeRequest
{
    public string NameEn { get; set; } = String.Empty;
    public string NameEt { get; set; } = String.Empty;
    public string? DescriptionEn { get; set; }
    public string? DescriptionEt { get; set; }
}