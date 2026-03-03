using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUserRole : IdentityUserRole<Guid>, IBaseEntity
{
    public Guid Id { get; set; }
    [ForeignKey(nameof(UserId))]
    public AppUser? User { get; set; }
    
    [ForeignKey(nameof(RoleId))]
    public AppRole? Role { get; set; }
}