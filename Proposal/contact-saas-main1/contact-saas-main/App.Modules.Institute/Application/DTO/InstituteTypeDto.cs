using App.Domain.Entities;

public class InstituteTypeListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class InstituteTypeResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateInstituteTypeRequest
{
    public string? Name { get; set; }
}

public class UpdateInstituteTypeRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public UpdateInstituteTypeRequest(InstituteTypeResponse instituteType)
    {
        Id = instituteType.Id;
    }
}