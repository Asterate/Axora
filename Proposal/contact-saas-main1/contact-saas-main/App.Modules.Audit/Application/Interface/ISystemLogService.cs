
using App.Modules.Audit.Application.DTO;

namespace App.Modules.Audit.Application.Interface;

public interface ISystemLogService
{
    Task<IEnumerable<SystemLogResponse>> GetAllAsync();
    Task CreateAsync(CreateSystemLogRequest request);
    Task<IEnumerable<SystemLogResponse>> GetRecentAsync(int take);
}