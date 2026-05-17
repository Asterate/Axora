using App.Modules.Project.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IExperimentTaskTypeService
{
    Task<IEnumerable<ExperimentTaskTypeResponse>> GetAllAsync();
    Task<ExperimentTaskTypeResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveExperimentTaskTypeRequest request);
    Task UpdateAsync(Guid id, SaveExperimentTaskTypeRequest request);
    Task DeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
}