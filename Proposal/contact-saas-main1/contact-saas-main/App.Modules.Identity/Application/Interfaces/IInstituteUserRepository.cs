using App.Modules.Identity.Domain;
using App.Shared.Contracts;

namespace App.Modules.Identity.Application.Interfaces;

public interface IInstituteUserRepository : IBaseRepository<InstituteUser>
{
    Task<InstituteUser?> GetByUserIdAsync(Guid userId);

}