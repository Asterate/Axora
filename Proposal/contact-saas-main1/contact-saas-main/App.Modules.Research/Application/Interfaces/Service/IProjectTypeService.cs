using App.Modules.Project.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IProjectTypeService
{
    Task<IEnumerable<ProjectTypeListResponse>> GetAllAsync();
    Task<ProjectTypeResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateProjectTypeRequest request);
    Task UpdateAsync(Guid id, UpdateProjectTypeRequest request);
    Task DeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
}