using App.Modules.Project.Application.DTO;
using App.Modules.Project.Domain;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IExperimentTaskService
{
    Task<IEnumerable<ExperimentTaskListResponse>> GetAllAsync();
    Task<IEnumerable<ExperimentTaskListResponse>> GetAllByExperimentIdsAsync(IEnumerable<Guid> experimentIds);
    Task CreateAsync(SaveExperimentTaskRequest request);
    Task<ExperimentTaskResponse?> GetByIdAsync(Guid id);
    Task<ExperimentTask> CreateAndReturnAsync(SaveExperimentTaskRequest request);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(Guid id, SaveExperimentTaskRequest request);
    Task SoftDeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
}