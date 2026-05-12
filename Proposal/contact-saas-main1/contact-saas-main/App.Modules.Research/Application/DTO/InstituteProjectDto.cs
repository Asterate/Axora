using App.Domain.Entities;

public class InstituteProjectListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class InstituteProjectResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateInstituteProjectRequest
{
    public Guid Id { get; set; }
}

public class UpdateInstituteProjectRequest
{
    public Guid Id { get; set; }

    public UpdateInstituteProjectRequest(InstituteProject instituteProject)
    {
        Id = instituteProject.Id;
    }
}