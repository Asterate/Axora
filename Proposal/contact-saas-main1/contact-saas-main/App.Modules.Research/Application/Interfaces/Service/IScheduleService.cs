using App.Modules.Project.Application.DTO;

namespace App.Modules.Project.Application.Interfaces.Service;

public interface IScheduleService
{
    Task<IEnumerable<ScheduleListResponse>> GetAllAsync();
    Task<ScheduleResponse?> GetByIdAsync(Guid id);
    Task CreateAsync(SaveScheduleRequest request);
    Task UpdateAsync(Guid id, SaveScheduleRequest request);
    Task DeleteAsync(Guid id);
    Task<SaveScheduleRequest?> GetByIdEditAsync(Guid id);

}