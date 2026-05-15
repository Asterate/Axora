namespace App.Modules.Identity.Application.Interfaces;

public interface IInstituteUserService
{
    Task<IEnumerable<InstituteUserListResponse>> GetAllAsync();
    Task<InstituteUserResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateInstituteUserRequest request);
    Task DeleteAsync(Guid id);
    Task<bool> HasInstituteAsync(Guid userId);
}