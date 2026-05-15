using App.Modules.Lab.Application.DTO;

namespace App.Modules.Lab.Application.Interfaces;

public interface ILabTypeService
{
    Task<IEnumerable<LabTypeListResponse>> GetAllAsync();
    Task<LabTypeResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateLabTypeRequest request);
    Task UpdateAsync(Guid id, UpdateLabTypeRequest request);
    Task DeleteAsync(Guid id);
}