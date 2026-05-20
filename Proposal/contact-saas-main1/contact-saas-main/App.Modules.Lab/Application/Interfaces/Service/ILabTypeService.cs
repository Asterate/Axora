using App.Modules.Lab.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Interfaces.Service;

public interface ILabTypeService
{
    Task<IEnumerable<LabTypeResponse>> GetAllAsync();
    Task<LabTypeResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveLabTypeRequest request);
    Task UpdateAsync(Guid id, SaveLabTypeRequest request);
    Task DeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
}