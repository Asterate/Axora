using App.Modules.Lab.Application.DTO;

namespace App.Modules.Lab.Application.Interfaces.Service;

public interface IReagentService
{
    Task<IEnumerable<ReagentListResponse>> GetAllAsync();
    Task<ReagentResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveReagentRequest request);
    Task UpdateAsync(Guid id, SaveReagentRequest request);
    Task DeleteAsync(Guid id);
}