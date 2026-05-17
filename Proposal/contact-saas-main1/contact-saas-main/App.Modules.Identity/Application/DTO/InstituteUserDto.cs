using App.Modules.Identity.Domain;

namespace App.Modules.Identity.Application.DTO;

public class InstituteUserListResponse
{
    public Guid Id { get; set; }
    public EInstituteUserRole Role { get; set; }
}

public class InstituteUserResponse
{
    public Guid Id { get; set; }
    public EInstituteUserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
}

public class SaveInstituteUserRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid InstituteId { get; set; }
    public EInstituteUserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
}