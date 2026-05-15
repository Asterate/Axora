using App.Modules.Lab.Application.DTO;

namespace App.Modules.Lab.Application.Interfaces;

public interface IReagentService
{
    Task<IEnumerable<ReagentListResponse>> GetAllAsync();
    Task<ReagentResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateReagentRequest request);
    Task UpdateAsync(Guid id, UpdateReagentRequest request);
    Task DeleteAsync(Guid id);
}