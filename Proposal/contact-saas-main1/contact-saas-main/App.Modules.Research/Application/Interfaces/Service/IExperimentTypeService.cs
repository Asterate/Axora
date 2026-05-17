using App.Modules.Project.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IExperimentTypeService
{
    Task<IEnumerable<ExperimentTypeResponse>> GetAllAsync();
    Task<ExperimentTypeResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveExperimentTypeRequest request);
    Task UpdateAsync(Guid id, SaveExperimentTypeRequest request);
    Task DeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
}