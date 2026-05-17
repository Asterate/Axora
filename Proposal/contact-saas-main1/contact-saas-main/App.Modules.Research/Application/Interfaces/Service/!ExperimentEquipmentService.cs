using App.Modules.Project.Application.DTO;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IExperimentEquipmentService
{
    Task<IEnumerable<ExperimentEquipmentResponse>> GetAllAsync();
    Task<ExperimentEquipmentResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveExperimentEquipmentRequest request);
    Task UpdateAsync(Guid id, SaveExperimentEquipmentRequest request);
    Task DeleteAsync(Guid id);
}