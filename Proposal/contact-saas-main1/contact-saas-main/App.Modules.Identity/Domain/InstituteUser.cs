using App.Domain.Identity;
using App.Shared.Domain;

namespace App.Modules.Identity.Domain;

public class InstituteUser : BaseEntity
{
    public Guid UserId { get; set; }
    public AppUser User { get; set; }  = default!;
    
    public Guid InstituteId { get; set; }
    public EInstituteUserRole Role { get; set; } = EInstituteUserRole.Employee;

}