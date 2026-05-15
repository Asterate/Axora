using App.Modules.Project.Application.DTO;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IExperimentEquipmentService
{
    Task<IEnumerable<ExperimentEquipmentResponse>> GetAllAsync();
    Task<ExperimentEquipmentResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateExperimentEquipmentRequest request);
    Task UpdateAsync(Guid id, UpdateExperimentEquipmentRequest request);
    Task DeleteAsync(Guid id);
}