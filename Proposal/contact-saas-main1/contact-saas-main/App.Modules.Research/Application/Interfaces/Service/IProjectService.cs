using App.Modules.Project.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IProjectService
{
    Task<IEnumerable<ProjectListResponse>> GetAllAsync();
    Task<ProjectResponse?> GetByIdAsync(Guid id);
    Task<ProjectResponse> CreateAsync(SaveProjectRequest request);
    Task UpdateAsync(Guid id, SaveProjectRequest request);
    Task DeleteAsync(Guid id);
    Task<int> CountAsync();
    Task<IEnumerable<ProjectListResponse>> GetRecentAsync(int take);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
    Task<SaveProjectRequest?> GetByIdEditAsync(Guid id);
}