using App.Modules.Identity.Application.DTO;
using App.Modules.Identity.Domain;

namespace App.Modules.Identity.Application.Interfaces;

public interface IInstituteUserService
{
    Task<IEnumerable<InstituteUserListResponse>> GetAllAsync();
    Task<InstituteUserResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveInstituteUserRequest request);
    Task DeleteAsync(Guid id);
    Task<bool> HasInstituteAsync(Guid userId);
    Task UpdateAsync(Guid id, SaveInstituteUserRequest request);
    Task<InstituteUser?> GetByUserIdAsync(Guid userId);
}