using App.Modules.Project.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IProjectTypeService
{
    Task<IEnumerable<ProjectTypeResponse>> GetAllAsync();
    Task<ProjectTypeResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveProjectTypeRequest request);
    Task UpdateAsync(Guid id, SaveProjectTypeRequest request);
    Task DeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
}