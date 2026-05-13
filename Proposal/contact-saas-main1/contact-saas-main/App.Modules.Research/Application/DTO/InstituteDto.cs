using App.Domain.Entities;
using App.Shared.Domain;

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
    public LangStr InstituteName { get; set; } = new();
    public string InstituteCountry {get; set;}  = default!;
    public LangStr InstituteAddress { get; set; } = new();
    public string InstitutePhoneNumber { get; set; }   = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public Boolean Active { get; set; } =  true;
    public Guid InstituteTypeId { get; set; }
}

public class UpdateInstituteRequest
{
    public Guid Id { get; set; }
    public LangStr InstituteName { get; set; } = new();
    public string InstituteCountry {get; set;}  = default!;
    public LangStr InstituteAddress { get; set; } = new();
    public string InstitutePhoneNumber { get; set; }   = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public Boolean Active { get; set; } =  true;
    public Guid InstituteTypeId { get; set; }
    
    public UpdateInstituteRequest(Institute experimentTask)
    {
        Id = experimentTask.Id;
        InstituteName = experimentTask.InstituteName;
        InstituteCountry = experimentTask.InstituteCountry;
        InstituteAddress = experimentTask.InstituteAddress;
        InstitutePhoneNumber = experimentTask.InstitutePhoneNumber;
        Active = experimentTask.Active;
        InstituteTypeId = experimentTask.InstituteTypeId;
    }
}