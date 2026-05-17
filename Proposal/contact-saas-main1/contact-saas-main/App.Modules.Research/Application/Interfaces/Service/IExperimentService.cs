using App.Modules.Project.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IExperimentService
{
    Task<IEnumerable<ExperimentResponse>> GetAllAsync(Guid currentUserId);
    Task<ExperimentResponse?> GetByIdAsync(Guid id, Guid currentUserId);
    Task<ExperimentResponse> CreateAsync(SaveExperimentRequest request, Guid currentUserId);
    Task UpdateAsync(Guid id, SaveExperimentRequest request, Guid currentUserId);
    Task DeleteAsync(Guid id, Guid currentUserId);
    Task<List<LookupItem>> GetActivesAsync(Guid currentUserId, string? culture = null);
    Task<SaveExperimentRequest?> GetByIdEditAsync(Guid id, Guid currentUserId);

}