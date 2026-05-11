public class ProjectTypeListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class ProjectTypeResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateProjectTypeRequest
{
    public Guid Id { get; set; }
}

public class UpdateProjectTypeRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}