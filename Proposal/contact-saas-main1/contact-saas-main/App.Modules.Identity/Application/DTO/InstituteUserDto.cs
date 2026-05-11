using App.Domain.Entities;
using App.Domain.Identity;

public class InstituteUserListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class InstituteUserResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateInstituteUserRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid InstituteId { get; set; }
    public EInstituteUserRole Role { get; set; }
}

public class UpdateInstituteUserRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}