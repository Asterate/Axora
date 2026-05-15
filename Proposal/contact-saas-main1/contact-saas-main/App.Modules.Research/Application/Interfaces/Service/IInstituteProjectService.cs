using App.Modules.Project.Application.DTO;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IInstituteProjectService
{
    Task<IEnumerable<InstituteProjectResponse>> GetAllAsync();
    Task<InstituteProjectResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateInstituteProjectRequest request);
    Task UpdateAsync(Guid id, UpdateInstituteProjectRequest request);
    Task DeleteAsync(Guid id);
}