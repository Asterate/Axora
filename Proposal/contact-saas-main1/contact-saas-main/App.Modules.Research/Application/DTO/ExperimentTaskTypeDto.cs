namespace App.Modules.Project.Application.DTO;

public class ExperimentTaskTypeResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class SaveExperimentTaskTypeRequest
{
    public string NameEn { get; set; } = String.Empty;
    public string NameEt { get; set; } = String.Empty;
    public string? DescriptionEn { get; set; }
    public string? DescriptionEt { get; set; }
}

