using App.Modules.Audit.Application.DTO;
using App.Modules.Audit.Application.Interface;
using App.Modules.Audit.Application.Mappers;
using App.Shared.Contracts;

namespace App.Modules.Audit.Application.Services;

public class SystemLogService
{
    private readonly ISystemLogRepository _systemLogRepo;
    private readonly IUnitOfWork _uow;

    public SystemLogService(
        ISystemLogRepository systemLogRepo,
        IUnitOfWork uow)
    {
        _systemLogRepo = systemLogRepo;
        _uow = uow;
    }
    public async Task<IEnumerable<SystemLogResponse>> GetAllAsync()
    {
        var entities = await _systemLogRepo.GetAllAsync();
        return entities.Select(SystemLogMapper.ToResponse);
    }

    public async Task CreateAsync(CreateSystemLogRequest request)
    {
        var entity = SystemLogMapper.ToEntity(request);
        await _systemLogRepo.AddAsync(entity);
        await _uow.SaveChangesAsync(); // ← actually saves now
    }
    public async Task<IEnumerable<SystemLogResponse>> GetRecentAsync(int take)
    {
        var all = await _systemLogRepo.GetAllAsync();
        return all
            .Where(i => i.DeletedAt == null)
            .OrderByDescending(i => i.CreatedAt)
            .Take(take)
            .Select(SystemLogMapper.ToResponse);
    }
}