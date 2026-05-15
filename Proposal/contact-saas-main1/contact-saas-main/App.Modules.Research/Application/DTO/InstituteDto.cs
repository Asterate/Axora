namespace App.Modules.Project.Application.DTO;

public class InstituteListResponse
{
    public Guid Id { get; set; }
    public string? InstituteName { get; set; }
    public bool Active { get; set; }
    public string? InstituteTypeName { get; set; }
}

public class InstituteResponse
{
    public Guid Id { get; set; }
    public string? InstituteName { get; set; }
    public string? InstituteAddress { get; set; }
    public string? InstituteCountry { get; set; }
    public string? InstitutePhoneNumber { get; set; }
    public bool Active { get; set; }
    public string? InstituteTypeName { get; set; }
    
}

public class CreateInstituteRequest
{
    public string? InstituteName { get; set; }
    public string InstituteCountry {get; set;}  = default!;
    public string? InstituteAddress { get; set; }
    public string InstitutePhoneNumber { get; set; }   = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Boolean Active { get; set; } =  true;
    public Guid InstituteTypeId { get; set; }
}

public class UpdateInstituteRequest :  CreateInstituteRequest
{
    public Guid Id { get; set; }
}