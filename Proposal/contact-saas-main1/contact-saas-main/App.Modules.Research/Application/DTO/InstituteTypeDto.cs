using App.Domain.Entities;

public class InstituteTypeListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class InstituteTypeResponse
{
    public Guid Id { get; set; }
    public string? NameEn { get; set; }
    public string? NameEt { get; set; }
    public string? DescriptionEn { get; set; }
    public string? DescriptionEt { get; set; }
}

public class CreateInstituteTypeRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? NameEn { get; set; }
    public string? NameEt { get; set; }
    public string? DescriptionEn { get; set; }
    public string? DescriptionEt { get; set; }
}

public class UpdateInstituteTypeRequest
{
    public Guid Id { get; set; }
    public string? NameEn { get; set; }
    public string? NameEt { get; set; }
    public string? DescriptionEn { get; set; }
    public string? DescriptionEt { get; set; }

    public UpdateInstituteTypeRequest(InstituteTypeResponse instituteType)
    {
        Id = instituteType.Id;
    }
}