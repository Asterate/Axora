using App.Domain.Entities;

public class InstituteLabListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class InstituteLabResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateInstituteLabRequest
{
    public Guid Id { get; set; }
}

public class UpdateInstituteLabRequest
{
    public Guid Id { get; set; }
    
    public UpdateInstituteLabRequest(InstituteLab experimentTask)
    {
        Id = experimentTask.Id;
    }
}