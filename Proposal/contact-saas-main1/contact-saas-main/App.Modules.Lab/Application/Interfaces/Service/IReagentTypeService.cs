using App.Modules.Lab.Application.DTO;
using App.Shared.Contracts;

namespace App.Modules.Lab.Application.Interfaces;

public interface IReagentTypeService
{
    Task<IEnumerable<ReagentTypeListResponse>> GetAllAsync();
    Task<ReagentTypeResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateReagentTypeRequest request);
    Task UpdateAsync(Guid id, UpdateReagentTypeRequest request);
    Task DeleteAsync(Guid id);
    Task<List<LookupItem>> GetActivesAsync(string? culture = null);
}