public class ExperimentTaskTypeListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class ExperimentTaskTypeResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateExperimentTaskTypeRequest
{
    public Guid Id { get; set; }
}

public class UpdateExperimentTaskTypeRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}