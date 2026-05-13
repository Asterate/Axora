using App.Domain.Entities;

public class InstituteListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? InstituteName { get; set; }
    public string? InstituteAddress { get; set; }
    public string? InstituteCountry { get; set; }
    public string? InstitutePhoneNumber { get; set; }
    public bool Active { get; set; }
}

public class InstituteResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateInstituteRequest
{
    public Guid Id { get; set; }
    public string? InstituteName { get; set; }
    public string InstituteCountry {get; set;}  = default!;
    public string? InstituteAddress { get; set; }
    public string InstitutePhoneNumber { get; set; }   = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public Boolean Active { get; set; } =  true;
    public Guid InstituteTypeId { get; set; }
}

public class UpdateInstituteRequest
{
    public Guid Id { get; set; }
    public string? InstituteName { get; set; }
    public string InstituteCountry {get; set;}  = default!;
    public string? InstituteAddress { get; set; }
    public string InstitutePhoneNumber { get; set; }   = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public Boolean Active { get; set; } =  true;
    public Guid InstituteTypeId { get; set; }

    public UpdateInstituteRequest(Institute institute)
    {
        Id = institute.Id;
        InstituteName = institute.InstituteName;
        InstituteCountry = institute.InstituteCountry;
        InstituteAddress = institute.InstituteAddress;
        InstitutePhoneNumber = institute.InstitutePhoneNumber;
        Active = institute.Active;
        InstituteTypeId = institute.InstituteTypeId;
    }
}