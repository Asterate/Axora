using App.Modules.Lab.Application.DTO;

namespace App.Modules.Lab.Application.Interfaces;

public interface ILabService
{
    Task<IEnumerable<LabResponse>> GetAllAsync();
    Task<LabResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateLabRequest request);
    Task UpdateAsync(Guid id, UpdateLabRequest request);
    Task DeleteAsync(Guid id);
}