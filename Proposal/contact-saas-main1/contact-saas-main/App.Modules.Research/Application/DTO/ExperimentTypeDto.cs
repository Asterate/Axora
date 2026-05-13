using App.Domain.Entities;

public class ExperimentTypeListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class ExperimentTypeResponse
{
    public Guid Id { get; set; }
    public string? NameEn { get; set; }
    public string? NameEt { get; set; }
    public string? DescriptionEn { get; set; }
    public string? DescriptionEt { get; set; }
}

public class CreateExperimentTypeRequest
{
    public Guid Id { get; set; }
    public string? NameEn { get; set; }
    public string? NameEt { get; set; }
    public string? DescriptionEn { get; set; }
    public string? DescriptionEt { get; set; }
}

public class UpdateExperimentTypeRequest
{
    public Guid Id { get; set; }
    public string? NameEn { get; set; }
    public string? NameEt { get; set; }
    public string? DescriptionEn { get; set; }
    public string? DescriptionEt { get; set; }
    
    public UpdateExperimentTypeRequest(ExperimentType experimentTask)
    {
        NameEn = experimentTask.Name;
    }
}