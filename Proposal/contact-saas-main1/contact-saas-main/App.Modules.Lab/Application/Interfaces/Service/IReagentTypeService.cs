using App.Modules.Lab.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Interfaces.Service;

public interface IReagentTypeService
{
    Task<IEnumerable<ReagentTypeResponse>> GetAllAsync();
    Task<ReagentTypeResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveReagentTypeRequest request);
    Task UpdateAsync(Guid id, SaveReagentTypeRequest request);
    Task DeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
}