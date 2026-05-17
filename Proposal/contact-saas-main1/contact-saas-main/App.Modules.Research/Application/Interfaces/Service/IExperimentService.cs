using App.Modules.Project.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IExperimentService
{
    Task<IEnumerable<ExperimentResponse>> GetAllAsync();
    Task<ExperimentResponse?> GetByIdAsync(Guid id);
    Task<ExperimentResponse> CreateAsync(SaveExperimentRequest request);
    Task UpdateAsync(Guid id, SaveExperimentRequest request);
    Task DeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
    Task<SaveExperimentRequest?> GetByIdEditAsync(Guid id);

}