using App.Modules.Project.Application.DTO;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IExperimentService
{
    Task<IEnumerable<ExperimentResponse>> GetAllAsync();
    Task<ExperimentResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateExperimentRequest request);
    Task UpdateAsync(Guid id, UpdateExperimentRequest request);
    Task DeleteAsync(Guid id);

}