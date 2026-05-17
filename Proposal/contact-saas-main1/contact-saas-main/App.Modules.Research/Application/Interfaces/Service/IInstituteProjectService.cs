using App.Modules.Project.Application.DTO;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IInstituteProjectService
{
    Task<IEnumerable<InstituteProjectResponse>> GetAllAsync();
    Task<InstituteProjectResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveInstituteProjectRequest request);
    Task UpdateAsync(Guid id, SaveInstituteProjectRequest request);
    Task DeleteAsync(Guid id);
}