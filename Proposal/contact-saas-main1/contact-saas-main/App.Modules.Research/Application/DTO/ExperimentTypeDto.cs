using App.Domain.Entities;

public class ExperimentTypeListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class ExperimentTypeResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateExperimentTypeRequest
{
    public string? Name { get; set; }
}

public class UpdateExperimentTypeRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    
    public UpdateExperimentTypeRequest(ExperimentType experimentTask)
    {
        Name = experimentTask.Name;
    }
}