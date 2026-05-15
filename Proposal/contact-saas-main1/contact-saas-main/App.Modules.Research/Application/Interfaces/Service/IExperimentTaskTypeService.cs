using App.Shared.Contracts;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IExperimentTaskTypeService
{
    Task<IEnumerable<ExperimentTaskTypeListResponse>> GetAllAsync();
    Task<ExperimentTaskTypeResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateExperimentTaskTypeRequest request);
    Task UpdateAsync(Guid id, UpdateExperimentTaskTypeRequest request);
    Task DeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
}