public class ProjectListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class ProjectResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateProjectRequest
{
    public Guid Id { get; set; }
    
}

public class UpdateProjectRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}