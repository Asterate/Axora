using App.Modules.Project.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IExperimentTypeService
{
    Task<IEnumerable<ExperimentTypeListResponse>> GetAllAsync();
    Task<ExperimentTypeResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateExperimentTypeRequest request);
    Task UpdateAsync(Guid id, UpdateExperimentTypeRequest request);
    Task DeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
}