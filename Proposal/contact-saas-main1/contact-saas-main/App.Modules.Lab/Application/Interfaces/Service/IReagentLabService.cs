using App.Modules.Lab.Application.DTO;

namespace App.Modules.Lab.Application.Interfaces;

public interface IReagentLabService
{
    Task<IEnumerable<ReagentLabResponse>> GetAllAsync();
    Task<ReagentLabResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateReagentLabRequest request);
    Task UpdateAsync(Guid id, UpdateReagentLabRequest request);
    Task DeleteAsync(Guid id);
}