using App.Modules.Lab.Application.DTO;

namespace App.Modules.Lab.Application.Interfaces.Service;

public interface IReagentLabService
{
    Task<IEnumerable<ReagentLabResponse>> GetAllAsync();
    Task<ReagentLabResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveReagentLabRequest request);
    Task UpdateAsync(Guid id, SaveReagentLabRequest request);
    Task DeleteAsync(Guid id);
}